using Copepod.Sdk.Models;

namespace Copepod.Sdk.Platform;

/// <summary>
/// App-user authentication operations bound to a specific app auth collection.
/// </summary>
public class AppAuthService
{
    private readonly CopepodHttpClient _http;
    private readonly string _orgId;
    private readonly string _appId;
    private readonly string _collection;

    internal AppAuthService(CopepodHttpClient http, string orgId, string appId, string collection)
    {
        _http = http;
        _orgId = orgId;
        _appId = appId;
        _collection = collection;
    }

    private string BasePath => $"/api/platform/orgs/{_orgId}/apps/{_appId}/auth/{_collection}";

    public Task<AppAuthResponse> LoginAsync(string identity, string password, CancellationToken ct = default)
        => _http.PostAsync<AppAuthResponse>(
            $"{BasePath}/auth-with-password",
            new AppLoginRequest { Identity = identity, Password = password },
            ct);

    public Task<AppAuthResponse> RegisterAsync(AppRegisterRequest input, CancellationToken ct = default)
        => _http.PostAsync<AppAuthResponse>($"{BasePath}/register", input, ct);

    public Task<AppAuthResponse> RefreshAsync(string refreshToken, CancellationToken ct = default)
        => _http.PostAsync<AppAuthResponse>(
            $"{BasePath}/auth-refresh",
            new RefreshRequest { RefreshToken = refreshToken },
            ct);

    public Task RequestVerificationAsync(string email, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/request-verification", new { email }, ct);

    public Task ConfirmVerificationAsync(string token, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/confirm-verification", new { token }, ct);

    public Task RequestPasswordResetAsync(string email, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/request-password-reset", new { email }, ct);

    public Task ConfirmPasswordResetAsync(
        string token,
        string password,
        CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/confirm-password-reset", new { token, password }, ct);

    public Task RequestEmailChangeAsync(string newEmail, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/request-email-change", new { new_email = newEmail }, ct);

    public Task ConfirmEmailChangeAsync(string token, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/confirm-email-change", new { token }, ct);

    public Task<AppMfaEnrollResponse> MfaEnrollAsync(CancellationToken ct = default)
        => _http.PostAsync<AppMfaEnrollResponse>($"{BasePath}/mfa/enroll", new { }, ct);

    public Task MfaConfirmEnrollAsync(string code, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/mfa/confirm-enroll", new { code }, ct);

    public Task MfaDisableAsync(string code, CancellationToken ct = default)
        => _http.PostAsync($"{BasePath}/mfa/disable", new { code }, ct);

    public Task<AppAuthResponse> MfaVerifyAsync(
        string mfaToken,
        string code,
        CancellationToken ct = default)
        => _http.PostAsync<AppAuthResponse>(
            $"{BasePath}/mfa/verify",
            new MfaVerifyRequest { MfaToken = mfaToken, Code = code },
            ct);

    public Task<AppAuthResponse> MfaRecoveryAsync(
        string mfaToken,
        string recoveryCode,
        CancellationToken ct = default)
        => _http.PostAsync<AppAuthResponse>(
            $"{BasePath}/mfa/recovery",
            new { mfa_token = mfaToken, recovery_code = recoveryCode },
            ct);
}
