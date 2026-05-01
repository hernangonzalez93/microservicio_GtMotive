using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Handlers;
using GtMotive.Microservice.Domain.Entities;
using GtMotive.Microservice.Domain.Filters;
using GtMotive.Microservice.Domain.Ports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Test.Functional;

public class AddVehicleUseCaseTests
{
    /// <summary>
    /// Vehicle repository for testing purposes.
    /// </summary>
    public class MockVehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles = new();

        public Task AddAsync(Vehicle vehicle, CancellationToken ct)
        {
            _vehicles.Add(vehicle);
            return Task.CompletedTask;
        }

        public Task<Vehicle?> GetByIdAsync(string id, CancellationToken ct) =>
            Task.FromResult(_vehicles.FirstOrDefault(v => v.Id == id));

        public Task<List<Vehicle>> ListAsync(GetAllVehicleRequestDomain getAllVehicleRequestDomain, CancellationToken ct = default) => Task.FromResult(_vehicles.ToList());

        public Task UpdateAsync(Vehicle vehicle, CancellationToken ct)
        {
            var index = _vehicles.FindIndex(v => v.Id == vehicle.Id);
            if (index >= 0) _vehicles[index] = vehicle;
            return Task.CompletedTask;
        }

        public Task<bool> HasPersonRentedVehicleAsync(string personId) =>
            Task.FromResult(_vehicles.Any(v => v.RentedBy == personId && v.IsRented));

        public Task<int> CountAsync(CancellationToken ct = default)
        {
            return Task.FromResult(_vehicles.Count);
        }
    }


    /// <summary>
    /// Tests the functionality of adding a vehicle to the fleet />
    /// implementation.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Should_Add_Vehicle_To_Fleet()
    {
        // Arrange
        var repository = new MockVehicleRepository();
        var handler = new CreateVehicleCommandHandler(repository, Mock.Of<Microsoft.Extensions.Logging.ILogger<CreateVehicleCommandHandler>>());
        var command = new CreateVehicleCommand("Tesla",  "Model 3", new DateTime(2023, 1, 1));
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, await repository.CountAsync());

    }
}
