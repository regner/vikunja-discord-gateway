namespace VikunjaDiscordGateway;

public record Config
{
    public bool DryRun { get; set; }
    
    public Uri? VikunjaUrl { get; set; }
    
    public Dictionary<string, string> Webhooks { get; set; }

    public Dictionary<int, ProjectConfig> Projects { get; set; }
}

public record ProjectConfig
{
    public string Name { get; set; }
    public string Webhook { get; set; }
}
