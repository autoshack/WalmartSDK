using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Base.Http
{
    public class OAuthRequest :  Request
    {
        public OAuthRequest(IRequestConfig config) : base(config)
        {
        }

        protected override void AddWalmartHeaders()
        {
            HttpRequest.Headers.Clear();
            var authHeaderValue = GetAuthorizationHeader();

            HttpRequest.Headers.Add("WM_SVC.NAME", config.ServiceName);
            HttpRequest.Headers.Add("WM_QOS.CORRELATION_ID", config.ServiceName);
            HttpRequest.Headers.Add("Authorization", $"Basic {authHeaderValue}");
            HttpRequest.Headers.Add("Accept", GetAcceptType());
            if (HttpRequest.Content == null)
            {
                HttpRequest.Content = new StringContent("", Encoding.UTF8);
            }
            HttpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(GetContentType());

            if (!string.IsNullOrEmpty(config.AccessToken))
            {
                HttpRequest.Headers.Add("WM_SEC.ACCESS_TOKEN", config.AccessToken);
            }
        }
        public string GetAcceptType()
        {
            switch (config.ApiFormat)
            {
                case Primitive.ApiFormat.JSON:
                    return "application/json";
                default:
                case Primitive.ApiFormat.XML:
                    return "application/xml";
            }
        }

        public override string GetContentType()
        {
            switch (config.ContentType)
            {
                case ContentTypeFormat.FORM_URLENCODED:
                    {
                        return "application/x-www-form-urlencoded";
                    }
                case ContentTypeFormat.JSON:
                    {
                        return "application/json";
                    }

                default:
                case ContentTypeFormat.XML:
                    {
                        return "application/xml";
                    }

            }
        }
        private string GetAuthorizationHeader()
        {
            return Base64Encode($"{config.Credentials.Id}:{config.Credentials.Secret}");
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    
    }
}