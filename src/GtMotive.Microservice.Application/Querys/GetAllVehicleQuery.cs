using GtMotive.Microservice.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Querys;

public record GetAllVehicleQuery(GetAllVehicleRequest GetAllVehicleFilter) : IRequest<List<VehicleResponse>>;
