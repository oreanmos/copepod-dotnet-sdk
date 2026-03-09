using System.Text.Json.Serialization;

namespace Copepod.Sdk.Models;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}

public class AuthResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = "";

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = "";

    [JsonPropertyName("user")]
    public User? User { get; set; }

    [JsonPropertyName("mfa_required")]
    public bool? MfaRequired { get; set; }

    [JsonPropertyName("mfa_token")]
    public string? MfaToken { get; set; }
}

public class RefreshRequest
{
    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; set; }
}

public class MfaVerifyRequest
{
    [JsonPropertyName("mfa_token")]
    public required string MfaToken { get; set; }

    [JsonPropertyName("code")]
    public required string Code { get; set; }
}

public class User
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = "";
}

public class AppLoginRequest
{
    [JsonPropertyName("identity")]
    public required string Identity { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}

public class AppAuthResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = "";

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = "";

    [JsonPropertyName("record")]
    public Record? Record { get; set; }

    [JsonPropertyName("mfa_required")]
    public bool? MfaRequired { get; set; }

    [JsonPropertyName("mfa_token")]
    public string? MfaToken { get; set; }
}

public class AppRegisterRequest
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class AppMfaEnrollResponse
{
    [JsonPropertyName("qr_svg")]
    public string QrSvg { get; set; } = "";

    [JsonPropertyName("totp_uri")]
    public string TotpUri { get; set; } = "";

    [JsonPropertyName("recovery_codes")]
    public List<string> RecoveryCodes { get; set; } = [];
}
