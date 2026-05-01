using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Handlers;
using GtMotive.Microservice.Domain.Ports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Test.Unit;

public class RentVehicleUseCaseTests
{
    [Fact]
    public async Task Should_Throw_If_Person_Already_Has_Vehicle()
    {
        // Arrange
        var repositoryMock = new Mock<IVehicleRepository>();
        // repositoryMock.Setup(r => r.HasPersonRentedVehicleAsync(It.IsAny<string>())).ReturnsAsync(true);
        repositoryMock.Setup(r => r.HasPersonRentedVehicleAsync("63214789H")).ReturnsAsync(true);
        var handler = new RentVehicleCommandHandler(repositoryMock.Object, Mock.Of<Microsoft.Extensions.Logging.ILogger<RentVehicleCommandHandler>>());
        var command = new RentVehicleCommand("913f36ea-c52c-4566-a6a9-4d3613f3dda3", "63214789H");
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Person already has vehicle rented", result.Error);

    }
}
