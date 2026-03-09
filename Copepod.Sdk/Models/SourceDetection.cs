using System.Text.Json.Serialization;

namespace Copepod.Sdk.Models;

public class SourceDetectionResult
{
    [JsonPropertyName("framework")]
    public string? Framework { get; set; }

    [JsonPropertyName("port")]
    public int? Port { get; set; }

    [JsonPropertyName("health_check_mode")]
    public string? HealthCheckMode { get; set; }

    [JsonPropertyName("memory_request")]
    public string? MemoryRequest { get; set; }

    [JsonPropertyName("memory_limit")]
    public string? MemoryLimit { get; set; }

    [JsonPropertyName("suggested_env_vars")]
    public List<SuggestedEnvVar> SuggestedEnvVars { get; set; } = [];
}

public class SuggestedEnvVar
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = "";

    [JsonPropertyName("example")]
    public string Example { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";
}
