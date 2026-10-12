using PRReviewAgent.Services;

namespace PRReviewAgent.Agents;

public class PRAgent
{
  private readonly OllamaService _ollama;

  public PRAgent(OllamaService ollama)
  {
    _ollama = ollama;
  }

  public async Task<string> ReviewAsync(string title, string description, string codeChanges)
  {
    var prompt = $""" 
    You are a senior .Net code reviewer.

    Analyze this pull request information.
    Identify potential bugs, security issues,
    performance problems, and missing validation.

    PR title: 
    {title}
    PR description:
     {description}
    Code Changes:
    {codeChanges}

    Find:
    - Bugs
    - Security issues
    - Performance problems
    - .NET best practices violations

    Give actionable comments.
    """;

    return await _ollama.GenerateReviewAsync(prompt);
  }
}