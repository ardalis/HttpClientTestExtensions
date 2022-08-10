using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions.Api;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
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
  public async Task DeleteAndDeserializeTestAsync()
  {
    var expectedId = SeedData.TestCountry2.Id;
    var expectedName = SeedData.TestCountry2.Name;

    var response = await _client.DeleteAndDeserializeAsync<CountryDto>("/countries/KSA", _outputHelper);

    response.Id.ShouldBe(expectedId);
    response.Name.ShouldBe(expectedName);
  }

  [Fact]
  public async Task DeleteAndEnsureNotFoundTestAsync()
  {
    var response = await _client.DeleteAndEnsureNotFoundAsync("/wrongendpoint", _outputHelper);

    // It's not needed but shown to demonstrate it's available to use
    response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
  }

  [Fact]
  public async Task DeleteAndEnsureNoContentTestAsync()
  {
    _ = await _client.DeleteAndEnsureNoContentAsync("/countries/4", _outputHelper);
  }

  [Fact]
  public async Task DeleteAndEnsureSubstringAsync_With_Matching_Substring()
  {
    var expectedJson = "{\"id\":\"USA\",\"name\":\"USA\"}";
    var response = await _client.DeleteAndEnsureSubstringAsync("/countries/USA", "USA", _outputHelper);

    response.ShouldBe(expectedJson);
  }

  [Fact]
  public async Task DeleteAndEnsureSubstringAsync_Without_Matching_Substring()
  {
    await Assert.ThrowsAsync<HttpRequestException>(() => _client.DeleteAndEnsureSubstringAsync("/countries/USA", "banana", _outputHelper));
  }
}
