using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Copepod.Sdk;

/// <summary>
/// Internal HTTP client with authentication and error handling.
/// </summary>
internal class CopepodHttpClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    private readonly HttpClient _http;
    private readonly string _baseUrl;
    private string? _bearerToken;
    private string? _apiKey;

    public CopepodHttpClient(string baseUrl, HttpClient? httpClient = null)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _http = httpClient ?? new HttpClient();
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("Copepod.Sdk/0.1.0");
    }

    public string BaseUrl => _baseUrl;

    public void SetBearerToken(string token)
    {
        _bearerToken = token;
        _apiKey = null;
    }

    public void SetApiKey(string key)
    {
        _apiKey = key;
        _bearerToken = null;
    }

    public void ClearAuth()
    {
        _bearerToken = null;
        _apiKey = null;
    }

    public string? GetToken() => _bearerToken;

    public async Task<T> GetAsync<T>(string path, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Get, path);
        return await SendAsync<T>(request, ct);
    }

    public async Task<T> PostAsync<T>(string path, object? body = null, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Post, path);
        if (body != null)
            request.Content = JsonContent.Create(body, options: JsonOptions);
        return await SendAsync<T>(request, ct);
    }

    public async Task PostAsync(string path, object? body = null, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Post, path);
        if (body != null)
            request.Content = JsonContent.Create(body, options: JsonOptions);
        await SendAsync(request, ct);
    }

    public async Task<T> PatchAsync<T>(string path, object body, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Patch, path);
        request.Content = JsonContent.Create(body, options: JsonOptions);
        return await SendAsync<T>(request, ct);
    }

    public async Task<T> PutAsync<T>(string path, object body, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Put, path);
        request.Content = JsonContent.Create(body, options: JsonOptions);
        return await SendAsync<T>(request, ct);
    }

    public async Task PutAsync(string path, object body, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Put, path);
        request.Content = JsonContent.Create(body, options: JsonOptions);
        await SendAsync(request, ct);
    }

    public async Task DeleteAsync(string path, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Delete, path);
        await SendAsync(request, ct);
    }

    public async Task<byte[]> DownloadAsync(string path, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Get, path);
        ApplyAuth(request);
        var response = await _http.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode)
            await ThrowApiException(response, ct);
        return await response.Content.ReadAsByteArrayAsync(ct);
    }

    public async Task<T> UploadAsync<T>(string path, byte[] data, string filename, string contentType, CancellationToken ct = default)
    {
        var request = CreateRequest(HttpMethod.Post, path);
        var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(data);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        content.Add(fileContent, "file", filename);
        request.Content = content;
        return await SendAsync<T>(request, ct);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string path)
    {
        var request = new HttpRequestMessage(method, $"{_baseUrl}{path}");
        ApplyAuth(request);
        return request;
    }

    private void ApplyAuth(HttpRequestMessage request)
    {
        if (_bearerToken != null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        else if (_apiKey != null)
            request.Headers.Add("X-API-Key", _apiKey);
    }

    private async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken ct)
    {
        var response = await _http.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode)
            await ThrowApiException(response, ct);
        var result = await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct);
        return result ?? throw new CopepodException(200, "Empty response body");
    }

    private async Task SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var response = await _http.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode)
            await ThrowApiException(response, ct);
    }

    private static async Task ThrowApiException(HttpResponseMessage response, CancellationToken ct)
    {
        var body = await response.Content.ReadAsStringAsync(ct);
        try
        {
            var error = JsonSerializer.Deserialize<ApiErrorResponse>(body);
            if (error != null)
                throw new CopepodException(error.Status, error.Message, error.Data);
        }
        catch (JsonException)
        {
            // Not a structured error response
        }
        throw new CopepodException((int)response.StatusCode, body);
    }

    private class ApiErrorResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("status")]
        public int Status { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string Message { get; set; } = "";

        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public object? Data { get; set; }
    }
}
