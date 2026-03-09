using System.Text.Json;
using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Record CRUD operations for a specific app.
/// </summary>
public class RecordService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;
    private readonly string _appId;

    internal RecordService(CopepodHttpClient http, string orgId, string appId)
    {
        _http = http;
        _orgId = orgId;
        _appId = appId;
    }

    private string BasePath(string collection) =>
        $"/api/platform/orgs/{_orgId}/apps/{_appId}/records/{collection}";

    /// <summary>
    /// List records with optional filtering and pagination.
    /// </summary>
    public Task<PaginatedResponse<Record>> ListAsync(
        string collection,
        ListParams? listParams = null,
        CancellationToken ct = default)
    {
        var qs = listParams?.ToQueryString() ?? "";
        return _http.GetAsync<PaginatedResponse<Record>>($"{BasePath(collection)}{qs}", ct);
    }

    /// <summary>
    /// Get a single record by ID.
    /// </summary>
    public Task<Record> GetAsync(string collection, string id, CancellationToken ct = default)
        => _http.GetAsync<Record>($"{BasePath(collection)}/{id}", ct);

    /// <summary>
    /// Create a new record.
    /// </summary>
    public Task<Record> CreateAsync(string collection, object data, CancellationToken ct = default)
        => _http.PostAsync<Record>(BasePath(collection), data, ct);

    /// <summary>
    /// Update an existing record.
    /// </summary>
    public Task<Record> UpdateAsync(string collection, string id, object data, CancellationToken ct = default)
        => _http.PatchAsync<Record>($"{BasePath(collection)}/{id}", data, ct);

    /// <summary>
    /// Delete a record.
    /// </summary>
    public Task DeleteAsync(string collection, string id, CancellationToken ct = default)
        => _http.DeleteAsync($"{BasePath(collection)}/{id}", ct);
}
