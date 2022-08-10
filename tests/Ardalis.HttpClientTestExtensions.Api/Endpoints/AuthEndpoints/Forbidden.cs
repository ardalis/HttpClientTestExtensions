using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.AuthEndpoints;

public class Forbidden : EndpointBaseSync
  .WithoutRequest
  .WithResult<ForbidResult>
    
{
  [HttpGet("/forbid")]
  public override ForbidResult Handle()
  {
    return Forbid();
  }
}
