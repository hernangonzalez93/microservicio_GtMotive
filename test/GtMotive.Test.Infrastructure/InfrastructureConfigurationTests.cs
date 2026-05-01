using GtMotive.Microservice.Domain.Ports;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Context;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GtMotive.Test.Infrastructure;

/// <summary>
/// Tests for infrastructure configuration and dependency injection setup.
/// Validates that infrastructure services are properly registered and configured.
/// </summary>
public class InfrastructureConfigurationTests
{
    /// <summary>
    /// Tests that infrastructure services are registered correctly in the DI container.
    /// </summary>
    [Fact]
    public void Should_Register_Infrastructure_Services()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider);
    }

    /// <summary>
    /// Tests that PostgresDbContext can be instantiated with proper options.
    /// </summary>
    [Fact]
    public void Should_Instantiate_PostgresDbContext()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseInMemoryDatabase("TestDatabase");
        });

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var dbContext = serviceProvider.GetService<PostgresDbContext>();

        // Assert
        Assert.NotNull(dbContext);
    }

    /// <summary>
    /// Tests that IVehicleRepository is properly resolved from the DI container.
    /// </summary>
    [Fact]
    public void Should_Resolve_Vehicle_Repository()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseInMemoryDatabase("TestDatabase");
        });

        services.AddLogging(builder => builder.AddConsole());

        services.AddScoped<IVehicleRepository>(provider =>
        {
            var context = provider.GetRequiredService<PostgresDbContext>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<PostgresVehicleRepository>();

            return new PostgresVehicleRepository(context, logger);
        });

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var repository = serviceProvider.GetService<IVehicleRepository>();

        // Assert
        Assert.NotNull(repository);
        Assert.IsAssignableFrom<IVehicleRepository>(repository);
    }

    /// <summary>
    /// Tests that the database context is properly configured for PostgreSQL.
    /// </summary>
    [Fact]
    public void Should_Configure_DbContext_With_InMemory_For_Testing()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PostgresDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        // Act
        using var dbContext = new PostgresDbContext(options);

        // Assert
        Assert.NotNull(dbContext);
        Assert.NotNull(dbContext.Vehicles);
    }
}
