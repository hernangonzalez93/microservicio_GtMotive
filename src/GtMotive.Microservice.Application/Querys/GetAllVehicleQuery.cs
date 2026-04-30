using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Application.Dtos.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Querys;

public record GetAllVehicleQuery(GetAllVehicleRequest GetAllVehicleFilter) : IRequest<Result<PagedResult<VehicleResponse>>>;
