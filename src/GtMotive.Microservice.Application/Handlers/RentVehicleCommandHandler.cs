using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Application.Dtos.Pagination;
using GtMotive.Microservice.Domain.DomainServices;
using GtMotive.Microservice.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Handlers;

public class RentVehicleCommandHandler : IRequestHandler<RentVehicleCommand, Result<string>>
{
    private readonly IVehicleRepository _repository;
    private readonly IVehicleDomainService _vehicleDomainService;
    private readonly ILogger<RentVehicleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RentVehicleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The vehicle repository used for data operations.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public RentVehicleCommandHandler(IVehicleRepository repository, IVehicleDomainService vehicleDomainService , ILogger<RentVehicleCommandHandler> logger)
    {
        _repository = repository;
        _vehicleDomainService = vehicleDomainService;
        _logger = logger;
    }

    /// <summary>
    /// Handles the RentVehicleCommand by marking the vehicle as rented.
    /// </summary>
    /// <param name="request">The RentVehicleCommand containing the vehicle and person IDs.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result representing the outcome of the operation.</returns>
    public async Task<Result<string>> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RentVehicleCommand for Vehicle: {VehicleId}, Person: {PersonId}", request.VehicleId, request.PersonId);

        // Check if the person already has a rented vehicle (BUSINESS RULE)
        if (await _repository.HasPersonRentedVehicleAsync(request.PersonId))
            return Result<string>.Failure("Person already has vehicle rented");
 
        var vehicle = await _repository.GetByIdAsync(request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            _logger.LogWarning("Vehicle not found with ID: {VehicleId}", request.VehicleId);
            return Result<string>.Failure($"Vehicle with ID {request.VehicleId} not found");
        }

        var resultValidation = _vehicleDomainService.ValidateRent(vehicle);
        if (!string.IsNullOrEmpty(resultValidation))
        {
            _logger.LogWarning("Vehicle {VehicleId} cannot be rented: {Reason}", request.VehicleId, resultValidation);
            return Result<string>.Failure(resultValidation);
        }

        vehicle.Rent(request.PersonId);
        await _repository.UpdateAsync(vehicle);

        _logger.LogInformation("Vehicle {VehicleId} rented successfully by {PersonId}", request.VehicleId, request.PersonId);
        return Result<string>.Success($"Vehicle {request.VehicleId} rented successfully by {request.PersonId}");
    }
}
