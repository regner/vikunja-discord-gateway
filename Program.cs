using Microsoft.Extensions.Options;
using VikunjaDiscordGateway;
using VikunjaDiscordGateway.Models;
using VikunjaDiscordGateway.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<DiscordService>();
builder.Services.Configure<Config>(builder.Configuration.GetSection("VikunjaDiscordGateway"));

WebApplication app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var config = app.Services.GetRequiredService<IOptions<Config>>().Value;

if (config.Projects == null)
{
    throw new InvalidOperationException("Configuration Error: Must supply at least one project.");
}

foreach (KeyValuePair<int, ProjectConfig> project in config.Projects)
{
    if (!config.Webhooks.ContainsKey(project.Value.Webhook))
    {
        throw new InvalidOperationException(
            $"Configuration Error: Project '{project.Value.Name}' (ID: {project.Key}) " +
            $"references WebhookIdentifier '{project.Value.Webhook}', which does not exist in WebhookUrls."
        );
    }
}

logger.LogInformation("Application Starting...");
logger.LogInformation("Configuration information:");
logger.LogInformation("DryRun: {DryRun}", config.DryRun);
logger.LogInformation("VikunjaUrl: {VikunjaUrl}", config.VikunjaUrl);
logger.LogInformation("Configured Webhooks: {WebhookIdentifiers}", string.Join(", ", config.Webhooks.Keys));
logger.LogInformation("Configured Projects: {ProjectNames}", string.Join(", ", config.Projects.Values.Select(p => p.Name)));


app.MapPost("/webhook", HandleWebhook);

app.Run();

static async Task<IResult> HandleWebhook(
    IOptions<Config> configOptions,
    VikunjaWebhook payload,
    DiscordService discordService
)
{
    Config config = configOptions.Value;
    int projectId = payload.Data.Task.ProjectId;
    
    if (!config.Projects.ContainsKey(projectId))
    {
        return Results.BadRequest(new { message = $"Unable to find a config for project ID {projectId}" });
    }
    
    switch (payload.EventName)
    {
        case "task.created":
            await discordService.SendWebhook("New Task Created", payload.Data, "0x195919");
            break;
        case "task.deleted":
            break;
        case "task.assignee.created":
            break;
        case "task.assignee.deleted":
            break;
        default:
            break;
    }
    
    return Results.Ok(new { message = $"Webhook {payload.EventName} received successfully" });
}
