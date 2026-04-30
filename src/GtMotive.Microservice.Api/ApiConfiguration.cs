using AutoMapper;
using GtMotive.Microservice.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Api;

public static class ApiConfiguration
{
    /// <summary>
    /// Adds services required for the  API to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the Vehicle API services will be added.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
    public static IServiceCollection AddVehicleApi(this IServiceCollection services)
    {
        return services;
    }
}
