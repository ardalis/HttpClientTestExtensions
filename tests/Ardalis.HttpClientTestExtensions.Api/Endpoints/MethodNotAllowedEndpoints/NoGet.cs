using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.MethodNotAllowedEndpoints;

public class NoGet : EndpointBaseSync
  .WithoutRequest
  .WithResult<NoContentResult>

{
  [Route("/noget")]
  [AcceptVerbs("DELETE", "PUT", "POST")]
  public override NoContentResult Handle()
  {
    return NoContent();
  }
}
