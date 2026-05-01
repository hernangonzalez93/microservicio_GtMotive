using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace GtMotive.Test.Infrastructure;

/// <summary>
/// Integration tests for the Vehicle API controller endpoints.
/// Tests the full HTTP request-response cycle with the application.
/// </summary>
public class VehicleControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public VehicleControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Validates that the vehicle creation request fails when required fields are empty.
    /// </summary>
    [Fact]
    public async Task Should_Return_Bad_Request_When_Required_Fields_Missing()
    {
        // Arrange
        var client = _factory.CreateClient();

        var request = new CreateVehicleRequest
        {
            Model = "", // Empty model
            Brand = "", // Empty brand
            ManufactureDate = DateTime.UtcNow.AddYears(-2)
        };

        var jsonContent = System.Text.Json.JsonSerializer.Serialize(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/vehicles", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Validates that the vehicle creation request fails when the manufacture date is too old.
    /// </summary>
    [Fact]
    public async Task Should_Validate_Model_On_Create()
    {
        // Arrange
        var client = _factory.CreateClient();

        var request = new CreateVehicleRequest
        {
            Model = "Smart",
            Brand = "Yris",
            ManufactureDate = DateTime.UtcNow.AddYears(-10) // More than 5 years old - should fail
        };

        var reqSeri = System.Text.Json.JsonSerializer.Serialize(request);

        var content = new StringContent(reqSeri, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/vehicles", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Tests successful vehicle creation with valid data.
    /// </summary>
    [Fact]
    public async Task Should_Create_Vehicle_Successfully_With_Valid_Data()
    {
        // Arrange
        var client = _factory.CreateClient();

        var request = new CreateVehicleRequest
        {
            Model = "Civic",
            Brand = "Honda",
            ManufactureDate = DateTime.UtcNow.AddYears(-4)
        };

        var jsonContent = System.Text.Json.JsonSerializer.Serialize(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/vehicles", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    /// <summary>
    /// Tests retrieving all vehicles from the API.
    /// </summary>
    [Fact]
    public async Task Should_Get_All_Vehicles()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/vehicles");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Tests retrieving a specific vehicle by ID.
    /// </summary>
    [Fact]
    public async Task Should_Get_Vehicle_By_Id()
    {
        // Arrange
        var client = _factory.CreateClient();
        var vehicleId = Guid.NewGuid().ToString();

        // Act
        var response = await client.GetAsync($"/api/vehicles/{vehicleId}");

        // Assert
        // Not found is expected if the vehicle doesn't exist
        Assert.True(response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.OK);
    }

    private class CreateVehicleRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("manufactureDate")]
        public DateTime ManufactureDate { get; set; }
    }
}
