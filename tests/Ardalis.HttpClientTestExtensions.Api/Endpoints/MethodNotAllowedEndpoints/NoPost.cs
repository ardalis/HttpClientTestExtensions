using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.ErrorEndpoints;

public class NoPost : EndpointBaseSync
  .WithoutRequest
  .WithResult<NoContentResult>
    
{
  [Route("/nopost")]
  [AcceptVerbs("DELETE", "PUT", "GET")]
  public override NoContentResult Handle()
  {
    return NoContent();
  }
}
