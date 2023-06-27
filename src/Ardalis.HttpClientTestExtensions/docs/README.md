# HttpClient Test Extensions

Extensions for testing HTTP endpoints and deserializing the results. Currently works with XUnit.

## Installation

Add the NuGet package:

```powershell
dotnet add package Ardalis.HttpClientTestExtensions
```

In your tests add this namespace:

```csharp
using Ardalis.HttpClientTestExtensions;
```

## Usage

If you have existing test code that looks something like this:

```csharp
public class DoctorsList : IClassFixture<CustomWebApplicationFactory<Startup>>
{
  private readonly HttpClient _client;
  private readonly ITestOutputHelper _outputHelper;

  public DoctorsList(CustomWebApplicationFactory<Startup> factory,
    ITestOutputHelper outputHelper)
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

```csharp
[Fact]
public async Task Returns3Doctors()
{
  var result = await _client.GetAndDeserialize<ListDoctorResponse>("/api/doctors", _outputHelper);

  Assert.Equal(3, result.Doctors.Count());
  Assert.Contains(result.Doctors, x => x.Name == "Dr. Smith");
}
```

If you need to verify an endpoint returns a 404, you can use this approach:

```csharp
[Fact]
public async Task ReturnsNotFoundGivenInvalidAuthorId()
{
  int invalidId = 9999;

  var response = await _client.GetAsync(Routes.Authors.Get(invalidId));

  response.EnsureNotFound();
}
```

## List of Included Helper Methods

### HttpClient

All of these methods are extensions on `HttpClient`; the following samples assume `client` is an `HttpClient`. All methods take an optional `ITestOutputHelper`, which is an xUnit type.

#### [GET](src\Ardalis.HttpClientTestExtensions\HttpClientGetExtensionMethods.cs)

```csharp
// GET and return an object T
AuthorDto result = await client.GetAndDeserializeAsync("/authors/1", _testOutputHelper);

// GET and return response as a string
string result = client.GetAndReturnStringAsync("/healthcheck");

// GET and ensure response contains a substring
string result = client.GetAndEnsureSubstringAsync("/healthcheck", "OMG!");

// GET and assert a 302 is returned
var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
await client.GetAndEnsureRedirectAsync("/oldone, "/newone");

// GET and assert a 400 is returned
await client.GetAndEnsureBadRequestAsync("/authors?page");

// GET and assert a 401 is returned
await client.GetAndEnsureUnauthorizedAsync("/authors/1");

// GET and assert a 403 is returned
await client.GetAndEnsureForbiddenAsync("/authors/1");

// GET and assert a 404 is returned
await client.GetAndEnsureNotFoundAsync("/authors/-1");

// GET and assert a 405 is returned
await client.GetAndEnsureMethodNotAllowedAsync("/wrongendpoint", content)
```

#### [POST](src\Ardalis.HttpClientTestExtensions\HttpClientPostExtensionMethods.cs)

```csharp
// NOTE: There's a helper for this now, too (see below)
var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

// POST and return an object T
AuthorDto result = await client.PostAndDeserializeAsync("/authors", content);

// POST and ensure response contains a substring
string result = client.PostAndEnsureSubstringAsync("/authors", content, "OMG!");

// POST and assert a 302 is returned
var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
await client.PostAndEnsureRedirectAsync("/oldone", content, "/newone");

// POST and assert a 400 is returned
await client.PostAndEnsureBadRequestAsync("/authors", "banana");

// POST and assert a 401 is returned
await client.PostAndEnsureUnauthorizedAsync("/authors", content);

// POST and assert a 403 is returned
await client.PostAndEnsureForbiddenAsync("/authors", content);

// POST and assert a 404 is returned
await client.PostAndEnsureNotFoundAsync("/wrongendpoint", content)

// POST and assert a 405 is returned
await client.PostAndEnsureMethodNotAllowedAsync("/wrongendpoint", content)
```

#### [PUT](src\Ardalis.HttpClientTestExtensions\HttpClientPutExtensionMethods.cs)

```csharp
var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

// PUT and return an object T
AuthorDto result = await client.PutAndDeserializeAsync("/authors/1", content);

// PUT and ensure response contains a substring
string result = client.PutAndEnsureSubstringAsync("/authors/1", content, "OMG!");

// PUT and assert a 302 is returned
var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
await client.PutAndEnsureRedirectAsync("/oldone", content, "/newone");

// PUT and assert a 400 is returned
await client.PutAndEnsureBadRequestAsync("/authors/1", "banana");

// PUT and assert a 401 is returned
await client.PutAndEnsureUnauthorizedAsync("/authors/1", content);

// PUT and assert a 403 is returned
await client.PutAndEnsureForbiddenAsync("/authors/1", content);

// PUT and assert a 404 is returned
await client.PutAndEnsureNotFoundAsync("/wrongendpoint", content)

// PUT and assert a 405 is returned
await client.PutAndEnsureMethodNotAllowedAsync("/wrongendpoint", content)
```

#### [DELETE](src\Ardalis.HttpClientTestExtensions\HttpClientDeleteExtensionMethods.cs)

```csharp
// DELETE and return an object T
AuthorDto result = await client.DeleteAndDeserializeAsync("/authors/1");

// DELETE and ensure response contains a substring
string result = client.DeleteAndEnsureSubstringAsync("/authors/1", "OMG!");

// DELETE and assert a 204 is returned
await client.DeleteAndEnsureNoContentAsync("/authors/1");

// DELETE and assert a 302 is returned
var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
await client.DeleteAndEnsureRedirectAsync("/oldone", "/newone");

// DELETE and assert a 400 is returned
await client.DeleteAndEnsureBadRequestAsync("/authors/1");

// DELETE and assert a 401 is returned
await client.DeleteAndEnsureUnauthorizedAsync("/authors/1");

// DELETE and assert a 403 is returned
await client.DeleteAndEnsureForbiddenAsync("/authors/1");

// DELETE and assert a 404 is returned
await client.DeleteAndEnsureNotFoundAsync("/wrongendpoint");

// DELETE and assert a 405 is returned
await client.DeleteAndEnsureMethodNotAllowedAsync("/wrongendpoint", content)
```

### [HttpResponseMessage](src\Ardalis.HttpClientTestExtensions\HttpResponseMessageExtensionMethods.cs)

All of these methods are extensions on `HttpResponseMessage`.

```csharp
// Assert a response has a status code of 204
response.EnsureNoContent();

// Assert a response has a status code of 302
response.EnsureRedirect("/newone");

// Assert a response has a status code of 400
response.EnsureBadRequest();

// Assert a response has a status code of 401
response.EnsureUnauthorized();

// Assert a response has a status code of 403
response.EnsureForbidden();

// Assert a response has a status code of 404
response.EnsureNotFound();

// Assert a response has a status code of 405
response.EnsureMethodNotAllowed();

// Assert a response has a given status code
response.Ensure(HttpStatusCode.Created);

// Assert a response contains a substing
response.EnsureContainsAsync("OMG!", _testOutputHelper);
```

### [StringContentHelpers](src\Ardalis.HttpClientTestExtensions\StringContentHelpers.cs)

Extensions on `HttpContent` which you'll typically want to return a `StringContent` type as you serialize your DTO to JSON.

```csharp

// Convert a C# DTO to a StringContent JSON type
var authorDto = new ("Steve");
var content = StringContentHelpers.FromModelAsJson(authorDto);

// now you can use this with a POST, PUT, etc.
AuthorDto result = await client.PostAndDeserializeAsync("/authors", content);

// Or you can do it all in one line (assuming you already have the DTO)
AuthorDto result = await client.PostAndDeserializeAsync("/authors",
    StringContentHelpers.FromModelAsJson(authorDto));
```

## Notes

- For now this is coupled with xUnit but if there is interest it could be split so the ITestOutputHelper dependency is removed/optional/swappable
- Additional helpers for other verbs are planned
- This is using System.Text.Json with default camelCase options that I've found most useful in my projects. This could be made extensible somehow as well.
