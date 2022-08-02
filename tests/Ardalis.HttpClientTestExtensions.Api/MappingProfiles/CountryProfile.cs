using AutoMapper;
using Ardalis.HttpClientTestExtensions.Api.Dtos;
using Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;
using Ardalis.HttpClientTestExtensions.Core.Entities;

namespace Ardalis.HttpClientTestExtensions.Api.MappingProfiles;

public class CountryProfile : Profile
{
  public CountryProfile()
  {
    CreateMap<Country, CountryDto>();
    CreateMap<CountryDto, Country>();
    CreateMap<AddCountryRequest, Country>();
    CreateMap<EditCountryRequest, Country>();
  }
}

