using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.AuthEndpoints;

public class Unauthorized : EndpointBaseSync
    .WithoutRequest
    .WithResult<UnauthorizedResult>
{
  [HttpGet("/unauthorized")]
  public override UnauthorizedResult Handle()
  {
    return Unauthorized();
  }
}
