using System.Collections.Generic;
using System.Threading.Tasks;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Marketplace.Sample
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        private Dictionary<string, object> _cache;

        public InMemoryCacheProvider()
        {
            _cache = new Dictionary<string, object>();
        }

        public Task<object> Get(string key)
        {
            return _cache.ContainsKey(key) ? Task.FromResult(_cache[key]) : Task.FromResult<object>(null);
        }

        public Task Set(string key, object value)
        {
            _cache[key] = value;
            return Task.FromResult(true);
        }

        public Task Remove(string key)
        {
            _cache.Remove(key);
            return Task.FromResult(true);
        }
    }
}