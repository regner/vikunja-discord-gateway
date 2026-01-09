using System.Text.Json.Serialization;

namespace VikunjaDiscordGateway.Models;

public class VikunjaWebhook
{
    [JsonPropertyName("event_name")]
    public string EventName { get; set; }
    
    [JsonPropertyName("data")]
    public VikunjaWebhookData Data { get; set; }
}

public class VikunjaWebhookData
{
    [JsonPropertyName("task")]
    public VikunjaWebhookTask Task { get; set; }
    
    [JsonPropertyName("doer")]
    public VikunjaWebhookDoer Doer { get; set; }
}

public class VikunjaWebhookTask
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("project_id")]
    public int ProjectId { get; set; }
}

public class VikunjaWebhookDoer
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}