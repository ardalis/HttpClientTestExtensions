using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class Add : EndpointBaseAsync
    .WithRequest<AddCityRequest>
    .WithActionResult<CityDto>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<City> _readRepository;
  private readonly IRepository<City> _repository;

  public Add(IMapper mapper, IReadRepository<City> readRepository, IRepository<City> repository)
  {
    _mapper = mapper;
    _readRepository = readRepository;
    _repository = repository;
  }

  [HttpPost(AddCityRequest.Route)]
  public override async Task<ActionResult<CityDto>> HandleAsync([FromBody] AddCityRequest cityDto, CancellationToken cancellationToken = default)
  {
    var entityToSave = _mapper.Map<City>(cityDto);

    var addedEntity = await _repository.AddAsync(entityToSave, cancellationToken);

    var response = _mapper.Map<CityDto>(addedEntity);

    return Ok(response);
  }
}
