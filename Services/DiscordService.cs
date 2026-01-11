using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using VikunjaDiscordGateway.Models;

namespace VikunjaDiscordGateway.Services;

public class DiscordService(HttpClient httpClient, IOptions<Config> config, ILogger<DiscordService> logger)
{
    private readonly Config _config = config.Value;
    
    public async Task SendWebhook(string eventTitle, VikunjaWebhookData payload, string color)
    {
        string projectName = _config.Projects[payload.Task.ProjectId].Name;
        string webhookIdentifier = _config.Projects[payload.Task.ProjectId].Webhook;
        string webhookUrl = _config.Webhooks[webhookIdentifier];

        string description =
            $"**Task:** {payload.Task.Title}"
            + $"\n**Project:** {projectName}"
            + $"\n**Created By:** {payload.Doer.Name}";

        if (_config.VikunjaUrl != null)
        {
            Uri vikunjaUrl = new Uri(_config.VikunjaUrl, $"tasks/{payload.Task.Id.ToString()}");

            description += $"\n\n[View Task]({vikunjaUrl})";
        }
        
        
        
        DiscordEmbed embed = new DiscordEmbed
        {
            Title = eventTitle,
            Color = "195919",
            Description = description,
        };

        DiscordMessage message = new DiscordMessage
        {
            Embeds = [embed]
        };
        
        StringContent serializedMessage = new StringContent(
            JsonSerializer.Serialize(message),
            Encoding.UTF8,
            "application/json"
        );

        if (_config.DryRun)
        {
            logger.LogInformation("Dry run enabled, will not actually send Discord webhook.");
            logger.LogInformation("Discord message: {message}", JsonSerializer.Serialize(message));
            return;
        }
        
        await httpClient.PostAsync(webhookUrl, serializedMessage);
    }
}