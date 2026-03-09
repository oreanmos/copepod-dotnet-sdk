using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Application management operations within an organization.
/// </summary>
public class AppService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;

    internal AppService(CopepodHttpClient http, string orgId)
    {
        _http = http;
        _orgId = orgId;
    }

    public Task<List<App>> ListAsync(CancellationToken ct = default)
        => _http.GetAsync<List<App>>($"/api/platform/orgs/{_orgId}/apps", ct);

    public Task<App> CreateAsync(AppCreate input, CancellationToken ct = default)
        => _http.PostAsync<App>($"/api/platform/orgs/{_orgId}/apps", input, ct);

    public Task<App> GetAsync(string appId, CancellationToken ct = default)
        => _http.GetAsync<App>($"/api/platform/orgs/{_orgId}/apps/{appId}", ct);

    public Task DeleteAsync(string appId, CancellationToken ct = default)
        => _http.DeleteAsync($"/api/platform/orgs/{_orgId}/apps/{appId}", ct);

    public Task<List<ApiKey>> ListApiKeysAsync(string appId, CancellationToken ct = default)
        => _http.GetAsync<List<ApiKey>>(
            $"/api/platform/orgs/{_orgId}/apps/{appId}/api-keys", ct);

    public Task<ApiKey> CreateApiKeyAsync(string appId, string name, List<string>? scopes = null, CancellationToken ct = default)
        => _http.PostAsync<ApiKey>(
            $"/api/platform/orgs/{_orgId}/apps/{appId}/api-keys",
            new { name, scopes },
            ct);

    public Task DeleteApiKeyAsync(string appId, string keyId, CancellationToken ct = default)
        => _http.DeleteAsync(
            $"/api/platform/orgs/{_orgId}/apps/{appId}/api-keys/{keyId}", ct);
}
