using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.Core.Specifications;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class Delete : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<bool>
{
  private readonly IRepository<City> _repository;

  public Delete(IRepository<City> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteCityRequest.Route)]
  public override async Task<ActionResult<bool>> HandleAsync(int id, CancellationToken cancellationToken = default)
  {
    var entity = await _repository.GetByIdAsync(id, cancellationToken);
    if (entity == null)
    {
      return NotFound();
    }
    await _repository.DeleteAsync(entity, cancellationToken);

    return Ok(true);
  }
}
