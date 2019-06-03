using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Walmart.Sdk.Base.Http
{
    public interface IAccessTokenCacheProvider
    {
        Task<string> GetStoredToken();

        Task SetStoredToken(string token);
    }
}
