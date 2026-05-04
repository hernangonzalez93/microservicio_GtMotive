using GtMotive.Microservice.Domain.DomainRules;
using GtMotive.Microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.DomainServices;

public interface IVehicleDomainService
{
    /// <summary>
    /// Validates that the specified vehicle is available for rent.
    /// </summary>
    /// <param name="vehicle">The vehicle to check for rental availability.</param>
    string ValidateRent(Vehicle vehicle);


    /// <summary>
    /// Validates a vehicle can be returned, ensuring it is currently rented before allowing the return process to proceed.
    /// </summary>
    /// <param name="vehicle"></param>
    string ValidateReturn(Vehicle vehicle);
   
}