using GtMotive.Microservice.Domain.Ports;
using GtMotive.Microservice.Infrastructure.Behaviors;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Context;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Repository;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register PostgreSQL settings from configuration
        services.Configure<PostgresDbSettings>(options => configuration.GetSection("PostgreSQL").Bind(options));

        // Register PostgreSQL DbContext
        services.AddDbContext<PostgresDbContext>((serviceProvider, options) =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<PostgresDbSettings>>().Value;
            options.UseNpgsql(settings.ConnectionString);
            
        });

        // Register  PostgreSQL repository
        services.AddScoped<IVehicleRepository, PostgresVehicleRepository>();

        // Register MediatR Pipeline Behaviors for logging
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}