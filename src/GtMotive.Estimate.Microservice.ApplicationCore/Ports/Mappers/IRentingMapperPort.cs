using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers
{
    /// <summary>
    /// Interface IRentingService.
    /// </summary>
    public interface IRentingMapperPort
    {
        /// <summary>
        /// Get the list of all rentings <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Renting.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingsAllAsync();

        /// <summary>
        /// Get one Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingId">rentingId.</param>
        /// <returns>Renting.</returns>
        Task<RentingCustomerVehicleDto> GetRentingByIdAsync(int rentingId);

        /// <summary>
        /// ConvertDtoToRenting.
        /// </summary>
        /// <param name="rentingNewDto">rentingNewDto.</param>
        /// <returns>RentingDto.</returns>
        RentingDto ConvertDtoToRenting(RentingNewDto rentingNewDto);

        /// <summary>
        /// Get one Renting where they are still active and dont return vehicle <see cref="Renting"/>.
        /// </summary>
        /// <param name="vehicleId">rentingId.</param>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingByVehicleIdAsync(int vehicleId);

        /// <summary>
        /// Get the list of Rentings where vehicles are active in the fleet <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingsVehicleActiveAsync();

        /// <summary>
        /// Get list of Rentings where vehicles are no active in the fleet <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingsVehicleNoActiveAsync();

        /// <summary>
        /// Add Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingDto">The unique identifier.</param>
        /// <returns>Renting.</returns>
        Task<RentingDto> AddRentingAsync(RentingDto rentingDto);

        /// <summary>
        /// Update Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingDto">The unique identifier.</param>
        /// <returns>Renting.</returns>
        Task<RentingDto> UpdateRentingAsync(RentingDto rentingDto);

        /// <summary>
        /// Update Renting, end of renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingId">The unique identifier.</param>
        /// <param name="dateEnd">dateEnd.</param>
        /// <returns>Renting.</returns>
        Task<RentingCustomerVehicleDto> UpdateRentingCloseAsync(int rentingId, DateTime dateEnd);

        /// <summary>
        /// Delete Renting <see cref="Renting"/>.
        /// </summary>
        /// <param name="rentingId">The unique identifier.</param>
        /// <returns>task.</returns>
        Task DeleteRentingAsync(int rentingId);

        /// <summary>
        /// ValidateRentingDates.
        /// </summary>
        /// <param name="vehicleId">vehicleId.</param>
        /// <param name="dateStart">dateStart.</param>
        /// <returns>(bool result, string message).</returns>
        Task<List<Renting>> ValidateCanRentingWithVehicleIdAsync(int vehicleId, DateTime dateStart);

        /// <summary>
        /// GetPrice.
        /// </summary>
        /// <param name="date1">date1.</param>
        /// <param name="date2">date2.</param>
        /// <returns>Price.</returns>
        Task<double> GetPrice(DateTime date1, DateTime date2);
    }
}
