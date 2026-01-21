using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers
{
    /// <summary>
    /// Interface IVehicleService.
    /// </summary>
    public interface IVehicleMapperPort
    {
        /// <summary>
        /// Get the list of all Vehicles./>.
        /// </summary>
        /// <returns>lisr of Vehicles.</returns>
        Task<List<VehicleDto>> GetVehiclesAllAsync();

        /// <summary>
        /// Get one Vehicle.
        /// </summary>
        /// <param name="vehicleId">id of Vehicle.</param>
        /// <returns>Vehicle.</returns>
        Task<VehicleDto> GetVehicleByIdAsync(int vehicleId);

        /// <summary>
        /// Get one Vehicle per number (placa)./>.
        /// </summary>
        /// <param name="numberId">The unique identifier of the vehicle (placa).</param>
        /// <returns>Vehicle.</returns>
        Task<VehicleDto> GetVehicleByNumberIdAsync(string numberId);

        /// <summary>
        /// Add Vehicle <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicleDto">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>Vehicle.</returns>
        Task<VehicleDto> AddVehicleAsync(VehicleDto vehicleDto);

        /// <summary>
        /// Update Vehicle <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicleDto">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>Vehicle.</returns>
        Task<VehicleDto> UpdateVehicleAsync(VehicleDto vehicleDto);

        /// <summary>
        /// Update Vehicles <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehiclesDto">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>task.</returns>
        Task UpdateVehiclesAsync(IList<VehicleDto> vehiclesDto);

        /// <summary>
        /// Update Vehicle to Active in the fleet. <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>task.</returns>
        Task<VehicleDto> UpdateVehicleByIdToActiveAsync(int vehicleId);

        /// <summary>
        /// Update Vehicle to no Active in the fleet <see cref="Vehicle"/>.
        /// </summary>
        /// <returns>list of vehicles that will be updated to no active in the fleet.</returns>
        Task<List<VehicleDto>> UpdateVehiclesToNoActiveAsync();

        /// <summary>
        /// Delete Vehicle <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>task.</returns>
        Task DeleteVehicleAsync(int vehicleId);

        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist  Color.</returns>
        bool ValidateColor(VehicleDto vehicleDto);

        /// <summary>
        /// ValidatePort.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist  Port.</returns>
        bool ValidatePort(VehicleDto vehicleDto);
    }
}
