using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace PRReviewAgent.Services;

public class DiffAnalyzerService
{
  public string ConvertToReviewInput(string githubResponse)
  {
    var files = 
    JsonSerializer.Deserialize<List<GitHubFile>>(githubResponse);

    if(files == null || files.Count == 0)
    {
      return "No code changes found.";
    }

    var result = new StringBuilder();

    foreach(var file in files)
    {
      result.AppendLine($"File: {file.FileName}");

      result.AppendLine($"File: {file.Status}");
      result.AppendLine($"Changes:");

      result.AppendLine(file.Patch ?? "No patch available");

      result.AppendLine("_____________________");
    }
    return result.ToString();
  }

  private class GitHubFile
  {
    public string FileName {get;set;} = "";
    public string Status {get;set;} = "";
    public string? Patch {get;set;}
  }
}