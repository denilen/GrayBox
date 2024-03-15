using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Facility.HttpClient.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Facility.HttpClient
{
    public abstract class BaseServiceClient<TOptions> where TOptions : BaseClientOptions, new()
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        protected TOptions ClientOptions { get; }

        protected BaseServiceClient(IHttpClientFactory clientFactory, TOptions clientOptions, ILogger logger)
        {
            ClientOptions = clientOptions;
            _httpClient = clientFactory.CreateClient(AppDomain.CurrentDomain.FriendlyName);
            _httpClient.Timeout = TimeSpan.FromSeconds(ClientOptions.Timeout);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue(ClientOptions.Application, ClientOptions.ApplicationVersion));
            _httpClient.BaseAddress = SetBaseAddress();
        }

        protected virtual Uri SetBaseAddress()
        {
            return ClientOptions.BaseUrl;
        }


        protected Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> urlParameters, CancellationToken cancellationToken)
        {
            return SendAsync<TResponse>(HttpMethod.Get, url, urlParameters, null, null, cancellationToken);
        }

        protected Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> urlParameters, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            return SendAsync<TResponse>(HttpMethod.Get, url, urlParameters, null, customHeaders, cancellationToken);
        }

        protected Task<byte[]> GetByteArrayAsync(string url, Dictionary<string, string> urlParameters, IEnumerable<KeyValuePair<string, string>> customHeaders, CancellationToken cancellationToken)
        {
            return SendByteArrayAsync(HttpMethod.Get, url, urlParameters, null, customHeaders, cancellationToken);
        }

        protected Task<TResponse> PostAsync<TResponse, TRequest>(string url, Dictionary<string, string> urlParameters, TRequest tRequest, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(OnSerializeContent(tRequest), Encoding.UTF8, "application/json");
            return SendAsync<TResponse>(HttpMethod.Post, url, urlParameters, content, customHeaders, cancellationToken);
        }

        protected Task<TResponse> PostAsync<TResponse, TRequest>(string url, Dictionary<string, string> urlParameters, TRequest tRequest, CancellationToken cancellationToken)
        {
            var content = new StringContent(OnSerializeContent(tRequest), Encoding.UTF8, "application/json");
            return SendAsync<TResponse>(HttpMethod.Post, url, urlParameters, content, null, cancellationToken);
        }

        protected Task<TResponse> PostAsync<TResponse>(string url, Dictionary<string, string> urlParameters, HttpContent content, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            return SendAsync<TResponse>(HttpMethod.Post, url, urlParameters, content, customHeaders, cancellationToken);
        }

        protected Task<TResponse> PostAsync<TResponse>(string url, Dictionary<string, string> urlParameters, HttpContent content, CancellationToken cancellationToken)
        {
            return SendAsync<TResponse>(HttpMethod.Post, url, urlParameters, content, null, cancellationToken);
        }

        protected Task<byte[]> PostByteArray<TRequest>(string url, Dictionary<string, string> urlParameters, TRequest tRequest, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(OnSerializeContent(tRequest), Encoding.UTF8, "application/json");
            return SendByteArrayAsync(HttpMethod.Post, url, urlParameters, content, customHeaders, cancellationToken);
        }

        protected Task<TResponse> PutAsync<TResponse, TRequest>(string url, Dictionary<string, string> urlParameters, TRequest tRequest, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(OnSerializeContent(tRequest), Encoding.UTF8, "application/json");
            return SendAsync<TResponse>(HttpMethod.Put, url, urlParameters, content, customHeaders, cancellationToken);
        }

        protected async Task<HttpStatusCode> HeadAsync(string url, Dictionary<string, string> urlParameters, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            var result = await SendAsync(HttpMethod.Head, url, urlParameters, null, customHeaders, cancellationToken);
            return result.StatusCode;
        }

        protected Task<TResponse> DeleteAsync<TResponse>(string url, Dictionary<string, string> urlParameters, IEnumerable<KeyValuePair<string, string>> customHeaders,
            CancellationToken cancellationToken)
        {
            return SendAsync<TResponse>(HttpMethod.Delete, url, urlParameters, null, customHeaders, cancellationToken);
        }

        protected async Task<string> AddUrlParametersAsync(string url, Dictionary<string, string> urlParameters)
        {
            if (urlParameters != null && urlParameters.Count > 0)
            {
                return $"{url}?" +
                       await new FormUrlEncodedContent(urlParameters)
                           .ReadAsStringAsync().ConfigureAwait(false);
            }

            return url;
        }

        private void EnsureSuccessStatusCode(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpResponseException(httpResponseMessage);
            }
        }

        private TResponse DeserializeObject<TResponse>(string value)
        {
            if (!typeof(TResponse).IsPrimitive && typeof(TResponse) != typeof(string))
                return JsonConvert.DeserializeObject<TResponse>(value);
            return (TResponse)Convert.ChangeType(value, typeof(TResponse));
        }

        protected virtual async Task<TResponse> SendAsync<TResponse>(
            HttpMethod method, string url, Dictionary<string, string> urlParameters, HttpContent content, IEnumerable<KeyValuePair<string, string>> customHeaders, CancellationToken cancellationToken)
        {
            var result = await SendAsync(method, url, urlParameters, content, customHeaders, cancellationToken);
            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return DeserializeObject<TResponse>(resultContent);
        }

        protected virtual async Task<TResponse> SendAsync<TResponse>(
            HttpMethod method, string url, Dictionary<string, string> urlParameters, HttpContent content, CancellationToken cancellationToken)
        {
            var result = await SendAsync(method, url, urlParameters, content, null, cancellationToken);
            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return DeserializeObject<TResponse>(resultContent);
        }

        protected async Task<byte[]> SendByteArrayAsync(
            HttpMethod method, string url, Dictionary<string, string> urlParameters, HttpContent content, IEnumerable<KeyValuePair<string, string>> customHeaders, CancellationToken cancellationToken)
        {
            var result = await SendAsync(method, url, urlParameters, content, customHeaders, cancellationToken);
            return await result.Content.ReadAsByteArrayAsync();
        }

        private IEnumerable<KeyValuePair<string, string>> FitHeadersToUrl(string url, IEnumerable<KeyValuePair<string, string>> customHeaders = null)
        {
            var headers = (customHeaders ?? new List<KeyValuePair<string, string>>())
                .ToDictionary(x => x.Key, y => y.Value);

            if (ClientOptions.CallOptions?.ContainsKey(url) == true && ClientOptions.CallOptions[url].Headers != null)
            {
                ClientOptions.CallOptions[url].Headers.ToList().ForEach(header =>
                {
                    var (key, value) = header;
                    if (headers.ContainsKey(key)) headers[key] = value;
                    else headers.Add(key, value);
                });
            }

            return headers;
        }

        protected IEnumerable<KeyValuePair<string, string>> CombineHeaders(IEnumerable<KeyValuePair<string, string>> h1, IEnumerable<KeyValuePair<string, string>> h2)
        {
            var result = new Dictionary<string, string>();
            foreach (var (key, value) in h1)
            {
                result.Add(key, value);
            }

            foreach (var (key, value) in h2)
            {
                if (result.ContainsKey(key) == false)
                    result.Add(key, value);
                else
                    result[key] = value;
            }

            return result;
        }

        protected async Task<HttpResponseMessage> SendAsync(
            HttpMethod method, string url, Dictionary<string, string> urlParameters, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers, CancellationToken cancellationToken)
        {
            var sendHeaders = FitHeadersToUrl(url, headers);
            var urlWithParams = await AddUrlParametersAsync(url, urlParameters);
            var httpRequestMessage =
                new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = new Uri(urlWithParams, UriKind.RelativeOrAbsolute),
                    Content = content
                };

            if (sendHeaders != null)
            {
                foreach (var keyValuePair in sendHeaders)
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
                }
            }

            //Headers.Clear();
            await OnBeforeRequestAsync(httpRequestMessage, cancellationToken);

            var result = await _httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            EnsureSuccessStatusCode(result);
            return result;
        }

        protected Task<IEnumerable<KeyValuePair<string, string>>> GetBearerAuthorizationHeaderAsync(string token)
            => GetAuthorizationHeaderAsync(token, "Bearer");

        protected Task<IEnumerable<KeyValuePair<string, string>>> GetPrivateAuthorizationHeaderAsync(string token)
            => GetAuthorizationHeaderAsync(token, "PrivateToken");

        protected Task<IEnumerable<KeyValuePair<string, string>>> GetAuthorizationHeaderAsync(string token, string tokenType)
        {
            const string key = "Authorization";

            var customHeaders = new CustomDictionary();
            customHeaders.Add(key, $"{tokenType} {token}");

            return Task.FromResult((IEnumerable<KeyValuePair<string, string>>)customHeaders);
        }

        protected virtual Task OnBeforeRequestAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        protected virtual string OnSerializeContent<TRequest>(TRequest tRequest)
        {
            return JsonConvert.SerializeObject(tRequest);
        }
    }
}
