using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Base.Http
{
    public class OAuthHttpFactory : IHttpFactory
    {
        public IHandler GetHttpHandler(IHttpConfig cfg, ICacheProvider cacheProvider)
        {
            return new OAuthHttpHandler(cfg, cacheProvider);
        }
    }
}