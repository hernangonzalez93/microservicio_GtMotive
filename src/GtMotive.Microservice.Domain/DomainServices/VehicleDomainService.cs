using GtMotive.Microservice.Domain.DomainContracts;
using GtMotive.Microservice.Domain.DomainRules;
using GtMotive.Microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.DomainServices;

public class VehicleDomainService
{
    
    /// <summary>
    /// Validates that the specified vehicle is available for rent.
    /// </summary>
    /// <param name="vehicle">The vehicle to check for rental availability.</param>
    public void ValidateRent(Vehicle vehicle)
    {
        CheckRule(new VehicleMustBeAvailableRule(vehicle.IsRented));
    }

    /// <summary>
    /// Validates a vehicle can be returned, ensuring it is currently rented before allowing the return process to proceed.
    /// </summary>
    /// <param name="vehicle"></param>
    public void ValidateReturn(Vehicle vehicle)
    {
        CheckRule(new VehicleMustBeRentedRule(vehicle.IsRented));
    }

    /// <summary>
    /// Checks the specified domain rule and throws an <see cref="InvalidOperationException"/> if the rule is not satisfied, using the rule's error message for the exception.
    /// </summary>
    /// <param name="rule"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void CheckRule(IDomainRule rule)
    {
        if (!rule.IsSatisfied())
            throw new InvalidOperationException(rule.ErrorMessage);
    }
}
