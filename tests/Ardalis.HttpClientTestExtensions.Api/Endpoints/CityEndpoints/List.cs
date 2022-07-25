using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.Core.Specifications;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<ListResponse<CityDto>>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<City> _repository;

  public List(IMapper mapper, IReadRepository<City> repository)
  {
    _mapper = mapper;
    _repository = repository;
  }

  [HttpGet(ListCityRequest.Route)]
  public override async Task<ActionResult<ListResponse<CityDto>>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var spec = new CitiesOrderByNameSpec();
    var entities = await _repository.ListAsync(spec, cancellationToken);
    var responseData = _mapper.Map<List<CityDto>>(entities);
    var response = new ListResponse<CityDto>(responseData);

    return Ok(response);
  }
}
