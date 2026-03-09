using System.Text.Json;
using System.Text.Json.Serialization;

namespace Copepod.Sdk.Models;

public class Org
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = "";

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = "";
}

public class OrgCreate
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("slug")]
    public required string Slug { get; set; }
}

public class OrgUpdate
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class OrgMember
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = "";

    [JsonPropertyName("org_id")]
    public string OrgId { get; set; } = "";

    [JsonPropertyName("role")]
    public string Role { get; set; } = "";

    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";
}

public class App
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("org_id")]
    public string OrgId { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = "";

    [JsonPropertyName("schema_version")]
    public int SchemaVersion { get; set; }

    [JsonPropertyName("shard_mode")]
    public string ShardMode { get; set; } = "";

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = "";
}

public class AppCreate
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("slug")]
    public required string Slug { get; set; }

    [JsonPropertyName("shard_mode")]
    public string? ShardMode { get; set; }
}

public class Collection
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("app_id")]
    public string AppId { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public string CollectionType { get; set; } = "";

    [JsonPropertyName("schema")]
    public List<Field> Schema { get; set; } = [];

    [JsonPropertyName("indexes")]
    public List<string> Indexes { get; set; } = [];

    [JsonPropertyName("rules")]
    public CollectionRules? Rules { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = "";
}

public class Field
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required string FieldType { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("unique")]
    public bool Unique { get; set; }

    [JsonPropertyName("options")]
    public JsonElement? Options { get; set; }
}

public class CollectionRules
{
    [JsonPropertyName("list")]
    public string? List { get; set; }

    [JsonPropertyName("view")]
    public string? View { get; set; }

    [JsonPropertyName("create")]
    public string? Create { get; set; }

    [JsonPropertyName("update")]
    public string? Update { get; set; }

    [JsonPropertyName("delete")]
    public string? Delete { get; set; }
}

public class CollectionCreate
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required string CollectionType { get; set; }

    [JsonPropertyName("schema")]
    public required List<Field> Schema { get; set; }

    [JsonPropertyName("rules")]
    public CollectionRules? Rules { get; set; }
}

public class Record
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Data { get; set; }
}

public class PaginatedResponse<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; } = [];

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }

    [JsonPropertyName("total_items")]
    public int TotalItems { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}

public class ListParams
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? Sort { get; set; }
    public string? Filter { get; set; }
    public string? Fields { get; set; }
    public string? Expand { get; set; }

    internal string ToQueryString()
    {
        var parts = new List<string>();
        if (Page.HasValue) parts.Add($"page={Page}");
        if (PerPage.HasValue) parts.Add($"per_page={PerPage}");
        if (Sort != null) parts.Add($"sort={Uri.EscapeDataString(Sort)}");
        if (Filter != null) parts.Add($"filter={Uri.EscapeDataString(Filter)}");
        if (Fields != null) parts.Add($"fields={Uri.EscapeDataString(Fields)}");
        if (Expand != null) parts.Add($"expand={Uri.EscapeDataString(Expand)}");
        return parts.Count > 0 ? "?" + string.Join("&", parts) : "";
    }
}

public class Deployment
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("org_id")]
    public string OrgId { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = "";

    [JsonPropertyName("image")]
    public string Image { get; set; } = "";

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = "";

    [JsonPropertyName("replicas")]
    public int Replicas { get; set; }

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    [JsonPropertyName("last_deployed_at")]
    public string? LastDeployedAt { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("updated")]
    public string Updated { get; set; } = "";
}

public class DeploymentCreate
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("slug")]
    public required string Slug { get; set; }

    [JsonPropertyName("image")]
    public required string Image { get; set; }

    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("replicas")]
    public int? Replicas { get; set; }

    [JsonPropertyName("port")]
    public int? Port { get; set; }
}

public class DeploymentDomain
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = "";

    [JsonPropertyName("ssl_status")]
    public string SslStatus { get; set; } = "";

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";
}

public class ApiKey
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("app_id")]
    public string AppId { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("key_prefix")]
    public string KeyPrefix { get; set; } = "";

    [JsonPropertyName("scopes")]
    public List<string> Scopes { get; set; } = [];

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";
}
