using GtMotive.Microservice.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Test.Infrastructure;

public  class VehicleControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public VehicleControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Validates that the vehicle creation request fails when the model is missing or invalid.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Should_Validate_Model_On_Create()
    {
        // Arrange
        var client = _factory.CreateClient();

        var request = new CreateVehicleRequest
        {
            Model = "Smart",
            Brand = "Yris",
            ManufactureDate = DateTime.UtcNow.AddYears(-10)
        };

        var reqSeri = System.Text.Json.JsonSerializer.Serialize(request);

        ///Serialize the request to JSON
        var content = new StringContent(reqSeri, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/vehicles", content);

        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }
}
