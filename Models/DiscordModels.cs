using System.Text.Json.Serialization;

namespace VikunjaDiscordGateway.Models;

public class DiscordMessage
{
    [JsonPropertyName("embeds")]
    public List<DiscordEmbed> Embeds { get; set; }
}

public class DiscordEmbed
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("color")]
    public string Color { get; set; }
}
