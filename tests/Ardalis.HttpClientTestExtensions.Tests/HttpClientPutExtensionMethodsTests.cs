using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions.Tests;

public class HttpClientPutExtensionMethodsTests : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;

  public HttpClientPutExtensionMethodsTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  [Fact]
  public async Task PostAndEnsureNotFoundTestAsync()
  {
    var dto = new CountryDto();
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    var response = await _client.PutAndEnsureNotFoundAsync("/wrongendpoint", content, _outputHelper);

    response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
  }
}
