
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Base.Http
{
    public interface IRequestFactory
    {
        Request CreateRequest(IEndpointConfig config);
    }
}
