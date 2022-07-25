using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class ById : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<CountryDto>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<Country> _repository;

  public ById(IMapper mapper, IReadRepository<Country> repository)
  {
    _mapper = mapper;
    _repository = repository;
  }

  [HttpGet(ByIdCountryRequest.Route)]
  public override async Task<ActionResult<CountryDto>> HandleAsync(string id, CancellationToken cancellationToken = default)
  {
    var entity = await _repository.GetByIdAsync(id, cancellationToken);
    var response = _mapper.Map<CountryDto>(entity);

    return Ok(response);
  }
}
