using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Base.Http
{
    public class RequestFactory:IRequestFactory
    {
        public Request CreateRequest(IEndpointConfig config)
        {
            switch (config.AuthType)
            {
                case AuthenticationType.OAuth:
                {
                    return new OAuthRequest(config.GetRequestConfig());
                }

                case AuthenticationType.SignatureBased:
                default:
                {
                    return new Request(config.GetRequestConfig());
                }
            }
        }
    }
}
