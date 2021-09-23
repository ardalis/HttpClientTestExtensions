[![.NET Build and Test](https://github.com/ardalis/HttpClientTestExtensions/workflows/.NET%20Build%20and%20Test/badge.svg)](https://github.com/ardalis/HttpClientTestExtensions/actions?query=workflow%3A%22.NET+Build+and+Test%22)
[![Nuget](https://img.shields.io/nuget/v/Ardalis.HttpClientTestExtensions)](https://www.nuget.org/packages/Ardalis.HttpClientTestExtensions/)
[![Nuget](https://img.shields.io/nuget/dt/Ardalis.HttpClientTestExtensions)](https://www.nuget.org/packages/Ardalis.HttpClientTestExtensions/)

# HttpClient Test Extensions

Extensions for testing HTTP endpoints and deserializing the results. Currently works with XUnit.

## Usage

Add the NuGet package and in your tests add this namespace:

```
using Ardalis.HttpClientTestExtensions;
```

If you have existing test code that looks something like this:

```
public class DoctorsList : IClassFixture<CustomWebApplicationFactory<Startup>>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;

  public DoctorsList(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper)
  {
    _client = factory.CreateClient();
    _outputHelper = outputHelper;
  }

  [Fact]
  public async Task Returns3Doctors()
  {
    var response = await _client.GetAsync("/api/doctors");
    response.EnsureSuccessStatusCode();
    var stringResponse = await response.Content.ReadAsStringAsync();
    _outputHelper.WriteLine(stringResponse);
    var result = JsonSerializer.Deserialize<ListDoctorResponse>(stringResponse,
      Constants.DefaultJsonOptions);

    Assert.Equal(3, result.Doctors.Count());
    Assert.Contains(result.Doctors, x => x.Name == "Dr. Smith");
  }
}
```

You can now update the test to eliminate all but one of the lines prior to the assertions:

```
[Fact]
public async Task Returns3Doctors()
{
  var result = await _client.GetAndDeserialize<ListDoctorResponse>("/api/doctors", _outputHelper);

  Assert.Equal(3, result.Doctors.Count());
  Assert.Contains(result.Doctors, x => x.Name == "Dr. Smith");
}
```

If you need to verify an endpoint returns a 404, you can use this approach:

```
[Fact]
public async Task ReturnsNotFoundGivenInvalidAuthorId()
{
  int invalidId = 9999;

  var response = await _client.GetAsync(Routes.Authors.Get(invalidId));

  response.EnsureNotFound();
}
```

## Notes

- For now this is coupled with xUnit but if there is interest it could be split so the ITestOutputHelper dependency is removed/optional/swappable
- Additional helpers for other verbs are planned
- This is using System.Text.Json with default camelCase options that I've found most useful in my projects. This could be made extensible somehow as well.

