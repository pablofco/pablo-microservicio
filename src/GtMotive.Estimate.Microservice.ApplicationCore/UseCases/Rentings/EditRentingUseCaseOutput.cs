using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentings
{
    /// <summary>
    /// EditRentingUseCase.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EditRentingUseCaseOutput"/> class.
    /// Constructor.
    /// </remarks>
    /// <param name="rentingMapperPort">renting.</param>
    /// <param name="vehicleMapperPort">vehicle.</param>
    public class EditRentingUseCaseOutput(
        IRentingMapperPort rentingMapperPort,
        IVehicleMapperPort vehicleMapperPort) : IRentingUseCaseOutput<EditRentingUseCaseOutput>
    {
        private readonly IRentingMapperPort _rentingMapperPort = rentingMapperPort;
        private readonly IVehicleMapperPort _vehicleMapperPort = vehicleMapperPort;

        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="rentingDto">objeto.</param>
        /// <returns>rules.</returns>
        public async Task<(string Message, RentingDto Model)> ExecuteAsync(RentingDto rentingDto)
        {
            ArgumentNullException.ThrowIfNull(rentingDto);

            var renting = new Renting
            {
                RentingId = rentingDto.RentingId,
                DateStart = rentingDto.DateStart,
                DateEnd = rentingDto.DateEnd,
                CustomerId = rentingDto.CustomerId,
                VehicleId = rentingDto.VehicleId
            };

            await _vehicleMapperPort.UpdateVehiclesToNoActiveAsync();

            var rentingUpdate = await _rentingMapperPort.GetRentingByIdAsync(renting.RentingId);
            if (rentingUpdate == null)
            {
                return ($"This renting not exist {rentingUpdate.RentingId} not found.", null);
            }

            await _rentingMapperPort.UpdateRentingCloseAsync(renting.RentingId, renting.DateEnd);

            return ("Ok", rentingDto);
        }

        /// <summary>
        /// Handle error.
        /// </summary>
        /// <param name="errorMessage">errorMessage.</param>
        /// <returns>string.</returns>
        public string HandleError(string errorMessage)
        {
            return $"An error occurred while adding the vehicle. {errorMessage}";
        }
    }
}
