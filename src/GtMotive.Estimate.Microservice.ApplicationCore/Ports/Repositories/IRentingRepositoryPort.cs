using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories
{
    /// <summary>
    /// Interface IRentingRepository.
    /// </summary>
    public interface IRentingRepositoryPort
    {
        /// <summary>
        /// Get the list of all rentings <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Renting.</returns>
        Task<List<Renting>> GetRentingsAllAsync();

        /// <summary>
        /// Get one Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingId">Date when Renting Stat.</param>
        /// <returns>Renting.</returns>
        Task<Renting> GetRentingByIdAsync(int rentingId);

        /// <summary>
        /// Get one Rentings per vehicleId <see cref="Renting"/>.
        /// </summary>
        /// <param name="vehicleId">vehicleId.</param>
        /// <returns>list of Rentings.</returns>
        Task<List<Renting>> GetRentingByVehicleIdAsync(int vehicleId);

        /// <summary>
        /// Get the list of Rentings where vehicles are active in the fleet <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        Task<List<Renting>> GetRentingsVehicleActiveAsync();

        /// <summary>
        /// Get list of Rentings where vehicles are no active in the fleet <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        Task<List<Renting>> GetRentingsVehicleNoActiveAsync();

        /// <summary>
        /// Add Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="renting">Values.</param>
        /// <returns>Renting.</returns>
        Task<Renting> AddRentingAsync(Renting renting);

        /// <summary>
        /// Update Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="renting">Values.</param>
        /// <param name="dateEnd">dateEnd.</param>
        /// <returns>Renting.</returns>
        Task<Renting> UpdateRentingAsync(Renting renting, DateTime dateEnd);

        /// <summary>
        /// Resolve problems for future rentings.
        /// </summary>
        /// /// <param name="renting">parameters.</param>
        /// /// <param name="dto">dto.</param>
        /// <returns>bool.</returns>
        Task<bool> ExistsFutureRentingAsync(RentingCustomerVehicleDto renting, RentingDto dto);

        /// <summary>
        /// Delete Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingId">The unique identifier of the renting to be deleted.</param>
        /// <returns>task.</returns>
        Task DeleteRentingAsync(int rentingId);
    }
}
