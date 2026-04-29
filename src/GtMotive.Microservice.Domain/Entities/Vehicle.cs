using GtMotive.Microservice.Domain.ValueObjects;
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
    public ManufactureDate ManufactureDate { get; init; }

    /// <summary>
    /// Is the vehicle currently rented?
    /// </summary>
    public bool IsRented { get; private set; }

    /// <summary>
    ///  Identifier of the person who rented the vehicle, if applicable.
    /// </summary>
    public string? RentedBy { get; private set; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Vehicle"/> class. EF
    /// </summary>
    public Vehicle()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="brand"></param>
    /// <param name="model"></param>
    /// <param name="manufactureDate"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public Vehicle(string brand, string model, ManufactureDate manufactureDate)
    {
        Id = Guid.NewGuid().ToString();
        Brand = brand;
        Model = model;
        ManufactureDate = manufactureDate; 
        IsRented = false;
    }

    /// <summary>
    /// Rents the vehicle to a person identified by <paramref name="personId"/>.
    /// </summary>
    /// <param name="personId"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Rent(string personId)
    {
        IsRented = true;
        RentedBy = personId;
    }

    /// <summary>
    /// Returns the vehicle, making it available for rent again.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Return()
    {      
        IsRented = false;
        RentedBy = null;
    }
}