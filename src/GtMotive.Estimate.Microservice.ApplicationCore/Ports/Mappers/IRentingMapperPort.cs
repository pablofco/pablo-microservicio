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
        /// Get the list of Rentings where they are still alive (dont return vehicle) <see cref="Renting"/>.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetStillAliveAsync();

        /// <summary>
        /// Get one Renting where they are still alive (dont return vehicle) <see cref="Renting"/>.
        /// </summary>
        /// <param name="customerId">rentingId.</param>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingStillAliveByCustomerIdAsync(int customerId);

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
        /// Get list of Rentings where date is between dateStart and dateEnd. <see cref="Renting"/>.
        /// </summary>
        /// <param name="dateBetween">date between start and end of renting.</param>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingsDatesBetweenAsync(DateTime dateBetween);

        /// <summary>
        /// Get list of Rentings per customerId where date is more or equals to input date <see cref="Renting"/>.
        /// </summary>
        /// <param name="customerId">The unique identifier of the renting to be deleted.</param>
        /// <param name="dateBetween">date between start and end of renting.</param>
        /// <returns>list of Rentings.</returns>
        Task<List<RentingCustomerVehicleDto>> GetRentingsByCustomerIdDatesBetweenAsync(int customerId, DateTime dateBetween);

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
        /// Validate if exist rentings live per Customer <see cref="Renting"/>.
        /// </summary>
        /// <param name="customerId">customerId.</param>
        /// <returns>(bool resultStillAlive, List entingsId).</returns>
        Task<(bool ResultStillAlive, List<int> RentingsId)> ValidateRentingStillAliveAsync(int customerId);

        /// <summary>
        /// ValidateRentingDates.
        /// </summary>
        /// <param name="dateStart">dateStart.</param>
        /// <param name="dateEnd">dateEnd.</param>
        /// <returns>(bool result, string message).</returns>
        (bool Result, string Message) ValidateRentingDates(DateTime dateStart, DateTime? dateEnd);

        /// <summary>
        /// ValidateRentingDates.
        /// </summary>
        /// <param name="vehicleId">vehicleId.</param>
        /// <param name="dateStart">dateStart.</param>
        /// <returns>(bool result, string message).</returns>
        Task<(bool Result, string Message)> ValidateCanRentingWithVehicleIdAsync(int vehicleId, DateTime dateStart);

        /// <summary>
        /// GetPrice.
        /// </summary>
        /// <param name="date1">date1.</param>
        /// <param name="date2">date2.</param>
        /// <returns>Price.</returns>
        Task<double> GetPrice(DateTime date1, DateTime date2);
    }
}
