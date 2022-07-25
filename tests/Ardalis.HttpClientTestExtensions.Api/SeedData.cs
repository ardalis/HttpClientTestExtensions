using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.Infrastructure.Data;

namespace Ardalis.HttpClientTestExtensions.Api;
public static class SeedData
{
  public static readonly Country TestCountry1 = new()
  {
    Id = "USA",
    Name = "USA"
  };

  public static readonly Country TestCountry2 = new()
  {
    Id = "KSA",
    Name = "Saudi Arabia"
  };

  public static readonly City TestCity1 = new()
  {
    CountryId = "EG",
    Name = "Cairo"
  };

  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Cities)
    {
      dbContext.Remove(item);
    }
    foreach (var item in dbContext.Countries)
    {
      dbContext.Remove(item);
    }
    dbContext.SaveChanges();

    TestCountry1.AddCity(TestCity1);
    dbContext.Countries.Add(TestCountry1);

    dbContext.Countries.Add(TestCountry2);

    dbContext.SaveChanges();
  }
}

