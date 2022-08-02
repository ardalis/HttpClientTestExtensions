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

public class Edit : EndpointBaseAsync
    .WithRequest<EditCityRequest>
    .WithActionResult<CityDto>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<City> _readRepository;
  private readonly IRepository<City> _repository;

  public Edit(IMapper mapper, IReadRepository<City> readRepository, IRepository<City> repository)
  {
    _mapper = mapper;
    _readRepository = readRepository;
    _repository = repository;
  }

  [HttpPut(EditCityRequest.Route)]
  public override async Task<ActionResult<CityDto>> HandleAsync([FromBody] EditCityRequest cityDto, CancellationToken cancellationToken = default)
  {
    var entity = await _repository.GetByIdAsync(cityDto.Id, cancellationToken);
    if (entity == null)
    {
      return NotFound();
    }
    var entityToSave = _mapper.Map<City>(cityDto);
    await _repository.UpdateAsync(entityToSave, cancellationToken);

    var response = _mapper.Map<CityDto>(entityToSave);

    return Ok(response);
  }
}
