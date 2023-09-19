using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.ErrorEndpoints;

public class BadRequest : EndpointBaseSync
  .WithoutRequest
  .WithResult<BadRequestResult>
    
{
  [Route("/badrequest")]
  [AcceptVerbs("GET", "DELETE", "PUT", "PATCH", "POST")]
  public override BadRequestResult Handle()
  {
    return BadRequest();
  }
}
