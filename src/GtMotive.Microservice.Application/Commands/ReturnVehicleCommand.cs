using GtMotive.Microservice.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Commands;

public record ReturnVehicleCommand (string VehicleId) : IRequest<Result<string>>;
