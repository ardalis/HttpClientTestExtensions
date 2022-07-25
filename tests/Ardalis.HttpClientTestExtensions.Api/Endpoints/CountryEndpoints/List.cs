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

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<ListResponse<CountryDto>>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<Country> _repository;

  public List(IMapper mapper, IReadRepository<Country> repository)
  {
    _mapper = mapper;
    _repository = repository;
  }

  [HttpGet(ListCountryRequest.Route)]
  public override async Task<ActionResult<ListResponse<CountryDto>>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var spec = new CountriesOrderByNameSpec();
    var entities = await _repository.ListAsync(spec, cancellationToken);
    var responseData = _mapper.Map<List<CountryDto>>(entities);
    var response = new ListResponse<CountryDto>(responseData);

    return Ok(response);
  }
}
