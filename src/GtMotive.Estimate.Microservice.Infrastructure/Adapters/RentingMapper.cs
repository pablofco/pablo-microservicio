using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Helpers;

namespace GtMotive.Estimate.Microservice.Infrastructure.Adapters
{
    /// <summary>
    /// RentingService.
    /// </summary>
    public class RentingMapper(IParameterRepositoryPort parameterRepository,
        IRentingRepositoryPort rentingRepository,
        IMapper mapper) : IRentingMapperPort
    {
        private readonly IParameterRepositoryPort _parameterRepository = parameterRepository;
        private readonly IRentingRepositoryPort _rentingRepository = rentingRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsAllAsync()
        {
            var rentings = await _rentingRepository.GetRentingsAllAsync();
            var rentingsDto = AdapterHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<RentingCustomerVehicleDto> GetRentingByIdAsync(int rentingId)
        {
            var renting = await _rentingRepository.GetRentingByIdAsync(rentingId);
            var rentingDto = AdapterHelper.ConvertToDto<Renting, RentingCustomerVehicleDto>(_mapper, renting);

            return rentingDto;
        }

        /// <inheritdoc/>
        public RentingDto ConvertDtoToRenting(RentingNewDto rentingNewDto)
        {
            var rentingDto = AdapterHelper.ConvertToEntity<RentingNewDto, RentingDto>(_mapper, rentingNewDto);

            return rentingDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingByVehicleIdAsync(int vehicleId)
        {
            var rentings = await _rentingRepository.GetRentingByVehicleIdAsync(vehicleId);
            var rentingsDto = AdapterHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsVehicleActiveAsync()
        {
            var rentings = await _rentingRepository.GetRentingsVehicleActiveAsync();
            var rentingsDto = AdapterHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsVehicleNoActiveAsync()
        {
            var rentings = await _rentingRepository.GetRentingsVehicleNoActiveAsync();
            var rentingsDto = AdapterHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<RentingDto> AddRentingAsync(RentingDto rentingDto)
        {
            var renting = AdapterHelper.ConvertToEntity<RentingDto, Renting>(_mapper, rentingDto);
            renting = await _rentingRepository.AddRentingAsync(renting);
            var rentingDtoAdded = AdapterHelper.ConvertToDto<Renting, RentingDto>(_mapper, renting);

            return rentingDtoAdded;
        }

        /// <inheritdoc/>
        public async Task<RentingDto> UpdateRentingAsync(RentingDto rentingDto)
        {
            var renting = AdapterHelper.ConvertToEntity<RentingDto, Renting>(_mapper, rentingDto);
            renting = await _rentingRepository.UpdateRentingAsync(renting);
            var rentingDtoUpdated = AdapterHelper.ConvertToDto<Renting, RentingDto>(_mapper, renting);

            return rentingDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task<RentingCustomerVehicleDto> UpdateRentingCloseAsync(int rentingId, DateTime dateEnd)
        {
            var rentingDto = await GetRentingByIdAsync(rentingId);
            rentingDto.DateEndReal = dateEnd;
            rentingDto.PriceReal = await GetPrice(rentingDto.DateStart, dateEnd);

            var renting = AdapterHelper.ConvertToEntity<RentingCustomerVehicleDto, Renting>(_mapper, rentingDto);
            renting = await _rentingRepository.UpdateRentingAsync(renting);
            var rentingDtoUpdated = AdapterHelper.ConvertToDto<Renting, RentingCustomerVehicleDto>(_mapper, renting);

            return rentingDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task DeleteRentingAsync(int rentingId)
        {
            await _rentingRepository.DeleteRentingAsync(rentingId);
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> ValidateCanRentingWithVehicleIdAsync(int vehicleId, DateTime dateStart)
        {
            var rentings = await _rentingRepository.GetRentingByVehicleIdAsync(vehicleId);

            return rentings;
        }

        /// <inheritdoc/>
        public async Task<double> GetPrice(DateTime date1, DateTime date2)
        {
            var parameters = await _parameterRepository.GetParametersAsync();

            var dif = date2.Subtract(date1).TotalDays;
            if (dif == 0)
            {
                dif = 1;
            }

            return parameters.PreciPerDay * dif;
        }
    }
}
