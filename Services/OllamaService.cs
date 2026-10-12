using System.Net.Http.Json;

namespace PRReviewAgent.Services;

public class OllamaService
{
  private readonly HttpClient _client;

  public OllamaService(HttpClient client)
  {
    _client = client;
  }

  public async Task<string> GenerateReviewAsync(string prompt)
  {
    var response = await _client.PostAsJsonAsync(
      "api/generate",
      new
      {
        model = "qwen2.5-coder:7b",
        prompt,
        stream = false
      });


      response.EnsureSuccessStatusCode();

      var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();

      return result?.Response ?? "";
  }

  private class OllamaResponse
  {
    public string Response {get;set;}= "";
  }
}