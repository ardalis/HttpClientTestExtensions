using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.AuthEndpoints;

public class Unauthorized : EndpointBaseSync
    .WithoutRequest
    .WithResult<UnauthorizedResult>
{
  [Route("/unauthorized")]
  [AcceptVerbs("GET", "DELETE")]
  public override UnauthorizedResult Handle()
  {
    return Unauthorized();
  }
}
