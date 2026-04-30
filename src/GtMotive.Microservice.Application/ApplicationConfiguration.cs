using GtMotive.Microservice.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        // Register AutoMapper with the VehicleMappingProfile
        //services.AddAutoMapper(typeof(VehicleMappingProfile).Assembly);
        services.AddAutoMapper(cfg => {
            cfg.AddMaps(typeof(VehicleMappingProfile).Assembly);
        }, typeof(VehicleMappingProfile).Assembly);

        // Register MediatR handlers from this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).Assembly));
     
        return services;
    }
}
