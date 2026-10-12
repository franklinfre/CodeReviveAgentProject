using System.Text.Json;
using PRReviewAgent.Services;
using PRReviewAgent.Agents;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<OllamaService>(client =>
{
  client.BaseAddress = new Uri("http://192.168.1.6:11434/");
});

builder.Services.AddHttpClient<GitHubService>(client =>
{
  client.BaseAddress = new Uri("https://api.github.com/");
});

builder.Services.AddScoped<PRAgent>();
builder.Services.AddScoped<DiffAnalyzerService>();

var app = builder.Build();

app.MapControllers();

app.Run();

