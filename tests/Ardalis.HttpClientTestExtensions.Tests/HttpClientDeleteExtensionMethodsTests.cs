using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions.Tests;

public class HttpClientDeleteExtensionMethodsTests : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;

  public HttpClientDeleteExtensionMethodsTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  [Fact]
  public async Task DeleteAndEnsureNotFoundAsyncTest()
  {
    var response = await _client.DeleteAndEnsureNotFoundAsync("/branches", _outputHelper);

    response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
  } 
  
  [Fact]
  public async Task DeleteAndEnsureNoContentAsyncTest()
  {
    var response = await _client.DeleteAndEnsureNoContentAsync("/countries/4", _outputHelper);

    response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
  }
}