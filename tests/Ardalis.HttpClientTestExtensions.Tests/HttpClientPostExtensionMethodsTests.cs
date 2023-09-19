using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions.Tests;

public class HttpClientPostExtensionMethodsTests : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;
  private readonly CustomWebApplicationFactory _factory;

  public HttpClientPostExtensionMethodsTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
    _factory = factory;
  }

  [Fact]
  public async Task PostAndDeserializeTestAsync()
  {
    var expectedId = "CAN";
    var expectedName = "Canada";
    var dto = new CountryDto { Id = expectedId, Name = expectedName };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    var response = await _client.PostAndDeserializeAsync<CountryDto>("/countries", content, _outputHelper);

    response.Id.ShouldBe(expectedId);
    response.Name.ShouldBe(expectedName);
  }

  [Fact]
  public async Task PostAndEnsureNotFoundTestAsync()
  {
    var dto = new CountryDto();
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    _ = await _client.PostAndEnsureNotFoundAsync("/wrongendpoint", content, _outputHelper);
  }

  [Fact]
  public async Task PostAndEnsureSubstringAsync_With_Matching_Substring()
  {
    var expectedJson = "{\"id\":\"FR\",\"name\":\"France\"}";
    var dto = new CountryDto { Id = "FR", Name = "France" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    var response = await _client.PostAndEnsureSubstringAsync("/countries", content, "France", _outputHelper);

    response.ShouldBe(expectedJson);
  }

  [Fact]
  public async Task PostAndEnsureSubstringAsync_Without_Matching_Substring()
  {
    var dto = new CountryDto { Id = "ESP", Name = "Spain" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    await Assert.ThrowsAsync<HttpRequestException>(() => _client.PostAndEnsureSubstringAsync("/countries", content, "banana", _outputHelper));
  }

  [Fact]
  public async Task PostAndEnsureBadRequestAsync()
  {
    var dto = new CountryDto { Id = "ESP", Name = "Spain" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    _ = await _client.PostAndEnsureBadRequestAsync("/badrequest", content, _outputHelper);
  }

  [Fact]
  public async Task PostAndRedirectAsync()
  {
    var dto = new CountryDto { Id = "ESP", Name = "Spain" };

    // use helper to get content
    //var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    var content = StringContentHelpers.FromModelAsJson(dto);
    var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
    _ = await client.PostAndRedirectAsync("/redirect", content, "/redirected", _outputHelper);
  }

  [Fact]
  public async Task PostAndEnsureMethodNotAllowedAsync()
  {
    var dto = new CountryDto { Id = "ESP", Name = "Spain" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    _ = await _client.PostAndEnsureMethodNotAllowedAsync("/nopost", content, _outputHelper);
  }
}
