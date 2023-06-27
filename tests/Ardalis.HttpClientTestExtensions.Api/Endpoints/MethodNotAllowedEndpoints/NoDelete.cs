using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.MethodNotAllowedEndpoints;

public class NoDelete : EndpointBaseSync
  .WithoutRequest
  .WithResult<NoContentResult>

{
  [Route("/nodelete")]
  [AcceptVerbs("GET", "PUT", "POST")]
  public override NoContentResult Handle()
  {
    return NoContent();
  }
}
