﻿using System.Net;
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
}
