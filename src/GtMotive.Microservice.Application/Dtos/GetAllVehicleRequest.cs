using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Dtos;

public record GetAllVehicleRequest
{
    public string? Id { get; set; } = null;

    public string? BrandContains { get; set; } = null;

    public string? ModelContains { get; set; } = null;

    public bool? IsRented { get; set; } = false;

    public string? SortedBy { get; set; } = "Id";

    public bool? Descending { get; set; } = false;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
