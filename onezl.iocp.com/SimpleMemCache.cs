using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace onezl.iocp.com
{
  public class SimpleMemCache<TKey, TValue>
  {


    private long Capacity = 100000;
    private object lockobj = new object();
    private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());



    public SimpleMemCache(long capacity)
    {
      Capacity = capacity;

    }
    public TValue GetOrExist(TKey key, out bool exist)
    {
      TValue value;
      if (_cache.TryGetValue<TValue>(key, out value))
      {
        exist = true;
        return value;
      }
      exist = false;

      return value;
    }

    public TValue GetOrAdd(TKey key, Func<TValue> valueGenerator)
    {
      return GetOrAdd(key, () => Task.FromResult(valueGenerator()));
    }

    public TValue GetOrAdd(TKey key, Func<Task<TValue>> valueGenerator)
    {
      TValue value;
      if (_cache.TryGetValue<TValue>(key, out value))
      {
        return value;
      }
      value = valueGenerator().Result;
      if (_cache.Count < Capacity)
      {

        _cache.Set<TValue>(key, value, DateTimeOffset.UtcNow.AddMinutes(60));

      }

      return value;
    }

    public void Remove(TKey key)
    {

      _cache.Remove(key);

    }
    public void Put(TKey key, TValue value)
    {

      if (_cache.Count < Capacity)
      {
        _cache.Set<TValue>(key, value, DateTimeOffset.UtcNow.AddMinutes(60));

      }

    }
  }
}
