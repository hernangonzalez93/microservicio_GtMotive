using AutoMapper;
using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Application.Querys;
using GtMotive.Microservice.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Handlers;

public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehicleQuery, List<VehicleResponse>>
{
    private readonly IVehicleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllVehiclesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllVehiclesQueryHandler"/> class.
    /// </summary>
    /// <param name="repository">The vehicle repository used for data operations.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public GetAllVehiclesQueryHandler(IVehicleRepository repository, IMapper mapper, ILogger<GetAllVehiclesQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the GetAllVehiclesQuery by retrieving all vehicles and mapping them to DTOs.
    /// </summary>
    /// <param name="request">The GetAllVehiclesQuery.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of VehicleResponse objects.</returns>
    public async Task<List<VehicleResponse>> Handle(GetAllVehicleQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllVehicleQuery");

        var f = request.GetAllVehicleFilter;
        var vehicles = await _repository.ListAsync();
        var vehicleResponses = _mapper.Map<List<VehicleResponse>>(vehicles);

        _logger.LogInformation("Retrieved {Count} vehicles", vehicleResponses.Count);
        return vehicleResponses;
    }

}