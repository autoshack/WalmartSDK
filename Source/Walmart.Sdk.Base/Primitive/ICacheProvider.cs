using System.Threading.Tasks;

namespace Walmart.Sdk.Base.Primitive
{
    public interface ICacheProvider
    {
        Task<object> Get(string key);
        Task Set(string key, object value);

        Task Remove(string key);
    }
}