using System;
using System.Linq;
using Ardalis.HttpClientTestExtensions.Api;
using Ardalis.HttpClientTestExtensions.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ardalis.HttpClientTestExtensions.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  protected override IHost CreateHost(IHostBuilder builder)
  {
    var host = builder.Build();
    host.Start();

    var serviceProvider = host.Services;

    using var scope = serviceProvider.CreateScope();
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<AppDbContext>();

    var logger = scopedServices
      .GetRequiredService<ILogger<CustomWebApplicationFactory>>();

    db.Database.EnsureCreated();

    try
    {
      SeedData.PopulateTestData(db);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An error occurred seeding the " +
                          "database with test messages. Error: {exceptionMessage}", ex.Message);
    }

    return host;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder
        .ConfigureServices(services =>
        {
          var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(DbContextOptions<AppDbContext>));

          if (descriptor != null)
          {
            services.Remove(descriptor);
          }

          var inMemoryCollectionName = Guid.NewGuid().ToString();

          services.AddDbContext<AppDbContext>(options =>
            {
              options.UseInMemoryDatabase(inMemoryCollectionName);
            });
        });
  }
}

