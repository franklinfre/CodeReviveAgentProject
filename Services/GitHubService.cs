using System.Net.Http.Headers;

namespace PRReviewAgent.Services;

public class GitHubService
{
  private readonly HttpClient _client;

  public GitHubService(HttpClient client, IConfiguration configuration)
  {
    _client = client;

    _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(
      "PRReviewAgent",
      "1.0"
    ));

    var token = configuration["GitHub:Token"];

    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
      "Bearer",
      token);
  }

  public async Task<string> GetPullRequestFilesAsync(string owner,string repo, int pullnumer)
  {
    var url = $"repos/{owner}/{repo}/pulls/{pullnumer}/files";

    var response = await _client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
  }
}