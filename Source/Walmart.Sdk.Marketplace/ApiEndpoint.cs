using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Marketplace
{
    public class ApiEndpoint:BaseEndpoint
    {
        protected ApiVersion apiVersion { get; set; } = ApiVersion.V3;
        public ApiEndpoint(IEndpointClient apiClient) : base(apiClient)
        {
        }

        protected string BuildEndpointUrl(string path)
        {
            var countryPrefix = string.IsNullOrEmpty(config.CountryPrefix) ? "" : $"{config.CountryPrefix}/";
            return $"/v{(int)apiVersion}/{countryPrefix}{path}";
        }
    }
}
