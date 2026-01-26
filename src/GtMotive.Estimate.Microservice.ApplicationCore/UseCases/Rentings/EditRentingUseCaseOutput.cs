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
    /// /// <param name="vehicleMapperPort">vehicle.</param>
    /// <param name="customerMapperPort">customer.</param>
    public class EditRentingUseCaseOutput(
        IRentingMapperPort rentingMapperPort,
        IVehicleMapperPort vehicleMapperPort,
        ICustomerMapperPort customerMapperPort) : IRentingUseCaseOutput<EditRentingUseCaseOutput>
    {
        private readonly IRentingMapperPort _rentingMapperPort = rentingMapperPort;
        private readonly IVehicleMapperPort _vehicleMapperPort = vehicleMapperPort;
        private readonly ICustomerMapperPort _customerMapperPort = customerMapperPort;

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
                RentingId = rentingDto.VehicleId,
                DateStart = rentingDto.DateStart,
                DateEnd = rentingDto.DateEnd,
                CustomerId = rentingDto.CustomerId,
                VehicleId = rentingDto.VehicleId
            };

            if (await _customerMapperPort.GetCustomerByIdAsync(renting.CustomerId) == null)
            {
                return ($"Customer with ID {renting.CustomerId} not found.", null);
            }

            if (await _vehicleMapperPort.GetVehicleByIdAsync(renting.VehicleId) == null)
            {
                return ($"Vehicle with ID {renting.VehicleId} not found.", null);
            }

            var (result, message) = _rentingMapperPort.ValidateRentingDates(renting.DateStart, renting.DateEnd);
            if (!result)
            {
                return (message, null);
            }

            rentingDto = await _rentingMapperPort.UpdateRentingAsync(rentingDto);

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
