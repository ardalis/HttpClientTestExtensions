using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions.Api;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions.Tests;

public class HttpClientGetExtensionMethodsTests : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;

  public HttpClientGetExtensionMethodsTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  [Fact]
  public async Task GetAndDeserializeTestAsync()
  {
    var expectedId = SeedData.TestCountry1.Id;
    var expectedName = SeedData.TestCountry1.Name;

    var response = await _client.GetAndDeserializeAsync<CountryDto>("/countries/USA", _outputHelper);

    response.Id.ShouldBe(expectedId);
    response.Name.ShouldBe(expectedName);
  }

  [Fact]
  public async Task GetAndEnsureNotFoundTestAsync()
  {
    var response = await _client.GetAndEnsureNotFoundAsync("/wrongendpoint/USA", _outputHelper);

    response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
  }

  [Fact]
  public async Task GetAndReturnStringTestAsync()
  {
    var expectedJson = "{\"id\":\"USA\",\"name\":\"USA\"}";
    var response = await _client.GetAndReturnStringAsync("/countries/USA", _outputHelper);

    response.ShouldBe(expectedJson);
  }
}