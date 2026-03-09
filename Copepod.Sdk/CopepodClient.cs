using Copepod.Sdk.Platform;

namespace Copepod.Sdk;

/// <summary>
/// Main entry point for the Copepod SDK.
/// </summary>
/// <example>
/// <code>
/// var client = new CopepodClient("http://localhost:8090");
/// var auth = await client.Auth.LoginAsync("admin@example.com", "password");
/// var orgs = await client.Orgs.ListAsync();
/// var posts = client.App("orgId", "appId").RecordCollection("posts");
/// var list = await posts.ListAsync();
/// </code>
/// </example>
public class CopepodClient
{
    private readonly CopepodHttpClient _http;

    /// <summary>
    /// Create a new SDK client pointing at the given Copepod instance.
    /// </summary>
    /// <param name="baseUrl">Base URL (e.g., "http://localhost:8090")</param>
    /// <param name="httpClient">Optional HttpClient for custom configuration</param>
    public CopepodClient(string baseUrl, HttpClient? httpClient = null)
    {
        _http = new CopepodHttpClient(baseUrl, httpClient);
        Auth = new AuthService(_http);
        Orgs = new OrgService(_http);
    }

    /// <summary>
    /// Set a pre-existing JWT bearer token for authentication.
    /// </summary>
    public void SetToken(string token) => _http.SetBearerToken(token);

    /// <summary>
    /// Set an API key for server-to-server authentication.
    /// </summary>
    public void SetApiKey(string key) => _http.SetApiKey(key);

    /// <summary>
    /// Clear any stored authentication credentials.
    /// </summary>
    public void ClearAuth() => _http.ClearAuth();

    /// <summary>Platform authentication (login, refresh, MFA).</summary>
    public AuthService Auth { get; }

    /// <summary>Organization management.</summary>
    public OrgService Orgs { get; }

    /// <summary>Bind an organization once and reuse org-scoped services.</summary>
    public ScopedOrgClient Org(string orgId) => new(_http, orgId);

    /// <summary>Bind an organization and app once and reuse app-scoped services.</summary>
    public ScopedAppClient App(string orgId, string appId) => new(_http, orgId, appId);

    /// <summary>App management within an organization.</summary>
    public AppService Apps(string orgId) => new(_http, orgId);

    /// <summary>Collection schema management.</summary>
    public CollectionService Collections(string orgId, string appId) => new(_http, orgId, appId);

    /// <summary>Record CRUD operations.</summary>
    public RecordService Records(string orgId, string appId) => new(_http, orgId, appId);

    /// <summary>File upload/download operations.</summary>
    public FileService Files(string orgId, string appId) => new(_http, orgId, appId);

    /// <summary>Deployment (PaaS) management.</summary>
    public DeploymentService Deployments(string orgId) => new(_http, orgId);

    /// <summary>Launchpad management and launch operations.</summary>
    public LaunchpadService Launchpads(string orgId) => new(_http, orgId);
}
