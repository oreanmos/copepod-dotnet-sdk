using System.Text.Json;
using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Launchpad management and launch operations.
/// </summary>
public class LaunchpadService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;

    internal LaunchpadService(CopepodHttpClient http, string orgId)
    {
        _http = http;
        _orgId = orgId;
    }

    private string BasePath => $"/api/platform/orgs/{_orgId}/launchpads";

    public Task<List<Launchpad>> ListAsync(CancellationToken ct = default)
        => _http.GetAsync<List<Launchpad>>(BasePath, ct);

    public Task<Launchpad> GetAsync(string launchpadId, CancellationToken ct = default)
        => _http.GetAsync<Launchpad>($"{BasePath}/{launchpadId}", ct);

    public Task<Launchpad> CreateAsync(LaunchpadCreate input, CancellationToken ct = default)
        => _http.PostAsync<Launchpad>(BasePath, input, ct);

    public Task<Launchpad> UpdateAsync(string launchpadId, LaunchpadUpdate input, CancellationToken ct = default)
        => _http.PatchAsync<Launchpad>($"{BasePath}/{launchpadId}", input, ct);

    public Task DeleteAsync(string launchpadId, CancellationToken ct = default)
        => _http.DeleteAsync($"{BasePath}/{launchpadId}", ct);

    public Task<Launchpad> PublishAsync(string launchpadId, CancellationToken ct = default)
        => _http.PostAsync<Launchpad>($"{BasePath}/{launchpadId}/publish", ct: ct);

    public Task<SourceDetectionResult> DetectSourceAsync(
        string launchpadId,
        LaunchpadLaunchRequest input,
        CancellationToken ct = default)
        => _http.PostAsync<SourceDetectionResult>($"{BasePath}/{launchpadId}/detect-source", input, ct);

    public Task<LaunchpadLaunchResponse> LaunchAsync(
        string launchpadId,
        LaunchpadLaunchRequest input,
        CancellationToken ct = default)
        => _http.PostAsync<LaunchpadLaunchResponse>($"{BasePath}/{launchpadId}/launch", input, ct);
}
