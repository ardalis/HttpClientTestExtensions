using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.AuthEndpoints;

public class Unauthorized : EndpointBaseAsync
    .WithoutRequest
    .WithoutResult
{
  [HttpGet("/unauthorized")]
  public override Task<UnauthorizedResult> HandleAsync(CancellationToken cancellationToken = default)
  {
    return Task.FromResult(Unauthorized());
  }
}
