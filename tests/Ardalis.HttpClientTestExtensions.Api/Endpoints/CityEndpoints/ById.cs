using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class ById : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<CityDto>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<City> _repository;

  public ById(IMapper mapper, IReadRepository<City> repository)
  {
    _mapper = mapper;
    _repository = repository;
  }

  [HttpGet(ByIdCityRequest.Route)]
  public override async Task<ActionResult<CityDto>> HandleAsync(int id, CancellationToken cancellationToken = default)
  {
    var entity = await _repository.GetByIdAsync(id, cancellationToken);
    var response = _mapper.Map<CityDto>(entity);

    return Ok(response);
  }
}
