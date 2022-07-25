using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ardalis.HttpClientTestExtensions.Core.Entities;
using Ardalis.HttpClientTestExtensions.Infrastructure.Data.Constants;

namespace Ardalis.HttpClientTestExtensions.Infrastructure.Data.Config;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
  public void Configure(EntityTypeBuilder<City> builder)
  {
    builder
      .ToTable("Cities", "Lockup")
      .HasKey(x => x.Id);

    builder
      .Property(p => p.Id)
      .HasColumnName("Id")
      .IsRequired();

    builder
      .Property(p => p.Name)
      .HasColumnName("Name")
      .HasMaxLength(DatabaseColumnsWidth.NAME)
      .IsRequired();

    builder
      .Property(p => p.CountryId)
      .HasColumnName("CountryId");

    builder
      .HasOne(t => t.Country)
      .WithMany(p => p.Cities)
      .HasForeignKey(d => d.CountryId)
      .OnDelete(DeleteBehavior.ClientSetNull);
  }
}
