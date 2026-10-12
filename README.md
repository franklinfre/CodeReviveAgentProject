# AI Pull Request Review Agent

An AI-powered Pull Request (PR) Review Agent built with **ASP.NET Core**, **GitHub Webhooks**, and **Ollama**. The agent automatically reviews GitHub pull requests using a locally hosted Large Language Model (LLM) and provides feedback on code quality, potential bugs, security concerns, and best practices.

## Features

- Receives GitHub Pull Request webhook events
- Fetches changed files from the GitHub REST API
- Converts GitHub diffs into an AI-friendly format
- Uses a local Ollama model (`qwen2.5-coder:7b`) for code review
- Returns AI-generated review findings
- Designed with a clean, extensible architecture

## Tech Stack

- ASP.NET Core (.NET)
- C#
- GitHub Webhooks
- GitHub REST API
- Ollama
- qwen2.5-coder:7b
- HttpClientFactory
- Dependency Injection

## Project Structure

```text
PRReviewAgent
│
├── Agents
│   └── PRReviewAgent.cs
│
├── Controllers
│   └── GitHubWebhookController.cs
│
├── Models
│   └── PullRequestEvent.cs
│
├── Services
│   ├── DiffAnalyzerService.cs
│   ├── GitHubService.cs
│   └── OllamaService.cs
│
├── Program.cs
└── appsettings.json
```

## Architecture

```text
GitHub Pull Request
        │
        ▼
GitHub Webhook
        │
        ▼
GitHubWebhookController
        │
        ▼
GitHubService
        │
        ▼
GitHub REST API
        │
        ▼
DiffAnalyzerService
        │
        ▼
PRReviewAgent
        │
        ▼
OllamaService
        │
        ▼
Ollama (qwen2.5-coder:7b)
        │
        ▼
AI Review Result
```

## Prerequisites

- .NET 8 or later
- Ollama installed and running
- `qwen2.5-coder:7b` model downloaded
- GitHub Personal Access Token (PAT)
- GitHub repository with Pull Requests

## Configuration

### 1. Configure GitHub Token

Update `appsettings.json`:

```json
{
  "GitHub": {
    "Token": "YOUR_GITHUB_PERSONAL_ACCESS_TOKEN"
  }
}
```

> **Note:** For production, use User Secrets or environment variables instead of storing the token in `appsettings.json`.

### 2. Configure Ollama

Ensure Ollama is running.

Example:

```bash
ollama serve
```

Update the Ollama base address if it is hosted on another machine.

```csharp
http://<OLLAMA_HOST>:11434/
```

## Running the Project

```bash
dotnet restore
dotnet run
```

The application starts the webhook endpoint.

Example:

```
POST /webhook
```

## Sample Webhook Payload

```json
{
  "action": "opened",
  "repository": {
    "full_name": "username/repository"
  },
  "pull_request": {
    "number": 1,
    "title": "Add Employee API",
    "body": "Added employee creation endpoint."
  }
}
```

## Current Workflow

1. Receive GitHub Pull Request webhook.
2. Extract repository and PR information.
3. Fetch changed files from the GitHub API.
4. Convert GitHub diff into a readable review format.
5. Generate an AI prompt.
6. Send the prompt to Ollama.
7. Return AI-generated review suggestions.

## Planned Enhancements

- Verify GitHub webhook signatures
- Post AI review comments directly to GitHub Pull Requests
- Inline review comments on specific lines
- Review only modified files
- Support multiple Ollama models
- Prompt templates
- Semantic code chunking for large pull requests
- Retry and error handling
- Docker support
- CI/CD pipeline
- Configuration using User Secrets
- Structured logging and monitoring

## Learning Objectives

This project demonstrates:

- ASP.NET Core Web API
- Clean Architecture principles
- Dependency Injection
- HttpClientFactory
- GitHub REST API integration
- GitHub Webhooks
- AI Agent design
- Local LLM integration using Ollama
- Prompt Engineering
- JSON serialization and deserialization

## Future Roadmap

- Multi-agent review architecture
- Security-focused review agent
- Performance review agent
- Unit test suggestion agent
- Automatic code fix generation
- Support for Azure OpenAI and OpenAI models
- GitHub Action integration

## License

This project is intended for learning and experimentation. Feel free to fork, modify, and extend it.
