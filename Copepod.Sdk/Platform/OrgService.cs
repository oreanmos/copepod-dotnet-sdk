using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Organization management operations.
/// </summary>
public class OrgService
{
    private readonly CopepodHttpClient _http;

    internal OrgService(CopepodHttpClient http) => _http = http;

    public Task<List<Org>> ListAsync(CancellationToken ct = default)
        => _http.GetAsync<List<Org>>("/api/platform/orgs", ct);

    public Task<Org> CreateAsync(OrgCreate input, CancellationToken ct = default)
        => _http.PostAsync<Org>("/api/platform/orgs", input, ct);

    public Task<Org> GetAsync(string orgId, CancellationToken ct = default)
        => _http.GetAsync<Org>($"/api/platform/orgs/{orgId}", ct);

    public Task<Org> UpdateAsync(string orgId, OrgUpdate input, CancellationToken ct = default)
        => _http.PatchAsync<Org>($"/api/platform/orgs/{orgId}", input, ct);

    public Task DeleteAsync(string orgId, CancellationToken ct = default)
        => _http.DeleteAsync($"/api/platform/orgs/{orgId}", ct);

    public Task<List<OrgMember>> ListMembersAsync(string orgId, CancellationToken ct = default)
        => _http.GetAsync<List<OrgMember>>($"/api/platform/orgs/{orgId}/members", ct);

    public Task<OrgMember> AddMemberAsync(string orgId, string userId, string role, CancellationToken ct = default)
        => _http.PostAsync<OrgMember>(
            $"/api/platform/orgs/{orgId}/members",
            new { user_id = userId, role },
            ct);

    public Task<OrgMember> UpdateMemberAsync(string orgId, string userId, string role, CancellationToken ct = default)
        => _http.PatchAsync<OrgMember>(
            $"/api/platform/orgs/{orgId}/members/{userId}",
            new { role },
            ct);

    public Task RemoveMemberAsync(string orgId, string userId, CancellationToken ct = default)
        => _http.DeleteAsync($"/api/platform/orgs/{orgId}/members/{userId}", ct);
}
