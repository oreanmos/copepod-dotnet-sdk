using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Record CRUD operations bound to a single collection.
/// </summary>
public class ScopedRecordCollectionService
{
    private readonly RecordService _records;

    internal ScopedRecordCollectionService(
        CopepodHttpClient http,
        string orgId,
        string appId,
        string collection)
    {
        _records = new RecordService(http, orgId, appId);
        Collection = collection;
    }

    public string Collection { get; }

    public Task<PaginatedResponse<Record>> ListAsync(
        ListParams? listParams = null,
        CancellationToken ct = default)
        => _records.ListAsync(Collection, listParams, ct);

    public Task<Record> GetAsync(string id, CancellationToken ct = default)
        => _records.GetAsync(Collection, id, ct);

    public Task<Record> CreateAsync(object data, CancellationToken ct = default)
        => _records.CreateAsync(Collection, data, ct);

    public Task<Record> UpdateAsync(string id, object data, CancellationToken ct = default)
        => _records.UpdateAsync(Collection, id, data, ct);

    public Task DeleteAsync(string id, CancellationToken ct = default)
        => _records.DeleteAsync(Collection, id, ct);
}
