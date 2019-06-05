using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Walmart.Sdk.Base.Http.Exception;
using Walmart.Sdk.Base.Http.Fetcher;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Base.Http
{
    class OAuthHttpHandler : Handler
    {
        private ICacheProvider _cacheProvider;
        private IAccessTokenFactory _accessTokenFactory;
        private string accessTokenCacheKey = "access_token";

        public OAuthHttpHandler(IHttpConfig apiConfig, ICacheProvider cacheProvider) : base(apiConfig)
        {
            //TODO: Change retry policy?
            RetryPolicy = new Retry.LuckyMePolicy();
            _cacheProvider = cacheProvider;
            _accessTokenFactory = new AccessTokenFactory(this.Fetcher);
        }

        protected override async Task<IResponse> ExecuteAsync(IRequest request)
        {
            request.Config.AccessToken = await GetAccessToken(request.Config.Credentials);
            try
            {
                return await RetryPolicy.GetResponse(Fetcher, request);
            }
            catch (InvalidAccessTokenException ex)
            {
                await RefreshAccessToken(request.Config.Credentials);
                return await ExecuteAsync(request);
            }
        }

        private async Task<string> RefreshAccessToken(Credentials credentials)
        {
            await this._cacheProvider.Remove(accessTokenCacheKey);
            return await GetAccessToken(credentials);
        }

        private async Task<string> GetAccessToken(Credentials credentials)
        {
            var accessToken = await this._cacheProvider.Get(accessTokenCacheKey);

            if (accessToken is null)
            {
                accessToken = await _accessTokenFactory.RetrieveAccessToken(credentials);
                await this._cacheProvider.Set(accessTokenCacheKey, accessToken);
            }

            return accessToken.ToString();
        }
    }
}