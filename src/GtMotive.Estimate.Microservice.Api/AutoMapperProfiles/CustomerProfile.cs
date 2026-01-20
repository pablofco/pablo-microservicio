using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.Api.AutoMapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Customer, CustomerRentingsDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Rentings, opt => opt.MapFrom(src => src.Rentings));
        }
    }
}
