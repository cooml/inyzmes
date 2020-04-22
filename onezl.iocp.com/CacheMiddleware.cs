using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace onezl.iocp.com
{


  public class CacheMiddleware<TKey, TValue>
  {
    //使用示例：
    //internal class ProductContentCache
    //{
    //  private static CacheMiddleware<string, Dictionary<string, string>> _instance;
    //  private static readonly object Lock = new object();
    //  public static CacheMiddleware<string, Dictionary<string, string>> Instance
    //  {
    //    get
    //    {
    //      if (_instance != null)
    //      { return _instance; }
    //      lock (Lock)
    //      {
    //        if (_instance == null)
    //        { _instance = new CacheMiddleware<string, Dictionary<string, string>>(100000); }
    //      }
    //      return _instance;
    //    }
    //  }
    //}

    //var result = ProductContentCache.Instance.GetOrAdd("keystring", () =>
    //{

    //  return "valuestring";
    //});












    private SimpleMemCache<TKey, TValue> PrivateModdle_instance;
    private readonly object Lock = new object();
    public CacheMiddleware(long capacity)
    {


      lock (Lock)
      {
        if (PrivateModdle_instance == null)
        {
          PrivateModdle_instance = new SimpleMemCache<TKey, TValue>(capacity);
        }
      }



    }


    public TValue GetOrExist(TKey key, out bool exist)
    {
      try
      {
        return this.PrivateModdle_instance.GetOrExist(key, out exist);


      }
      catch (Exception ex)
      {
        LogHelper.WriteLog("CacheMiddleware.GetOrExist exp" + ex.Message + ex.Source);
        exist = false;
        return default(TValue);

      }

    }

    public TValue GetOrAdd(TKey key, Func<TValue> valueGenerator)
    {
      try
      {
        return this.PrivateModdle_instance.GetOrAdd(key, valueGenerator);
      }
      catch (Exception ex)
      {

        LogHelper.WriteLog("CacheMiddleware.GetOrAdd exp" + ex.Message + ex.Source);
        return Task.FromResult(valueGenerator()).Result;
      }

    }


    /// <summary>
    /// put = add or update
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Put(TKey key, TValue value)
    {
      try
      {
        this.PrivateModdle_instance.Put(key, value);
      }
      catch (Exception ex)
      {

        LogHelper.WriteLog("CacheMiddleware.Put exp" + ex.Message + ex.Source);
      }

    }
    public void Remove(TKey key)
    {
      try
      {
        this.PrivateModdle_instance.Remove(key);
      }
      catch (Exception ex)
      {
        LogHelper.WriteLog("CacheMiddleware.Remove exp" + ex.Message + ex.Source);
      }

    }





  }
}