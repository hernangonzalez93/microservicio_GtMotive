using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Dtos;

public record VehicleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the vehicle.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the brand of the vehicle.
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Gets or sets the model of the vehicle.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the manufacture date of the vehicle.
    /// </summary>
    public DateTime ManufactureDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the vehicle is currently rented.
    /// </summary>
    public bool IsRented { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the person who rented the vehicle.
    /// </summary>
    public string? RentedBy { get; set; }
}
