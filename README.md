# Vikunja Discord Gateway

This is a super simple service that takes [Vikunja](https://github.com/go-vikunja/vikunja)
webhook events and sends them to a Discord webhook.

## Deployment

### Docker

```bash
docker run -d \
  -e VIKUNJADISCORDGATEWAY__DRYRUN=false \
  -e VIKUNJADISCORDGATEWAY__VIKUNJAURL=http://localhost \
  -e VIKUNJADISCORDGATEWAY__WEBHOOKS__DEFAULT=https://discord.com/ \
  -e VIKUNJADISCORDGATEWAY__PROJECTS__1__NAME=MyProject \
  -e VIKUNJADISCORDGATEWAY__PROJECTS__1__WEBHOOK=Default \
  -p 8080:8080 \
  ghcr.io/regner/vikunja-discord-gateway:latest
```

### Docker Compose

```yaml
---

services:
  vikunja-discord-gateway:
    image: "ghcr.io/regner/vikunja-discord-gateway:latest"
    restart: "unless-stopped"
    environment:
      VIKUNJADISCORDGATEWAY__DRYRUN: "false"
      VIKUNJADISCORDGATEWAY__VIKUNJAURL: "http://localhost"
      VIKUNJADISCORDGATEWAY__WEBHOOKS__DEFAULT: "https://discord.com/"
      VIKUNJADISCORDGATEWAY__PROJECTS__1__NAME: "MyProject"
      VIKUNJADISCORDGATEWAY__PROJECTS__1__WEBHOOK: "Default"
```

## Configuration

Configuration can be done via environment variables or appsettings.json.

### Environment Variables

```
VIKUNJADISCORDGATEWAY__DRYRUN: true
VIKUNJADISCORDGATEWAY__VIKUNJAURL: "http://localhost"
VIKUNJADISCORDGATEWAY__WEBHOOKS__DEFAULT: "https://discord.com/"
VIKUNJADISCORDGATEWAY__PROJECTS__1__NAME: "My Project"
VIKUNJADISCORDGATEWAY__PROJECTS__1__WEBHOOK: "Default"
```

### Configuration File

```json
{
  "VikunjaDiscordGateway": {
    "DryRun": true,
    "VikunjaUrl": "http://localhost",
    "Webhooks": {
      "TestWebhook": "Empty"
    },
    "Projects": {
      "1": {
        "Name": "TestProject",
        "Webhook": "TestWebhook"
      },
      "2": {
        "Name": "OtherProject",
        "Webhook": "TestWebhook"
      }
    }
  }
}

```

## Development

### Webhook Structure

- See list of registered webhooks [here](https://github.com/go-vikunja/vikunja/blob/main/pkg/models/listeners.go#L79)
- Task structure [here](https://github.com/go-vikunja/vikunja/blob/main/pkg/models/tasks.go#L53)
- User structure [here](https://github.com/go-vikunja/vikunja/blob/main/pkg/user/user.go#L76)
- Events [here](https://github.com/go-vikunja/vikunja/blob/main/pkg/models/events.go)
