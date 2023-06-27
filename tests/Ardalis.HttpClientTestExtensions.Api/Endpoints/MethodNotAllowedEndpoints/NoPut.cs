using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.ErrorEndpoints;

public class NoPut : EndpointBaseSync
  .WithoutRequest
  .WithResult<NoContentResult>
    
{
  [Route("/noput")]
  [AcceptVerbs("DELETE", "GET", "POST")]
  public override NoContentResult Handle()
  {
    return NoContent();
  }

}
