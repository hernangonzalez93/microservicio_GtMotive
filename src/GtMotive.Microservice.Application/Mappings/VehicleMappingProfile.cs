using AutoMapper;
using GtMotive.Microservice.Application.Commands;
using GtMotive.Microservice.Application.Dtos;
using GtMotive.Microservice.Application.Querys;
using GtMotive.Microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Application.Mappings;

public class VehicleMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleMappingProfile"/> class.
    /// </summary>
    public VehicleMappingProfile()
    {
        // Map CreateVehicleRequest to CreateVehicleCommand
        CreateMap<CreateVehicleRequest, CreateVehicleCommand>()
            .ConstructUsing(src => new CreateVehicleCommand(src.Brand, src.Model, src.ManufactureDate));

        // Map Vehicle entity to VehicleResponse DTO
        CreateMap<Vehicle, VehicleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.ManufactureDate, opt => opt.MapFrom(src => src.ManufactureDate))
            .ForMember(dest => dest.IsRented, opt => opt.MapFrom(src => src.IsRented))
            .ForMember(dest => dest.RentedBy, opt => opt.MapFrom(src => src.RentedBy));

        // Map GetAllVehicleRequest to GetAllVehicleQuery
        //CreateMap<GetAllVehicleRequest, GetAllVehicleQuery>()
        //    .ConstructUsing(src => new GetAllVehicleQuery(src));

        //CreateMap<GetAllVehicleRequest, GetAllVehicleQuery>()
        //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //   .ForMember(dest => dest.BrandContains, opt => opt.MapFrom(src => src.BrandContains))
        //   .ForMember(dest => dest.ModelContains, opt => opt.MapFrom(src => src.ModelContains))
        //   .ForMember(dest => dest.IsRented, opt => opt.MapFrom(src => src.IsRented))
        //   .ForMember(dest => dest.SortedBy, opt => opt.MapFrom(src => src.SortedBy))
        //   .ForMember(dest => dest.Descending, opt => opt.MapFrom(src => src.Descending))
        //   .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page))
        //   .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
    }
}
