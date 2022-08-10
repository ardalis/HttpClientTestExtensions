using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.ErrorEndpoints;

public class BadRequest : EndpointBaseSync
  .WithoutRequest
  .WithResult<BadRequestResult>
    
{
  [HttpGet("/badrequest")]
  public override BadRequestResult Handle()
  {
    return BadRequest();
  }
}
