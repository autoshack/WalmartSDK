/**
Copyright (c) 2018-present, Walmart Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities.Encoders;
using Walmart.Sdk.Base.Http;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Base.Http
{
    public class Request: IRequest
    {
        private Primitive.Config.IRequestConfig config;
        public Primitive.Config.IRequestConfig Config { get { return config; } }
        public string EndpointUri { get; set; }
        public HttpRequestMessage HttpRequest { get; }
        public Dictionary<string, string> QueryParams { get; set; } = new Dictionary<string, string>();

        public HttpMethod Method
        {
            get { return HttpRequest.Method; }
            set { HttpRequest.Method = value; }
        }

        public Request(Primitive.Config.IRequestConfig cfg)
        {
            config = cfg;
            HttpRequest = new HttpRequestMessage();
        }

        public void AddMultipartContent(byte[] content)
        {
            var multipartContent = new MultipartFormDataContent
            {
                new ByteArrayContent(content)
            };
            HttpRequest.Content = multipartContent;
        }

        public void AddMultipartContent(System.IO.Stream contentStream)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StreamContent(contentStream));
            HttpRequest.Content = multipartContent;
        }

        public void AddPayload(string payload)
        {
            HttpRequest.Content = new StringContent((string)payload, Encoding.UTF8, GetContentType());
        }

        public string BuildQueryParams()
        {
            var list = new List<string>();
            foreach (var param in this.QueryParams)
            {
                if (param.Value != null) {
                    list.Add(param.Key + "=" + param.Value);    
                }
            }
            if (list.Count > 0)
            {
                return "?" + string.Join("&", list);
            }
            return "";
        }

        public void FinalizePreparation()
        {
          
            HttpRequest.RequestUri = new Uri(config.BaseUrl + EndpointUri + BuildQueryParams());
            AddWalmartHeaders();
        }

        private void AddWalmartHeaders()
        {
            HttpRequest.Headers.Clear();
            var authHeaderValue = GetAuthorizationHeader();

            HttpRequest.Headers.Add("WM_SVC.NAME", config.ServiceName);
            HttpRequest.Headers.Add("WM_QOS.CORRELATION_ID", config.ServiceName);
            HttpRequest.Headers.Add("Authorization", $"Basic {authHeaderValue}");
            HttpRequest.Headers.Add("Accept", GetAcceptType());

            if (!string.IsNullOrEmpty(config.AccessToken))
            {
                HttpRequest.Headers.Add("WM_SEC.ACCESS_TOKEN",config.AccessToken);
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
        public string GetContentType()
        {
            switch (config.ContentType)
            {
                case ContentTypeFormat.JSON:
                    return "application/json";
                case ContentTypeFormat.FORM_URLENCODED:
                    return "application/x-www-form-urlencoded";
                default:
                case ContentTypeFormat.XML:
                    return "application/xml";
              
            }
        }

        private string GetAuthorizationHeader()
        {
           return Base64Encode($"{config.Credentials.ClientId}:{config.Credentials.ClientSecret}");
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private void SetAccessToken(string accessToken)
        {
            this.Config.AccessToken = accessToken;
        }
        private string GetSignature(string timestamp)
        {
            if (config.Credentials is null)
            {
                throw new Base.Exception.InitException("Configuration is not initialized with Merchant Credentials!");
            }

            var creds = config.Credentials;
            var requestUri = HttpRequest.RequestUri.ToString();
            var httpMethod = HttpRequest.Method.Method.ToUpper();
            // Construct the string to sign
            string stringToSign = string.Join("\n", new List<string>() {
                //creds.ConsumerId,
                requestUri,
                httpMethod,
                timestamp
            }) + "\n"; // extra newline symbol required for valid signature

            try
            {
                return Util.DigitalSignature.SignData(stringToSign, creds.ClientSecret);
            }
            catch (System.Exception ex)
            {
                //pop up this to the user of SDK 
                throw Base.Exception.SignatureException.Factory(creds.ClientId, requestUri, httpMethod, ex);
            }
        }
    }
}
