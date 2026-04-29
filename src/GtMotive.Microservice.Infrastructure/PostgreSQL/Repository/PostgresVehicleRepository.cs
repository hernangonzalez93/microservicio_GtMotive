using GtMotive.Microservice.Domain.Entities;
using GtMotive.Microservice.Domain.Ports;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Infrastructure.PostgreSQL.Repository;

public class PostgresVehicleRepository : IVehicleRepository
{
    private readonly PostgresDbContext _context;
    private readonly ILogger<PostgresVehicleRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostgresVehicleRepository"/> class.
    /// </summary>
    /// <param name="context">The <see cref="PostgresDbContext"/> used to access the PostgreSQL database.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public PostgresVehicleRepository(PostgresDbContext context, ILogger<PostgresVehicleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Asynchronously adds a new vehicle to the database.
    /// </summary>
    /// <param name="vehicle">The vehicle to add. Cannot be <see langword="null"/>.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddAsync(Vehicle vehicle)
    {
        _logger.LogInformation($"Adding vehicle with Id: {vehicle.Id}, Model: {vehicle.Model}, ManufactureDate: {vehicle.ManufactureDate}");

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Vehicle with Id: {vehicle.Id} added successfully.");
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to retrieve. Cannot be null or empty.</param>
    /// <returns>A <see cref="Vehicle"/> object representing the vehicle with the specified identifier,
    /// or <see langword="null"/> if no matching vehicle is found.</returns>
    public async Task<Vehicle?> GetByIdAsync(string id)
    {
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves a list of all vehicles from the database.
    /// </summary>
    /// <remarks>This method asynchronously fetches all vehicle records from the PostgreSQL database.
    /// The returned list will be empty if no vehicles are found.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="Vehicle"/>
    /// objects representing all vehicles in the database.</returns>
    public async Task<List<Vehicle>> ListAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the specified vehicle in the database asynchronously.
    /// </summary>
    /// <remarks>This method updates the existing database record that matches the <see cref="Vehicle.Id"/>
    /// of the provided <paramref name="vehicle"/>. Ensure that the <paramref name="vehicle"/>
    /// object contains valid data before calling this method.</remarks>
    /// <param name="vehicle">The vehicle to update. The <see cref="Vehicle.Id"/> property must match an existing record in the database.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    public async Task UpdateAsync(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Determines whether the specified person has rented a vehicle.
    /// </summary>
    /// <remarks>This method queries the PostgreSQL database to check if any vehicle is currently
    /// rented by the specified person.</remarks>
    /// <param name="personId">The unique identifier of the person to check. Cannot be <see langword="null"/> or empty.</param>
    /// <returns><see langword="true"/> if the person has rented a vehicle; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> HasPersonRentedVehicleAsync(string personId)
    {
        _logger.LogInformation($"Checking if person with Id: {personId} has rented a vehicle.");

        var hasRented = await _context.Vehicles.AnyAsync(v => v.RentedBy == personId && v.IsRented);

        _logger.LogInformation($"Person with Id: {personId} has rented a vehicle: {hasRented}");
        return hasRented;
    }
}