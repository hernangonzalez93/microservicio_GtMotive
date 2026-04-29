using GtMotive.Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Infrastructure.PostgreSQL.Context;

public class PostgresDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostgresDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by this DbContext.</param>
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the vehicles DbSet.
    /// </summary>
    public DbSet<Vehicle> Vehicles { get; set; }

    /// <summary>
    /// Configures the model for the database.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to construct the model for the database.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Vehicle entity
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Id)
                .HasColumnType("varchar(36)")
                .IsRequired();

            entity.Property(v => v.Brand)
                .HasColumnType("varchar(100)")
                .IsRequired();

            entity.Property(v => v.Model)
                .HasColumnType("varchar(100)")
                .IsRequired();

            entity.Property(v => v.ManufactureDate)
                .HasColumnType("timestamp")
                .IsRequired();

            entity.Property(v => v.IsRented)
                .HasColumnType("boolean")
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(v => v.RentedBy)
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            entity.ToTable("vehicles");
        });
    }
}
