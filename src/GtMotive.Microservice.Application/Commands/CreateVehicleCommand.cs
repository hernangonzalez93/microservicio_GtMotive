using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Commands;

public record CreateVehicleCommand : IRequest<Result<string>>
{
    /// <summary>
    /// Gets the brand of the vehicle to be created.
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Gets the model of the vehicle to be created.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets the manufacture date of the vehicle to be created.
    /// </summary>
    public ManufactureDate ManufactureDate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVehicleCommand"/> class.
    /// </summary>
    /// <param name="brand">The brand of the vehicle.</param>
    /// <param name="model">The model of the vehicle.</param>
    /// <param name="manufactureDate">The manufacture date of the vehicle.</param>
    public CreateVehicleCommand(string brand, string model, DateTime manDate)
    {
        var manufactureDate = new ManufactureDate(manDate); 

        Brand = brand;
        Model = model;
        ManufactureDate = manufactureDate;
    }
}
