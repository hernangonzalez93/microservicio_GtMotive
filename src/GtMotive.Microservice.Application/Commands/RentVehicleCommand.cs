using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Application.Dtos.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Commands;

public record RentVehicleCommand(string VehicleId, string PersonId) : IRequest<Result<string>>;

