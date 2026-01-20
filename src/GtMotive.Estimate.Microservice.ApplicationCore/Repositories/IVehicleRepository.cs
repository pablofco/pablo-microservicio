using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Repositories
{
    /// <summary>
    /// Interface IVehicleRepository.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Get the list of all Vehicles./>.
        /// </summary>
        /// <returns>lisr of Vehicles.</returns>
        Task<List<Vehicle>> GetVehiclesAllAsync();

        /// <summary>
        /// Get one Vehicle.
        /// </summary>
        /// <param name="vehicleId">id of Vehicle.</param>
        /// <returns>Vehicle.</returns>
        Task<Vehicle> GetVehicleByIdAsync(int vehicleId);

        /// <summary>
        /// Get one Vehicle per number (placa)./>.
        /// </summary>
        /// <param name="numberId">The unique identifier of the vehicle (placa).</param>
        /// <returns>Vehicle.</returns>
        Task<Vehicle> GetVehicleByNumberIdAsync(string numberId);

        /// <summary>
        /// Add Vehicle <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicle">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>Vehicle.</returns>
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Update Vehicle <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicle">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>Vehicle.</returns>
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Update Vehicles <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicles">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>task.</returns>
        Task UpdateVehiclesAsync(IList<Vehicle> vehicles);

        /// <summary>
        /// Update Vehicle to Active in the fleet. <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>Vehicle.</returns>
        Task<Vehicle> UpdateVehicleByIdToActiveAsync(int vehicleId);

        /// <summary>
        /// Update Vehicle to no Active in the fleet <see cref="Vehicle"/>.
        /// </summary>
        /// <returns>list of vehicles that will be updated to no active in the fleet.</returns>
        Task<List<Vehicle>> UpdateVehiclesToNoActiveAsync();

        /// <summary>
        /// Delete Vehicle <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be deleted.</param>
        /// <returns>task.</returns>
        Task DeleteVehicleAsync(int vehicleId);
    }
}
