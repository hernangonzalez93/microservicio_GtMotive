using GtMotive.Microservice.Domain.Entities;
using GtMotive.Microservice.Domain.Filters;
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
    public async Task AddAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation($"Adding vehicle with Id: {vehicle.Id}, Model: {vehicle.Model}, ManufactureDate: {vehicle.ManufactureDate}");

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Vehicle with Id: {vehicle.Id} added successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding vehicle with Id: {vehicle.Id}. Exception: {ex.Message}");        
        }     
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to retrieve. Cannot be null or empty.</param>
    /// <returns>A <see cref="Vehicle"/> object representing the vehicle with the specified identifier,
    /// or <see langword="null"/> if no matching vehicle is found.</returns>
    public async Task<Vehicle?> GetByIdAsync(string id, CancellationToken ct = default)
    {

        _logger.LogInformation($"Retrieving vehicle with Id: {id}");

        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);

        _logger.LogInformation($"Retrieving vehicle with Id: {id}. Found: {vehicle != null}");

        return vehicle;
    }

    /// <summary>
    /// Asynchronously counts the total number of vehicles that match the specified filters.
    /// </summary>
    /// <param name="ct">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the total number of vehicles.</returns>
    public async Task<int> CountAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Counting Total vehicles.");

        IQueryable<Vehicle> query = _context.Vehicles;
       
        var count = await query.CountAsync(ct);
        _logger.LogInformation($"Counted vehicles with filters. Total count: {count}");
        return count;
    }

    /// <summary>
    /// Retrieves a list of all vehicles from the database.
    /// </summary>
    /// <remarks>This method asynchronously fetches all vehicle records from the PostgreSQL database.
    /// The returned list will be empty if no vehicles are found.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="Vehicle"/>
    /// objects representing all vehicles in the database.</returns>
    public async Task<List<Vehicle>> ListAsync(GetAllVehicleRequestDomain getAllVehicleRequest, CancellationToken ct = default)
    {
        _logger.LogInformation("Listing all vehicles.");

        IQueryable<Vehicle> query = _context.Vehicles;

        if (!string.IsNullOrEmpty(getAllVehicleRequest.Id))
        {
            query = query.Where(v => v.Id.Contains(getAllVehicleRequest.Id));
        }
        if (!string.IsNullOrEmpty(getAllVehicleRequest.BrandContains))
        {
            query = query.Where(v => v.Brand.Contains(getAllVehicleRequest.BrandContains));
        }
        if (!string.IsNullOrEmpty(getAllVehicleRequest.ModelContains))
        {
            query = query.Where(v => v.Model.Contains(getAllVehicleRequest.ModelContains));
        }
        if (getAllVehicleRequest.IsRented.HasValue)
        {
            query = query.Where(v => v.IsRented == getAllVehicleRequest.IsRented.Value);
        }
        if (!string.IsNullOrEmpty(getAllVehicleRequest.SortedBy))
        {
            query = getAllVehicleRequest.Descending == true
                ? query.OrderByDescending(v => EF.Property<object>(v, getAllVehicleRequest.SortedBy))
                : query.OrderBy(v => EF.Property<object>(v, getAllVehicleRequest.SortedBy));
        }
       
        var pagedVehicles = await query
        .Skip((getAllVehicleRequest.Page - 1) * getAllVehicleRequest.PageSize)
        .Take(getAllVehicleRequest.PageSize)
        .ToListAsync(ct);

        _logger.LogInformation($"Listing vehicles. Total count: {pagedVehicles.Count}");
        return pagedVehicles;
    }

    /// <summary>
    /// Updates the specified vehicle in the database asynchronously.
    /// </summary>
    /// <remarks>This method updates the existing database record that matches the <see cref="Vehicle.Id"/>
    /// of the provided <paramref name="vehicle"/>. Ensure that the <paramref name="vehicle"/>
    /// object contains valid data before calling this method.</remarks>
    /// <param name="vehicle">The vehicle to update. The <see cref="Vehicle.Id"/> property must match an existing record in the database.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    public async Task UpdateAsync(Vehicle vehicle, CancellationToken ct = default)
    {
        _logger.LogInformation($"Updating vehicle with Id: {vehicle.Id}, Model: {vehicle.Model}, ManufactureDate: {vehicle.ManufactureDate}");

        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Vehicle with Id: {vehicle.Id} updated successfully.");
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