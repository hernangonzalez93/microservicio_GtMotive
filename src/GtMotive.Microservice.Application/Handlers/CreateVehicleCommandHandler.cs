using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Domain.Entities;
using GtMotive.Microservice.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Handlers;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<string>>
{
    private readonly IVehicleRepository _repository;
    private readonly ILogger<CreateVehicleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
    /// </summary>
    /// <param name="repository">The vehicle repository used for data operations.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public CreateVehicleCommandHandler(IVehicleRepository repository, ILogger<CreateVehicleCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CreateVehicleCommand by creating a new vehicle and storing it in the repository.
    /// </summary>
    /// <param name="request">The CreateVehicleCommand containing the vehicle details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ID of the newly created vehicle.</returns>
    public async Task<Result<string>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateVehicleCommand for Brand: {Brand}, Model: {Model}", request.Brand, request.Model);

        var vehicle = new Vehicle(request.Brand, request.Model, request.ManufactureDate);
        await _repository.AddAsync(vehicle, cancellationToken);

        _logger.LogInformation("Vehicle created successfully with ID: {VehicleId}", vehicle.Id);
        return Result<string>.Success($"Vehicle was successfully created with id {vehicle.Id}");
    }
}
