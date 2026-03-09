using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// Platform authentication operations (login, refresh, MFA, OAuth).
/// </summary>
public class AuthService
{
    private readonly CopepodHttpClient _http;

    internal AuthService(CopepodHttpClient http) => _http = http;

    /// <summary>
    /// Login with email and password. On success, the token is stored automatically.
    /// </summary>
    public async Task<AuthResponse> LoginAsync(string email, string password, CancellationToken ct = default)
    {
        var resp = await _http.PostAsync<AuthResponse>(
            "/api/platform/auth/login",
            new LoginRequest { Email = email, Password = password },
            ct);

        if (!string.IsNullOrEmpty(resp.Token))
            _http.SetBearerToken(resp.Token);

        return resp;
    }

    /// <summary>
    /// Refresh the access token using a refresh token.
    /// </summary>
    public async Task<AuthResponse> RefreshAsync(string refreshToken, CancellationToken ct = default)
    {
        var resp = await _http.PostAsync<AuthResponse>(
            "/api/platform/auth/refresh",
            new RefreshRequest { RefreshToken = refreshToken },
            ct);

        if (!string.IsNullOrEmpty(resp.Token))
            _http.SetBearerToken(resp.Token);

        return resp;
    }

    /// <summary>
    /// Get the currently authenticated user.
    /// </summary>
    public Task<User> MeAsync(CancellationToken ct = default)
        => _http.GetAsync<User>("/api/platform/auth/me", ct);

    /// <summary>
    /// Verify MFA code after login returns mfa_required.
    /// </summary>
    public async Task<AuthResponse> MfaVerifyAsync(string mfaToken, string code, CancellationToken ct = default)
    {
        var resp = await _http.PostAsync<AuthResponse>(
            "/api/platform/auth/mfa/verify",
            new MfaVerifyRequest { MfaToken = mfaToken, Code = code },
            ct);

        if (!string.IsNullOrEmpty(resp.Token))
            _http.SetBearerToken(resp.Token);

        return resp;
    }

    /// <summary>
    /// Logout and clear stored credentials.
    /// </summary>
    public async Task LogoutAsync(CancellationToken ct = default)
    {
        await _http.PostAsync("/api/platform/auth/logout", ct: ct);
        _http.ClearAuth();
    }
}
