using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Dtos;
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

public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, Result<string>>
{
    private readonly IVehicleRepository _repository;
    private readonly IVehicleDomainService _vehicleDomainService;
    private readonly ILogger<ReturnVehicleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The vehicle repository used for data operations.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public ReturnVehicleCommandHandler(IVehicleRepository repository, IVehicleDomainService vehicleDomainService, ILogger<ReturnVehicleCommandHandler> logger)
    {
        _repository = repository;
        _vehicleDomainService = vehicleDomainService;
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

        var resultValidation = _vehicleDomainService.ValidateReturn(vehicle);
        if (!string.IsNullOrEmpty(resultValidation))
        {
            _logger.LogWarning("Vehicle {VehicleId} cannot be returned: {Reason}", request.VehicleId, resultValidation);
            return Result<string>.Failure(resultValidation);
        }    

        vehicle.Return();
        await _repository.UpdateAsync(vehicle);

        _logger.LogInformation("Handling ReturnVehicleCommand for Vehicle: {VehicleId}", request.VehicleId);
        return Result<string>.Success($"Vehicle with ID {request.VehicleId} returned successfully");
    }
}
