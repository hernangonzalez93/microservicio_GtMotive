using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.Entities;

public class Vehicle
{
    /// <summary>
    /// Identifier for the vehicle.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Brand of the vehicle.
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Model of the vehicle.
    /// </summary>
    public string Model { get; init; }

    /// <summary>
    /// Manufacture date of the vehicle.
    /// </summary>
    public DateTime ManufactureDate { get; init; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Vehicle"/> class.
    /// </summary>
    public Vehicle()
    {

    }
}
