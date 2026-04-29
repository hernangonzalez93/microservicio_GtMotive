using GtMotive.Microservice.Domain.DomainContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.DomainRules;

public class VehicleMustBeAvailableRule : IDomainRule
{
    private readonly bool _isRented;

    /// <summary>
    /// Initializes a new instance of the VehicleMustBeAvailableRule class based on the vehicle's rental status.
    /// </summary>
    /// <param name="isRented">true if the vehicle is currently rented; otherwise, false.</param>
    public VehicleMustBeAvailableRule(bool isRented)
    {
        _isRented = isRented;
    }

    public bool IsSatisfied() => !_isRented;
    public string ErrorMessage => "Vehicle is already rented.";
}