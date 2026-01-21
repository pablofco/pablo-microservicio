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
    /// VehicleService.
    /// </summary>
    public class VehicleMapper(IVehicleRepositoryPort vehicleRepository,
        IMapper mapper) : IVehicleMapperPort
    {
        private readonly IVehicleRepositoryPort _vehicleRepository = vehicleRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<List<VehicleDto>> GetVehiclesAllAsync()
        {
            var vehicles = await _vehicleRepository.GetVehiclesAllAsync();
            var vehiclesDto = AdapterHelper.ConvertToList<Vehicle, VehicleDto>(_mapper, vehicles);

            return (List<VehicleDto>)vehiclesDto;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> GetVehicleByIdAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            var vehicleDto = AdapterHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDto;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> GetVehicleByNumberIdAsync(string numberId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByNumberIdAsync(numberId);
            var vehicleDto = AdapterHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDto;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> AddVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = AdapterHelper.ConvertToEntity<VehicleDto, Vehicle>(_mapper, vehicleDto);
            vehicle = await _vehicleRepository.AddVehicleAsync(vehicle);
            var vehicleDtoAdded = AdapterHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDtoAdded;
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> UpdateVehicleAsync(VehicleDto vehicleDto)
        {
            var vehicle = AdapterHelper.ConvertToEntity<VehicleDto, Vehicle>(_mapper, vehicleDto);
            vehicle = await _vehicleRepository.UpdateVehicleAsync(vehicle);
            var vehicleDtoUpdated = AdapterHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task UpdateVehiclesAsync(IList<VehicleDto> vehiclesDto)
        {
            var vehicles = AdapterHelper.ConvertToList<VehicleDto, Vehicle>(_mapper, vehiclesDto);
            await _vehicleRepository.UpdateVehiclesAsync(vehicles);
        }

        /// <inheritdoc/>
        public async Task<VehicleDto> UpdateVehicleByIdToActiveAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.UpdateVehicleByIdToActiveAsync(vehicleId);
            var vehicleDtoUpdated = AdapterHelper.ConvertToDto<Vehicle, VehicleDto>(_mapper, vehicle);

            return vehicleDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task<List<VehicleDto>> UpdateVehiclesToNoActiveAsync()
        {
            var vehicles = await _vehicleRepository.UpdateVehiclesToNoActiveAsync();
            var vehiclesDto = AdapterHelper.ConvertToList<Vehicle, VehicleDto>(_mapper, vehicles);

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
            return AdapterHelper.ValidateColor(vehicleDto);
        }

        /// <inheritdoc/>
        public bool ValidatePort(VehicleDto vehicleDto)
        {
            return AdapterHelper.ValidatePort(vehicleDto);
        }
    }
}
