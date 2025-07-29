using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public interface IApiService
{
    // CRUD (JSON)
    Task<HttpResponseMessage> CreateAsync<T>(string endpoint, T data, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> CreateAsync(string endpoint, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> UpdateAsync<T>(string endpoint, T data, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> UpdateAsync(string endpoint, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> PatchAsync<T>(string endpoint, T data, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> PatchAsync(string endpoint, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> ReadAsync(string endpoint, Dictionary<string, string> customHeaders = null);
    Task<HttpResponseMessage> DeleteAsync(string endpoint, Dictionary<string, string> customHeaders = null);

    // Files
    Task<HttpResponseMessage> UploadFileAsync(
        string endpoint,
        Stream fileStream,
        string fileName,
        Dictionary<string, string> formData = null,
        Dictionary<string, string> customHeaders = null);

    Task DownloadFileAsync(
        string endpoint,
        Stream outputStream,
        Dictionary<string, string> customHeaders = null);
}