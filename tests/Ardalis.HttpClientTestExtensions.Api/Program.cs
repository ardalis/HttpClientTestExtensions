
using System;
using System.Collections.Generic;
using Ardalis.ListStartupServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ardalis.HttpClientTestExtensions.Infrastructure.Data;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;
using Ardalis.HttpClientTestExtensions.Api;

const string CORS_POLICY = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
void OptionsAction(DbContextOptionsBuilder options)
{
  options.EnableSensitiveDataLogging();
  options.UseSqlServer(connectionString);
}

builder.Services.AddDbContext<AppDbContext>(OptionsAction);

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
  options.AddPolicy(CORS_POLICY, builder =>
  {
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.AllowAnyOrigin();
  });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = "forbidScheme";
  options.DefaultForbidScheme = "forbidScheme";
  options.AddScheme<MyAuthenticationHandler>("forbidScheme", "Handle Forbidden");
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}
app.UseRouting();

app.UseCors(CORS_POLICY);

app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();
  endpoints.MapRazorPages();
});

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    //var context = services.GetRequiredService<AppDbContext>();
    //                    context.Database.Migrate();
    //context.Database.EnsureCreated();
    //SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB.");
  }
}

app.Run();

public partial class Program { }
