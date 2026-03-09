using System.Text.Json;

namespace Copepod.Sdk.Platform;

/// <summary>
/// File upload and download operations.
/// </summary>
public class FileService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;
    private readonly string _appId;

    internal FileService(CopepodHttpClient http, string orgId, string appId)
    {
        _http = http;
        _orgId = orgId;
        _appId = appId;
    }

    private string BasePath =>
        $"/api/platform/orgs/{_orgId}/apps/{_appId}/files";

    /// <summary>
    /// Upload a file to a record's file field.
    /// </summary>
    public Task<JsonElement> UploadAsync(
        string collection,
        string recordId,
        string filename,
        byte[] data,
        string contentType,
        CancellationToken ct = default)
        => _http.UploadAsync<JsonElement>(
            $"{BasePath}/{collection}/{recordId}/{filename}",
            data, filename, contentType, ct);

    /// <summary>
    /// Download a file from a record.
    /// </summary>
    public Task<byte[]> DownloadAsync(
        string collection,
        string recordId,
        string filename,
        CancellationToken ct = default)
        => _http.DownloadAsync(
            $"{BasePath}/{collection}/{recordId}/{filename}", ct);

    /// <summary>
    /// Generate a signed URL for temporary public file access.
    /// </summary>
    public async Task<string> SignUrlAsync(string key, int? expirySecs = null, CancellationToken ct = default)
    {
        var resp = await _http.PostAsync<SignedUrlResponse>(
            $"/api/platform/apps/{_appId}/files/sign",
            new { key, expiry_secs = expirySecs },
            ct);
        return resp.SignedUrl;
    }

    private class SignedUrlResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("signed_url")]
        public string SignedUrl { get; set; } = "";
    }
}
