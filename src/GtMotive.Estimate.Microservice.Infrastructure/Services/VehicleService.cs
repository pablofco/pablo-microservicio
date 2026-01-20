using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.Services;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.Infrastructure.Services
{
    /// <summary>
    /// VehicleService.
    /// </summary>
    public class VehicleService(IVehicleRepository vehicleRepository,
        IMapper mapper) : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<List<VehicleDto>> GetVehiclesAllAsync()
        {
            var vehicles = await _vehicleRepository.GetVehiclesAllAsync();
            var vehiclesDto = ServiceHelper.ConvertToList<Vehicle, VehicleDto>(_mapper, vehicles);

            return (List<VehicleDto>)vehiclesDto;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> GetVehicleByIdAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            var vehicleDto = ServiceHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDto;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> GetVehicleByNumberIdAsync(string numberId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByNumberIdAsync(numberId);
            var vehicleDto = ServiceHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDto;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> AddVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = ServiceHelper.ConvertToEntity<VehicleDto, Vehicle>(_mapper, vehicleDto);
            vehicle = await _vehicleRepository.AddVehicleAsync(vehicle);
            var vehicleDtoAdded = ServiceHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDtoAdded;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> UpdateVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = ServiceHelper.ConvertToEntity<VehicleDto, Vehicle>(_mapper, vehicleDto);
            vehicle = await _vehicleRepository.UpdateVehicleAsync(vehicle);
            var vehicleDtoUpdated = ServiceHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task UpdateVehiclesAsync(IList<VehicleDto> vehiclesDto)
        {
            var vehicles = ServiceHelper.ConvertToList<VehicleDto, Vehicle>(_mapper, vehiclesDto);
            await _vehicleRepository.UpdateVehiclesAsync(vehicles);
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> UpdateVehicleByIdToActiveAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.UpdateVehicleByIdToActiveAsync(vehicleId);
            var vehicleDtoUpdated = ServiceHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task<List<VehicleDto>> UpdateVehiclesToNoActiveAsync()
        {
            var vehicles = await _vehicleRepository.UpdateVehiclesToNoActiveAsync();
            var vehiclesDto = ServiceHelper.ConvertToList<Vehicle, VehicleDto>(_mapper, vehicles);

            return (List<VehicleDto>)vehiclesDto;
        }

        /// <inheritdoc/>
        public async Task DeleteVehicleAsync(int vehicleId)
        {
            await _vehicleRepository.DeleteVehicleAsync(vehicleId);
        }

        /// <inheritdoc/>
        public bool ValidateColor(VehicleDto vehicleDto)
        {
            return ServiceHelper.ValidateColor(vehicleDto);
        }

        /// <inheritdoc/>
        public bool ValidatePort(VehicleDto vehicleDto)
        {
            return ServiceHelper.ValidatePort(vehicleDto);
        }
    }
}
