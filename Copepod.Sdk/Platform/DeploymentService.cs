using System.Text.Json;
using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Deployment (PaaS) management operations.
/// </summary>
public class DeploymentService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;

    internal DeploymentService(CopepodHttpClient http, string orgId)
    {
        _http = http;
        _orgId = orgId;
    }

    private string BasePath => $"/api/platform/orgs/{_orgId}/deployments";

    public Task<List<Deployment>> ListAsync(CancellationToken ct = default)
        => _http.GetAsync<List<Deployment>>(BasePath, ct);

    public Task<Deployment> CreateAsync(DeploymentCreate input, CancellationToken ct = default)
        => _http.PostAsync<Deployment>(BasePath, input, ct);

    public Task<Deployment> GetAsync(string deployId, CancellationToken ct = default)
        => _http.GetAsync<Deployment>($"{BasePath}/{deployId}", ct);

    public Task DeleteAsync(string deployId, CancellationToken ct = default)
        => _http.DeleteAsync($"{BasePath}/{deployId}", ct);

    /// <summary>Trigger a deploy action.</summary>
    public Task<JsonElement> DeployAsync(string deployId, CancellationToken ct = default)
        => _http.PostAsync<JsonElement>($"{BasePath}/{deployId}/deploy", ct: ct);

    /// <summary>Stop a running deployment.</summary>
    public Task<JsonElement> StopAsync(string deployId, CancellationToken ct = default)
        => _http.PostAsync<JsonElement>($"{BasePath}/{deployId}/stop", ct: ct);

    /// <summary>Start a stopped deployment.</summary>
    public Task<JsonElement> StartAsync(string deployId, CancellationToken ct = default)
        => _http.PostAsync<JsonElement>($"{BasePath}/{deployId}/start", ct: ct);

    /// <summary>Get deployment status.</summary>
    public Task<JsonElement> StatusAsync(string deployId, CancellationToken ct = default)
        => _http.GetAsync<JsonElement>($"{BasePath}/{deployId}/status", ct);

    /// <summary>Fetch deployment logs.</summary>
    public Task<JsonElement> LogsAsync(string deployId, CancellationToken ct = default)
        => _http.GetAsync<JsonElement>($"{BasePath}/{deployId}/logs", ct);

    /// <summary>List domains for a deployment.</summary>
    public Task<List<DeploymentDomain>> ListDomainsAsync(string deployId, CancellationToken ct = default)
        => _http.GetAsync<List<DeploymentDomain>>($"{BasePath}/{deployId}/domains", ct);

    /// <summary>Add a custom domain to a deployment.</summary>
    public Task<DeploymentDomain> AddDomainAsync(string deployId, string domain, CancellationToken ct = default)
        => _http.PostAsync<DeploymentDomain>(
            $"{BasePath}/{deployId}/domains",
            new { domain },
            ct);

    /// <summary>Remove a domain from a deployment.</summary>
    public Task RemoveDomainAsync(string deployId, string domainId, CancellationToken ct = default)
        => _http.DeleteAsync($"{BasePath}/{deployId}/domains/{domainId}", ct);

    /// <summary>Get environment variables.</summary>
    public Task<JsonElement> GetEnvAsync(string deployId, CancellationToken ct = default)
        => _http.GetAsync<JsonElement>($"{BasePath}/{deployId}/env", ct);

    /// <summary>Set environment variables.</summary>
    public Task<JsonElement> SetEnvAsync(string deployId, object env, CancellationToken ct = default)
        => _http.PutAsync<JsonElement>($"{BasePath}/{deployId}/env", env, ct);

    /// <summary>Detect runtime hints from a deployment's configured git source.</summary>
    public Task<SourceDetectionResult> DetectSourceAsync(string deployId, CancellationToken ct = default)
        => _http.PostAsync<SourceDetectionResult>($"{BasePath}/{deployId}/detect", ct: ct);
}
