using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions.Api;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions.Tests;

public class HttpClientGetExtensionMethodsTests : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;
  private readonly CustomWebApplicationFactory _factory;

  public HttpClientGetExtensionMethodsTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
    _factory = factory;
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
    _ = await _client.GetAndEnsureNotFoundAsync("/wrongendpoint/USA", _outputHelper);
  }

  [Fact]
  public async Task GetAndReturnStringTestAsync()
  {
    var expectedJson = "{\"id\":\"USA\",\"name\":\"USA\"}";
    var response = await _client.GetAndReturnStringAsync("/countries/USA", _outputHelper);

    response.ShouldBe(expectedJson);
  }

  [Fact]
  public async Task GetAndEnsureSubstringAsync_With_Matching_Substring()
  {
    var expectedJson = "{\"id\":\"USA\",\"name\":\"USA\"}";
    var response = await _client.GetAndEnsureSubstringAsync("/countries/USA", "USA", _outputHelper);

    response.ShouldBe(expectedJson);
  }

  [Fact]
  public async Task GetAndEnsureSubstringAsync_Without_Matching_Substring()
  {
    await Assert.ThrowsAsync<HttpRequestException>(() => _client.GetAndEnsureSubstringAsync("/countries/USA", "banana", _outputHelper));
  }

  [Fact]
  public async Task GetAndEnsureUnauthorizedAsync()
  {
    _ = await _client.GetAndEnsureUnauthorizedAsync("/unauthorized", _outputHelper);
  }

  [Fact]
  public async Task GetAndEnsureForbiddenAsync()
  {
    _ = await _client.GetAndEnsureForbiddenAsync("/forbid", _outputHelper);
  }

  [Fact]
  public async Task GetAndEnsureBadRequestAsync()
  {
    _ = await _client.GetAndEnsureBadRequestAsync("/badrequest", _outputHelper);
  }

  [Fact]
  public async Task GetAndEnsureMethodNotAllowedAsync()
  {
    _ = await _client.GetAndEnsureMethodNotAllowedAsync("/noget", _outputHelper);
  }
  
  [Fact]
  public async Task GetAndRedirectAsync()
  {
    var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
    _ = await client.GetAndRedirectAsync("/redirect", "/redirected", _outputHelper);
  }
}
