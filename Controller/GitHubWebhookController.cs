using Microsoft.AspNetCore.Mvc;
using PRReviewAgent.Agents;
using PRReviewAgent.Models;
using PRReviewAgent.Services;

namespace PRReviewAgent.Controllers;

[ApiController]
[Route("webhook")]
public class GitHubhookController: ControllerBase
{
  private readonly PRAgent _agent;
  private readonly GitHubService _gitHub;

  private readonly DiffAnalyzerService _diffAnalyzerService;

  public GitHubhookController(PRAgent agent, GitHubService gitHub,DiffAnalyzerService diffAnalyzerService)
  {
    _agent = agent;
    _gitHub = gitHub;
    _diffAnalyzerService = diffAnalyzerService; 
  }

  [HttpPost]
  public async Task<IActionResult> Receive([FromBody] PullRequestEvent request)
  {

    var repositoryParts = request.repository.FullName.Split('/');
    var owner = repositoryParts[0];
    var repo = repositoryParts[1];


    var files = await _gitHub.GetPullRequestFilesAsync(
      owner,
      repo,
      request.pullRequest.Number
    );

    var codeChanges = _diffAnalyzerService.ConvertToReviewInput(files);

    var review = await _agent.ReviewAsync(request.pullRequest.Title,
    request.pullRequest.Body,codeChanges);
    if(request.pullRequest.Number <=0)
    {
      return BadRequest("Invalid pull request number");
    }

    return Ok(new
    {
      request.Action,
      request.pullRequest.Title,
      Review = review
    });
  }


}