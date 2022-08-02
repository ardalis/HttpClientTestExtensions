using AutoMapper;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;
using Ardalis.HttpClientTestExtensions.Core.Entities;

namespace Ardalis.HttpClientTestExtensions.Api.MappingProfiles;

public class CityProfile : Profile
{
  public CityProfile()
  {
    CreateMap<City, CityDto>();
    CreateMap<CityDto, City>();
    CreateMap<AddCityRequest, City>();
    CreateMap<EditCityRequest, City>();
  }
}

