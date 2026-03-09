namespace Copepod.Sdk.Platform;

/// <summary>
/// App-scoped SDK helpers.
/// </summary>
public class ScopedAppClient
{
    private readonly CopepodHttpClient _http;

    internal ScopedAppClient(CopepodHttpClient http, string orgId, string appId)
    {
        _http = http;
        OrgId = orgId;
        AppId = appId;
    }

    public string OrgId { get; }

    public string AppId { get; }

    public CollectionService Collections => new(_http, OrgId, AppId);

    public FileService Files => new(_http, OrgId, AppId);

    public RecordService Records => new(_http, OrgId, AppId);

    public ScopedRecordCollectionService RecordCollection(string collection)
        => new(_http, OrgId, AppId, collection);

    public AppAuthService Auth(string collection) => new(_http, OrgId, AppId, collection);
}
