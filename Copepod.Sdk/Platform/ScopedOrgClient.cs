namespace Copepod.Sdk.Platform;

/// <summary>
/// Organization-scoped SDK helpers.
/// </summary>
public class ScopedOrgClient
{
    private readonly CopepodHttpClient _http;

    internal ScopedOrgClient(CopepodHttpClient http, string orgId)
    {
        _http = http;
        OrgId = orgId;
    }

    public string OrgId { get; }

    public AppService Apps => new(_http, OrgId);

    public DeploymentService Deployments => new(_http, OrgId);

    public LaunchpadService Launchpads => new(_http, OrgId);

    public ScopedAppClient App(string appId) => new(_http, OrgId, appId);
}
