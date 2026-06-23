using System.Text.Json.Serialization;

namespace edbot.Commands.Types;
public class SystemData
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("requirePermit")]
    public bool RequirePermit { get; set; }

    [JsonPropertyName("permitName")]
    public string PermitName { get; set; } = string.Empty;

    [JsonPropertyName("information")]
    public SystemInfo Information { get; set; } = new();

    [JsonPropertyName("primaryStar")]
    public StarInfo? PrimaryStar { get; set; }
}

public class SystemInfo
{
    [JsonPropertyName("allegiance")]
    public string Allegiance { get; set; } = string.Empty;

    [JsonPropertyName("government")]
    public string Government { get; set; } = string.Empty;

    [JsonPropertyName("faction")]
    public string Faction { get; set; } = string.Empty;

    [JsonPropertyName("factionState")]
    public string FactionState { get; set; } = string.Empty;

    [JsonPropertyName("population")]
    public long Population { get; set; }

    [JsonPropertyName("security")]
    public string Security { get; set; } = string.Empty;

    [JsonPropertyName("economy")]
    public string Economy { get; set; } = string.Empty;

    [JsonPropertyName("secondEconomy")]
    public string SecondEconomy { get; set; } = string.Empty;

    [JsonPropertyName("reserve")]
    public string Reserve { get; set; } = string.Empty;
}

public class StarInfo
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("isScoopable")]
    public bool IsScoopable { get; set; }
}