using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace onezl.iocp.com
{
  public class NoSqlDB
  {
    public static string ConnectionString { get; set; } = "";
    private static long BulkWriteStats(BulkWriteResult<BsonDocument> pResult)
    {
      if (ConnectionString.IndexOf("w=0") >= 0) //Dont get counts
      {
        return pResult.ProcessedRequests.Count;
      }
      return pResult.InsertedCount + pResult.ModifiedCount + pResult.DeletedCount;
    }
    private static long BulkWriteStats(MongoBulkWriteException<BsonDocument> pResult)
    {
      if (ConnectionString.IndexOf("w=0") >= 0) //Dont get counts
      {
        return pResult.Result.ProcessedRequests.Count;
      }
      return pResult.Result.InsertedCount + pResult.Result.ModifiedCount + pResult.Result.DeletedCount;
    }
    public static long BulkInsert(string pTable, IEnumerable<dynamic> pItems)
    {
      if (pItems == null || pItems.Count() == 0)
        return 0;
      IMongoClient oClient = new MongoClient(ConnectionString);
      IMongoDatabase oDatabase = oClient.GetDatabase(MongoUrl.Create(ConnectionString).DatabaseName);
      var oType = oDatabase.GetCollection<BsonDocument>(pTable);
      var oBulk = new List<WriteModel<BsonDocument>>();
      foreach (dynamic o in pItems)
      {
        oBulk.Add(new InsertOneModel<BsonDocument>(BsonDocument.Create(o)));
      }
      BulkWriteResult<BsonDocument> oResult = null;
      try
      {
        oResult = oType.BulkWrite(oBulk, new BulkWriteOptions { IsOrdered = false });
      }
      catch (MongoBulkWriteException<BsonDocument> e)
      {
        return BulkWriteStats(e);
      }
      return BulkWriteStats(oResult);
    }
    public static long BulkWrite(string pTable, List<WriteModel<BsonDocument>> pData)
    {
      if (pData == null || pData.Count() == 0)
        return 0;
      IMongoClient oClient = new MongoClient(ConnectionString);
      IMongoDatabase oDatabase = oClient.GetDatabase(MongoUrl.Create(ConnectionString).DatabaseName);
      var oType = oDatabase.GetCollection<BsonDocument>(pTable);
      BulkWriteResult<BsonDocument> oResult = null;
      try
      {
        oResult = oType.BulkWrite(pData, new BulkWriteOptions { IsOrdered = false });
      }
      catch (MongoBulkWriteException<BsonDocument> e)
      {
        return BulkWriteStats(e);
      }
      return BulkWriteStats(oResult);
    }
    public static List<BsonDocument> Find(string pTable, FilterDefinition<BsonDocument> pFilter, ProjectionDefinition<BsonDocument> pFields = null, int pSkip = -1, int pCount = -1)
    {
      IMongoClient oClient = new MongoClient(ConnectionString);
      IMongoDatabase oDatabase = oClient.GetDatabase(MongoUrl.Create(ConnectionString).DatabaseName);
      var oCollection = oDatabase.GetCollection<BsonDocument>(pTable);

      var lResult = oCollection.Find(pFilter);

      if (pFields != null)
        lResult = lResult.Project(pFields);
      if (pSkip > 0)
        lResult = lResult.Skip(pSkip);
      if (pCount > 0)
        lResult = lResult.Limit(pCount);
      //oMethodTimer.Mark("" + lResult.Count());
      return lResult.ToList();
    }

    //It is recommended to store a MongoClient instance in a global place, either as a static variable or in an IoC container with a singleton lifetime.
    private static volatile IMongoClient _client;
    private static object syncClient = new Object();
    public static IMongoClient Client
    {
      get
      {
        if (_client == null)
        {
          lock (syncClient)
          {
            if (_client == null)
              _client = new MongoClient(ConnectionString);
          }
        }
        return _client;
      }
    }
    private static string _defaultDBName = MongoUrl.Create(ConnectionString).DatabaseName;
    private static ConcurrentDictionary<string, IMongoCollection<BsonDocument>> _collection = new ConcurrentDictionary<string, IMongoCollection<BsonDocument>>();
    public static IMongoCollection<BsonDocument> GetCollection(string collectionName)
    {
      Count++;
      return GetCollection(_defaultDBName, collectionName);
    }
    public static int Count;
    public static IMongoCollection<BsonDocument> GetCollection(string dbName, string collectionName)
    {
      var key = $"{dbName}.{collectionName}";
      return _collection.GetOrAdd(key, k =>
      {
        var db = Client.GetDatabase(dbName);
        var result = db.GetCollection<BsonDocument>(collectionName);
        if (result == null) throw new MongoException($"Collection: {collectionName} Not Found!");
        return result;
      });
    }
  }

  public static class BsonExtensions
  {
    public static string StringValue(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return "";
      if (pWhat[pField].IsString)
        return pWhat[pField].AsString;
      if (pWhat[pField].IsBsonArray && pWhat[pField].AsBsonArray[0].IsString)
        return pWhat[pField].AsBsonArray[0].AsString;
      return "";
    }
    public static string StringValue(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.StringValue(pField);
    }
    public static IEnumerable<string> StringValues(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return new string[] { };
      if (pWhat[pField].IsString)
        return new[] { pWhat[pField].AsString };
      if (pWhat[pField].IsBsonArray && pWhat[pField].AsBsonArray.Any() && pWhat[pField].AsBsonArray[0].IsString)
        return pWhat[pField].AsBsonArray.Select(x => x.AsString);
      return new string[] { };
    }
    public static IEnumerable<string> StringValues(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.StringValues(pField);
    }
    public static int IntValue(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return 0;
      return pWhat[pField].IsInt32 ? pWhat[pField].AsInt32 : 0;
    }
    public static int IntValue(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.IntValue(pField);
    }
    public static long LongValue(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return 0;
      return pWhat[pField].IsInt64 ? pWhat[pField].AsInt64 : IntValue(pWhat, pField);
    }
    public static long LongValue(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.IntValue(pField);
    }
    public static bool BoolValue(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return false;
      return pWhat[pField].IsBoolean && pWhat[pField].AsBoolean;
    }
    public static bool BoolValue(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.BoolValue(pField);
    }
    public static DateTime DateValue(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return DateTime.MinValue;
      return pWhat[pField].IsValidDateTime ? pWhat[pField].ToUniversalTime() : DateTime.MinValue;
    }
    public static DateTime DateValue(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.DateValue(pField);
    }
    public static BsonArray ArrayValue(this BsonDocument pWhat, string pField)
    {
      if (!pWhat.Contains(pField))
        return new BsonArray();
      return pWhat[pField].IsBsonArray ? pWhat[pField].AsBsonArray : new BsonArray();
    }
    public static BsonArray ArrayValue(this BsonValue pWhat, string pField)
    {
      return pWhat.AsBsonDocument.ArrayValue(pField);
    }
    public static ExpandoObject ToDynamic(this object pObject)
    {
      IDictionary<string, object> oResult = new ExpandoObject();
      foreach (var p in pObject.GetType().GetTypeInfo().GetProperties())
        oResult.Add(p.Name, p.GetValue(pObject));
      return (ExpandoObject)oResult;
    }
  }
}


//var result = ProductContentCache.Instance.GetOrAdd(item.ToUpper(), () =>
//{
//  var oFilterProduct = Builders<BsonDocument>.Filter.In("Products", new List<string> { item });
//  //var oFields = Builders<BsonDocument>.Projection.Include("Type").Include("ID");
//  Dictionary<string, string> dResult = new Dictionary<string, string>();
//  Stopwatch sw = new Stopwatch();
//  sw.Start();
//  foreach (BsonDocument oDocument in NoSqlDB.Find("Contents", oFilterProduct))
//  {
//    string sType = oDocument.StringValue("Type");
//    string sID = oDocument.StringValue("ID");
//    dResult[sID] = sType;
//    var resulttemp = ContentCache.Instance.GetOrAdd(oDocument.StringValue("ID").ToUpper(), () =>
//    {
//      return oDocument;
//    });

//  }
//  sw.Stop();
//  TimeSpan ts2 = sw.Elapsed;
//  LogHelper.WriteLog("getcontent from Monodb productid:" + item + " Time " + ts2.TotalMilliseconds + "ms.");






//  return dResult;
//});
