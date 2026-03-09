using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Collection schema management operations.
/// </summary>
public class CollectionService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;
    private readonly string _appId;

    internal CollectionService(CopepodHttpClient http, string orgId, string appId)
    {
        _http = http;
        _orgId = orgId;
        _appId = appId;
    }

    private string BasePath => $"/api/platform/orgs/{_orgId}/apps/{_appId}/collections";

    public Task<List<Collection>> ListAsync(CancellationToken ct = default)
        => _http.GetAsync<List<Collection>>(BasePath, ct);

    public Task<Collection> CreateAsync(CollectionCreate input, CancellationToken ct = default)
        => _http.PostAsync<Collection>(BasePath, input, ct);

    public Task<Collection> GetAsync(string collectionId, CancellationToken ct = default)
        => _http.GetAsync<Collection>($"{BasePath}/{collectionId}", ct);

    public Task DeleteAsync(string collectionId, CancellationToken ct = default)
        => _http.DeleteAsync($"{BasePath}/{collectionId}", ct);
}
