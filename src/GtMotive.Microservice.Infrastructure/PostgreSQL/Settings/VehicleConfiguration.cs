using GtMotive.Microservice.Domain.Entities;
using GtMotive.Microservice.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Infrastructure.PostgreSQL.Settings;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.Property(v => v.Brand)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(v => v.Model)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(v => v.ManufactureDate)
            .HasConversion(vo => vo.Value, raw => ManufactureDate.FromPersistence(raw))
            .HasColumnName("ManufactureDate")
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(v => v.IsRented)
            .HasColumnType("boolean")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(v => v.RentedBy)
            .HasColumnType("varchar(255)")
            .IsRequired(false);

        builder.ToTable("vehicles");
    }
}
