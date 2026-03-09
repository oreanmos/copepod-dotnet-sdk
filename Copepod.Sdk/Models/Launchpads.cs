using System.Text.Json.Serialization;

namespace Copepod.Sdk.Models;

public class Launchpad
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("org_id")]
    public string OrgId { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("draft_definition")]
    public LaunchpadDefinition DraftDefinition { get; set; } = new();

    [JsonPropertyName("published_definition")]
    public LaunchpadDefinition? PublishedDefinition { get; set; }

    [JsonPropertyName("published_at")]
    public string? PublishedAt { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = "";
}

public class LaunchpadCreate
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("slug")]
    public required string Slug { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("definition")]
    public required LaunchpadDefinition Definition { get; set; }
}

public class LaunchpadUpdate
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("definition")]
    public LaunchpadDefinition? Definition { get; set; }
}

public class LaunchpadDefinition
{
    [JsonPropertyName("headline")]
    public string Headline { get; set; } = "";

    [JsonPropertyName("launch_button_label")]
    public string LaunchButtonLabel { get; set; } = "";

    [JsonPropertyName("create_app")]
    public bool CreateApp { get; set; }

    [JsonPropertyName("app_defaults")]
    public LaunchpadAppDefaults AppDefaults { get; set; } = new();

    [JsonPropertyName("deployment_defaults")]
    public LaunchpadDeploymentDefaults DeploymentDefaults { get; set; } = new();

    [JsonPropertyName("source_defaults")]
    public LaunchpadSourceDefaults SourceDefaults { get; set; } = new();

    [JsonPropertyName("domain_defaults")]
    public LaunchpadDomainDefaults? DomainDefaults { get; set; }

    [JsonPropertyName("static_env")]
    public List<LaunchpadStaticEnvVar> StaticEnv { get; set; } = [];

    [JsonPropertyName("fields")]
    public List<LaunchpadField> Fields { get; set; } = [];

    [JsonPropertyName("hook_kind")]
    public string? HookKind { get; set; }
}

public class LaunchpadAppDefaults
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = "";

    [JsonPropertyName("shard_mode")]
    public string ShardMode { get; set; } = "";
}

public class LaunchpadDeploymentDefaults
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = "";

    [JsonPropertyName("port")]
    public int Port { get; set; } = 8080;

    [JsonPropertyName("replicas")]
    public int Replicas { get; set; } = 1;

    [JsonPropertyName("placement_preset")]
    public string PlacementPreset { get; set; } = "single-node";

    [JsonPropertyName("min_distinct_nodes")]
    public int MinDistinctNodes { get; set; } = 1;

    [JsonPropertyName("node_selector")]
    public string NodeSelector { get; set; } = "";
}

public class LaunchpadSourceDefaults
{
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = "image";

    [JsonPropertyName("image")]
    public string Image { get; set; } = "";

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "latest";

    [JsonPropertyName("git_provider")]
    public string GitProvider { get; set; } = "github";

    [JsonPropertyName("git_repo_url")]
    public string GitRepoUrl { get; set; } = "";

    [JsonPropertyName("git_branch")]
    public string GitBranch { get; set; } = "main";

    [JsonPropertyName("git_auth_method")]
    public string GitAuthMethod { get; set; } = "none";

    [JsonPropertyName("auto_deploy")]
    public bool AutoDeploy { get; set; } = true;
}

public class LaunchpadDomainDefaults
{
    [JsonPropertyName("host")]
    public string Host { get; set; } = "";

    [JsonPropertyName("path")]
    public string Path { get; set; } = "/";
}

public class LaunchpadStaticEnvVar
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = "";

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";

    [JsonPropertyName("is_secret")]
    public bool IsSecret { get; set; }
}

public class LaunchpadField
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = "";

    [JsonPropertyName("label")]
    public string Label { get; set; } = "";

    [JsonPropertyName("help_text")]
    public string HelpText { get; set; } = "";

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("default_value")]
    public string DefaultValue { get; set; } = "";

    [JsonPropertyName("field_type")]
    public string FieldType { get; set; } = "text";

    [JsonPropertyName("options")]
    public List<LaunchpadFieldOption> Options { get; set; } = [];

    [JsonPropertyName("binding")]
    public LaunchpadFieldBinding Binding { get; set; } = new();
}

public class LaunchpadFieldOption
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = "";

    [JsonPropertyName("label")]
    public string Label { get; set; } = "";
}

public class LaunchpadFieldBinding
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; } = "";

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("secret")]
    public bool? Secret { get; set; }
}

public class LaunchpadLaunchRequest
{
    [JsonPropertyName("values")]
    public Dictionary<string, string> Values { get; set; } = [];
}

public class LaunchpadLaunchResponse
{
    [JsonPropertyName("launchpad_id")]
    public string LaunchpadId { get; set; } = "";

    [JsonPropertyName("launchpad_version")]
    public int LaunchpadVersion { get; set; }

    [JsonPropertyName("deployment_id")]
    public string DeploymentId { get; set; } = "";

    [JsonPropertyName("app_id")]
    public string? AppId { get; set; }

    [JsonPropertyName("log_id")]
    public string? LogId { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; } = "";
}
