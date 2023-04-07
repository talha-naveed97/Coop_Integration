using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;

namespace API.Facebook
{
    public class FacebookApiService : IFacebookApiService
    {
        private readonly ILogger<FacebookApiService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SecretClient _keyVaultClient;

        public FacebookApiService(ILogger<FacebookApiService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<LeadsAdModel> GetLeadsAdByAdGroup(string adGroupId)
        {
            string requestUrl = $"/{adGroupId}/leads&access_token={GetAppId()}|{GetAppSecret()}";
            var httpClient = GetHttpClient();
            var responseResult = httpClient.GetAsync(requestUrl).Result;
            var content = responseResult.Content.ReadAsStringAsync().Result;
            var leads = JsonConvert.DeserializeObject<List<LeadsAdModel>>(content);
            return leads;
        }

        public IEnumerable<LeadsAdModel> GetLeadsAdByForm(string formId)
        {
            string requestUrl = $"/{formId}/leads&access_token={GetAppId()}|{GetAppSecret()}";
            var httpClient = GetHttpClient();
            var responseResult = httpClient.GetAsync(requestUrl).Result;
            var content = responseResult.Content.ReadAsStringAsync().Result;
            var leads = JsonConvert.DeserializeObject<List<LeadsAdModel>>(content);
            return leads;
        }

        private HttpClient GetHttpClient()
        {

            var httpClient = _httpClientFactory.CreateClient();
            // Url can be configurable and can be defined in the config files
            httpClient.BaseAddress = new Uri("https://graph.facebook.com/v16.0");

            return httpClient;
        }

        private string GetAppId()
        {
            string appIdKey = "fbappkey";
            var appId = _keyVaultClient.GetSecret(appIdKey);
            if (string.IsNullOrWhiteSpace(appId?.Value?.Value))
                throw new CredentialUnavailableException($"Could not find any secret value for key {appIdKey}");

            return appId.Value.Value;
        }

        private string GetAppSecret()
        {
            string appsecretKey = "fbappsecret";
            var appSecret = _keyVaultClient.GetSecret(appsecretKey);
            if (string.IsNullOrWhiteSpace(appSecret?.Value?.Value))
                throw new CredentialUnavailableException($"Could not find any secret value for key {appsecretKey}");

            return appSecret.Value.Value;
        }

    }
}