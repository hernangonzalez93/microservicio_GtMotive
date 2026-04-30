using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Dtos;

public  record RentVehicleRequest()
{
    /// <summary>
    /// Represents a request to rent a vehicle, including the identifier of the person making the request.
    /// </summary>
    [Required(ErrorMessage = "PersonId is mandatory to rent a vehicle")]
    public string PersonId { get; set; }
}