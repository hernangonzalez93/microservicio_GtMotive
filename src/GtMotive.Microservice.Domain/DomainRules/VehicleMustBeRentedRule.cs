using GtMotive.Microservice.Domain.DomainContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.DomainRules;

public class VehicleMustBeRentedRule : IDomainRule
{
    private readonly bool _isRented;

    /// <summary>
    /// Initializes a new instance of the VehicleMustBeRentedRule class with the specified rental status.
    /// </summary>
    /// <param name="isRented">true if the vehicle is currently rented; otherwise, false.</param>
    public VehicleMustBeRentedRule(bool isRented)
    {
        _isRented = isRented;
    }

    public bool IsSatisfied() => _isRented;
    public string ErrorMessage => "Vehicle is not currently rented.";
}
