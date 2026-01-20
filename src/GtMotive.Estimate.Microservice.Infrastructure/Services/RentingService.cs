using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Services;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Services
{
    /// <summary>
    /// RentingService.
    /// </summary>
    public class RentingService(IParameterRepository parameterRepository,
        IRentingRepository rentingRepository,
        IMapper mapper) : IRentingService
    {
        private readonly IParameterRepository _parameterRepository = parameterRepository;
        private readonly IRentingRepository _rentingRepository = rentingRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsAllAsync()
        {
            var rentings = await _rentingRepository.GetRentingsAllAsync();
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<RentingCustomerVehicleDto> GetRentingByIdAsync(int rentingId)
        {
            var renting = await _rentingRepository.GetRentingByIdAsync(rentingId);
            var rentingDto = ServiceHelper.ConvertToDto<Renting, RentingCustomerVehicleDto>(_mapper, renting);

            return rentingDto;
        }

        /// <inheritdoc/>
        public RentingDto ConvertDtoToRenting(RentingNewDto rentingNewDto)
        {
            var rentingDto = ServiceHelper.ConvertToEntity<RentingNewDto, RentingDto>(_mapper, rentingNewDto);

            return rentingDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetStillAliveAsync()
        {
            var rentings = await _rentingRepository.GetRentingsStillAliveAsync();
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingStillAliveByCustomerIdAsync(int customerId)
        {
            var rentings = await _rentingRepository.GetRentingStillAliveByCustomerIdAsync(customerId);
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingByVehicleIdAsync(int vehicleId)
        {
            var rentings = await _rentingRepository.GetRentingByVehicleIdAsync(vehicleId);
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsVehicleActiveAsync()
        {
            var rentings = await _rentingRepository.GetRentingsVehicleActiveAsync();
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsVehicleNoActiveAsync()
        {
            var rentings = await _rentingRepository.GetRentingsVehicleNoActiveAsync();
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsDatesBetweenAsync(DateTime dateBetween)
        {
            var rentings = await _rentingRepository.GetRentingsDatesBetweenAsync(dateBetween);
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<RentingCustomerVehicleDto>> GetRentingsByCustomerIdDatesBetweenAsync(int customerId, DateTime dateBetween)
        {
            var rentings = await _rentingRepository.GetRentingsByCustomerIdDatesBetweenAsync(customerId, dateBetween);
            var rentingsDto = ServiceHelper.ConvertToList<Renting, RentingCustomerVehicleDto>(_mapper, rentings);

            return (List<RentingCustomerVehicleDto>)rentingsDto;
        }

        /// <inheritdoc/>
        public async Task<RentingDto> AddRentingAsync(RentingDto rentingDto)
        {
            var renting = ServiceHelper.ConvertToEntity<RentingDto, Renting>(_mapper, rentingDto);
            renting = await _rentingRepository.AddRentingAsync(renting);
            var rentingDtoAdded = ServiceHelper.ConvertToDto<Renting, RentingDto>(_mapper, renting);

            return rentingDtoAdded;
        }

        /// <inheritdoc/>
        public async Task<RentingDto> UpdateRentingAsync(RentingDto rentingDto)
        {
            var renting = ServiceHelper.ConvertToEntity<RentingDto, Renting>(_mapper, rentingDto);
            renting = await _rentingRepository.UpdateRentingAsync(renting);
            var rentingDtoUpdated = ServiceHelper.ConvertToDto<Renting, RentingDto>(_mapper, renting);

            return rentingDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task<RentingCustomerVehicleDto> UpdateRentingCloseAsync(int rentingId, DateTime dateEnd)
        {
            var rentingDto = await GetRentingByIdAsync(rentingId);
            rentingDto.DateEndReal = dateEnd;
            rentingDto.PriceReal = await GetPrice(rentingDto.DateStart, dateEnd);

            var renting = ServiceHelper.ConvertToEntity<RentingCustomerVehicleDto, Renting>(_mapper, rentingDto);
            renting = await _rentingRepository.UpdateRentingAsync(renting);
            var rentingDtoUpdated = ServiceHelper.ConvertToDto<Renting, RentingCustomerVehicleDto>(_mapper, renting);

            return rentingDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task DeleteRentingAsync(int rentingId)
        {
            await _rentingRepository.DeleteRentingAsync(rentingId);
        }

        /// <inheritdoc/>
        public async Task<(bool ResultStillAlive, List<int> RentingsId)> ValidateRentingStillAliveAsync(int customerId)
        {
            var customerHasRentingsAliveList = await _rentingRepository.GetRentingStillAliveByCustomerIdAsync(customerId);

            return (customerHasRentingsAliveList.Count > 0, customerHasRentingsAliveList.Select(r => r.RentingId).ToList());
        }

        /// <inheritdoc/>
        public (bool Result, string Message) ValidateRentingDates(DateTime dateStart, DateTime? dateEnd)
        {
            if (dateEnd == null)
            {
                return (true, "The renting dates are valid.");
            }

            return dateStart > dateEnd
                ? ((bool Result, string Message))(false, "The start date cannot be later than the end date.")
                : ((bool Result, string Message))(true, "The renting dates are valid.");
        }

        /// <inheritdoc/>
        public async Task<(bool Result, string Message)> ValidateCanRentingWithVehicleIdAsync(int vehicleId, DateTime dateStart)
        {
            var rentings = await _rentingRepository.GetRentingByVehicleIdAsync(vehicleId);

            var result = rentings.Where(r => dateStart >= r.DateStart && r.DateEndReal == null).ToList();
            if (result.Count > 0)
            {
                return (false, $"The vehicle is already rented and has no end date, RentingsIds:{string.Join(", ", result.Select(r => r.RentingId).ToList())}");
            }

            result = rentings.Where(r => dateStart >= r.DateStart && r.DateEndReal != null && dateStart <= r.DateEndReal).ToList();
            if (result.Count > 0)
            {
                return (false, $"The vehicle is already rented during the specified dates. RentingsIds: {string.Join(", ", result.Select(r => r.RentingId).ToList())}");
            }

            return (true, "The vehicle is available for renting.");
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
