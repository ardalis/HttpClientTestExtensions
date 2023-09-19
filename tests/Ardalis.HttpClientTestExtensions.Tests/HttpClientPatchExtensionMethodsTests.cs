using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.HttpClientTestExtensions.Api;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions.Tests;

public class HttpClientPatchExtensionMethodsTests : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;
  private readonly CustomWebApplicationFactory _factory;

  public HttpClientPatchExtensionMethodsTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
    _factory = factory;
  }

  [Fact]
  public async Task PatchAndDeserializeTestAsync()
  {
    var expectedId = SeedData.TestCountry1.Id;
    var expectedName = "United States of America";
    var dto = new CountryDto { Id = expectedId, Name = expectedName };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    var response = await _client.PutAndDeserializeAsync<CountryDto>("/countries", content, _outputHelper);

    response.Id.ShouldBe(expectedId);
    response.Name.ShouldBe(expectedName);
  }

  [Fact]
  public async Task PatchAndEnsureNotFoundTestAsync()
  {
    var dto = new CountryDto();
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    _ = await _client.PatchAndEnsureNotFoundAsync("/wrongendpoint", content, _outputHelper);
  }

  [Fact]
  public async Task PatchAndEnsureSubstringAsync_With_Matching_Substring()
  {
    var expectedJson = "{\"id\":\"USA\",\"name\":\"'Merica\"}";
    var dto = new CountryDto { Id = SeedData.TestCountry1.Id, Name = "'Merica" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    var response = await _client.PatchAndEnsureSubstringAsync("/countries", content, "erica", _outputHelper);

    response.ShouldBe(expectedJson);
  }

  [Fact]
  public async Task PatchAndEnsureSubstringAsync_Without_Matching_Substring()
  {
    var dto = new CountryDto { Id = SeedData.TestCountry1.Id, Name = "'Merica" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

    await Assert.ThrowsAsync<HttpRequestException>(() => _client.PatchAndEnsureSubstringAsync("/countries", content, "banana", _outputHelper));
  }

  [Fact]
  public async Task PatchAndEnsureBadRequestAsync()
  {
    var dto = new CountryDto { Id = SeedData.TestCountry1.Id, Name = "'Merica" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    _ = await _client.PatchAndEnsureBadRequestAsync("/badrequest", content, _outputHelper);
  }

  [Fact]
  public async Task PutAndRedirectAsync()
  {
    var dto = new CountryDto { Id = "ESP", Name = "Spain" };
    var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
    var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
    _ = await client.PatchAndRedirectAsync("/redirect", content, "/redirected", _outputHelper);
  }
}
