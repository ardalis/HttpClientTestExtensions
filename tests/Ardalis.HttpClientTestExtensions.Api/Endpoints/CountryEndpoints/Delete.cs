using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;
using Ardalis.HttpClientTestExtensions.Api.Dtos;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class Delete : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<bool>
{
  private readonly IMapper _mapper;
  private readonly IRepository<Country> _repository;

  public Delete(IMapper mapper, IRepository<Country> repository)
  {
    _mapper = mapper;
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

    var response = _mapper.Map<CountryDto>(entity);

    return Ok(response);
  }
}
