using System.Text.Json.Serialization;

namespace PRReviewAgent.Models;

public class PullRequestEvent
{
  [JsonPropertyName("action")]
  public string Action {get;set;} = "";

  [JsonPropertyName("pull_request")]
  public PullRequest pullRequest {get;set;} = new();

  [JsonPropertyName("repository")]
  public Repository repository {get;set;} = new();
}

public class Repository
{
  public string FullName {get;set;} = "";
}