using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.Core.Specifications;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class Delete : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<bool>
{
  private readonly IRepository<Country> _repository;

  public Delete(IRepository<Country> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteCountryRequest.Route)]
  public override async Task<ActionResult<bool>> HandleAsync(string id, CancellationToken cancellationToken = default)
  {
    var entity = await _repository.GetByIdAsync(id, cancellationToken);
    if (entity == null)
    {
      return NoContent();
    }
    await _repository.DeleteAsync(entity, cancellationToken);

    return Ok(true);
  }
}
