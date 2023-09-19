using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.ErrorEndpoints;

public class Redirect : EndpointBaseSync
  .WithoutRequest
  .WithResult<RedirectResult>
    
{
  [Route("/redirect")]
  [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
  public override RedirectResult Handle()
  {
    return Redirect("/redirected");
  }
}
