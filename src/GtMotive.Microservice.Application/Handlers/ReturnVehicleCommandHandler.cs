using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Handlers;

public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, Result<string>>
{
    private readonly IVehicleRepository _repository;
    private readonly ILogger<ReturnVehicleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The vehicle repository used for data operations.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public ReturnVehicleCommandHandler(IVehicleRepository repository, ILogger<ReturnVehicleCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the ReturnVehicleCommand by marking the vehicle as returned.
    /// </summary>
    /// <param name="request">The ReturnVehicleCommand containing the vehicle and person IDs.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result representing the outcome of the operation.</returns>
    public async Task<Result<string>> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling ReturnVehicleCommand for Vehicle: {VehicleId}", request.VehicleId);

        var vehicle = await _repository.GetByIdAsync(request.VehicleId, cancellationToken);
        if (vehicle == null)
        {
            _logger.LogWarning("Vehicle not found with ID: {VehicleId}", request.VehicleId);
            return Result<string>.Failure($"Vehicle with ID {request.VehicleId} not found");
        }

        if (!vehicle.IsRented)
        {
            _logger.LogWarning("Vehicle {VehicleId} is not currently rented", request.VehicleId);
            return Result<string>.Failure($"Vehicle is not currently rented");
        }      

        vehicle.Return();
        await _repository.UpdateAsync(vehicle);

        _logger.LogInformation("Handling ReturnVehicleCommand for Vehicle: {VehicleId}", request.VehicleId);
        return Result<string>.Success($"Vehicle with ID {request.VehicleId} returned successfully");
    }
}
