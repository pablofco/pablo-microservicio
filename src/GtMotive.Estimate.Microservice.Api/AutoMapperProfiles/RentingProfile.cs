using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.Api.AutoMapperProfiles
{
    public class RentingProfile : Profile
    {
        public RentingProfile()
        {
            CreateMap<Renting, RentingCustomerVehicleDto>().ReverseMap();
            CreateMap<Renting, RentingDto>().ReverseMap();
            CreateMap<RentingDto, RentingNewDto>().ReverseMap();
        }
    }
}
