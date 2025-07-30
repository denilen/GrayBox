using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiClientExample;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<HttpResponseMessage> CreateAsync<T>(string endpoint, T data, Dictionary<string, string> customHeaders = null)
    {
        var content = PrepareContent(data, customHeaders);
        return await _httpClient.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> CreateAsync(string endpoint, Dictionary<string, string> customHeaders = null)
    {
        return await _httpClient.PostAsync(endpoint, null);
    }

    public Task<HttpResponseMessage> UpdateAsync<T>(string endpoint, T data, Dictionary<string, string> customHeaders = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> UpdateAsync(string endpoint, Dictionary<string, string> customHeaders = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> PatchAsync<T>(string endpoint, T data, Dictionary<string, string> customHeaders = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> PatchAsync(string endpoint, Dictionary<string, string> customHeaders = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> ReadAsync(string endpoint, Dictionary<string, string> customHeaders = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> DeleteAsync(string endpoint, Dictionary<string, string> customHeaders = null)
    {
        throw new NotImplementedException();
    }

    public async Task<HttpResponseMessage> UploadFileAsync(
        string endpoint,
        Stream fileStream,
        string fileName,
        Dictionary<string, string> formData = null,
        Dictionary<string, string> customHeaders = null)
    {
        var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
        content.Add(fileContent, "file", fileName);

        if (formData != null)
        {
            foreach (var field in formData)
            {
                content.Add(new StringContent(field.Value), field.Key);
            }
        }

        var request = new HttpRequestMessage(HttpMethod.Post, endpoint) { Content = content };
        AddCustomHeaders(request, customHeaders);
        return await _httpClient.SendAsync(request);
    }

    public async Task DownloadFileAsync(string endpoint, Stream outputStream, Dictionary<string, string> customHeaders = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        AddCustomHeaders(request, customHeaders);
        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await response.Content.CopyToAsync(outputStream);
    }

    // Остальные методы (UpdateAsync, PatchAsync, ReadAsync, DeleteAsync) остаются без изменений
    // ...

    private HttpContent PrepareContent<T>(T data, Dictionary<string, string> customHeaders)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        if (customHeaders != null)
        {
            foreach (var header in customHeaders)
            {
                content.Headers.Add(header.Key, header.Value);
            }
        }
        return content;
    }

    private void AddCustomHeaders(HttpRequestMessage request, Dictionary<string, string> customHeaders)
    {
        if (customHeaders != null)
        {
            foreach (var header in customHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
