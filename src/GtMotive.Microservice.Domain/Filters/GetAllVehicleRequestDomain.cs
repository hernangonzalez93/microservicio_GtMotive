using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.Filters;

public class GetAllVehicleRequestDomain
{
    public string? Id { get; set; }

    public string? BrandContains { get; set; } 

    public string? ModelContains { get; set; } 

    public bool? IsRented { get; set; }

    public string? SortedBy { get; set; } 

    public bool? Descending { get; set; } 

    public int Page { get; set; } 

    public int PageSize { get; set; } 
}