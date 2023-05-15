using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class Patch : EndpointBaseAsync
    .WithRequest<EditCountryRequest>
    .WithActionResult<CountryDto>
{
  private readonly IMapper _mapper;
  private readonly IReadRepository<Country> _readRepository;
  private readonly IRepository<Country> _repository;

  public Patch(IMapper mapper, IReadRepository<Country> readRepository, IRepository<Country> repository)
  {
    _mapper = mapper;
    _readRepository = readRepository;
    _repository = repository;
  }

  [HttpPatch(EditCountryRequest.Route)]
  public override async Task<ActionResult<CountryDto>> HandleAsync([FromBody] EditCountryRequest countryDto, CancellationToken cancellationToken = default)
  {
    var entity = await _repository.GetByIdAsync(countryDto.Id, cancellationToken);
    if (entity == null)
    {
      return NotFound();
    }
    var entityToSave = _mapper.Map(countryDto, entity);
    await _repository.UpdateAsync(entityToSave, cancellationToken);

    var response = _mapper.Map<CountryDto>(entityToSave);

    return Ok(response);
  }
}
