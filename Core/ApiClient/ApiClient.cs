using Core.Config;
using Newtonsoft.Json;
using RestSharp;

namespace Core.ApiClient
{
    public class ApiClient : IDisposable
    {
        private readonly RestClient _client;
        private readonly JsonSerializerSettings _jsonSettings;
        private bool _disposed = false;

        public ApiClient()
        {
            var baseUrl = ConfigurationReader.ApiBaseUrl;
            var timeout = ConfigurationReader.ApiTimeout;

            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };

            _client = new RestClient(options);
            
            _jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        public RestResponse Execute(RestRequest request)
        {
            return _client.Execute(request);
        }

        public RestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            return _client.Execute<T>(request);
        }

        public T DeserializeResponse<T>(RestResponse response)
        {
            if (response?.Content == null)
                return default;

            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content, _jsonSettings);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize response: {ex.Message}", ex);
            }
        }

        public RestRequest CreateJsonRequest<T>(T body, Method method, string resource) where T : class
        {
            var request = new RestRequest(resource, method);
            request.AddJsonBody(body);
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        public RestRequest CreateRequest(Method method, string resource)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Accept", "application/json");
            return request;
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _client?.Dispose();
                _disposed = true;
            }
        }
    }
}
