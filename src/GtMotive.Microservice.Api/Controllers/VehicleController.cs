using AutoMapper;
using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Application.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Api.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<VehicleController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator for dispatching requests.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public VehicleController(
        IMediator mediator,
        IMapper mapper,
        ILogger<VehicleController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new vehicle with the specified details and adds it to the system.
    /// </summary>
    /// <param name="request">The request containing vehicle details.</param>
    /// <returns>A 201 Created response with the vehicle ID.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request, CancellationToken ct)
    {
        _logger.LogInformation("API: Initialize vehicle creation");
        try
        {
            // Validate request model
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("API: Invalid model state");
                return BadRequest(ModelState);
            }

            // Map request to command
            var command = _mapper.Map<CreateVehicleCommand>(request);

            // Send command through MediatR
            var id = await _mediator.Send(command, ct);

            _logger.LogInformation("API: Vehicle created successfully with ID: {VehicleId}", id);
            //return CreatedAtAction(nameof(GetAll), new { id }, new { id });
            return Ok(id);
        }

        catch (Exception ex)
        {
            _logger.LogError("API: Error in Create Vehicle: {Exception}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Gets all vehicles available in the system.
    /// </summary>
    /// <returns>A 200 OK response with a list of vehicles.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllVehicleRequest getAllVehicleRequest,
        CancellationToken ct
        )
    {
        _logger.LogInformation("API: Getting all vehicles");
        try
        {
            // Create query
            var query = new GetAllVehicleQuery(getAllVehicleRequest);

            // Send query through MediatR
            var vehicles = await _mediator.Send(query, ct);

            _logger.LogInformation("API: Retrieved {Count} vehicles", vehicles.Value);
            return vehicles.IsSuccess ? Ok(vehicles.Value) : BadRequest(vehicles.Error);
        }
        catch (Exception ex)
        {
            _logger.LogError("API: Error retrieving vehicles: {Exception}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
}
