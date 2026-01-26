using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentings
{
    /// <summary>
    /// CreateRentingUseCase.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateRentingUseCaseOutput"/> class.
    /// Constructor.
    /// </remarks>
    /// <param name="rentingMapperPort">renting.</param>
    /// <param name="vehicleMapperPort">vehicle.</param>
    /// <param name="customerMapperPort">customer.</param>
    public class CreateRentingUseCaseOutput(
        IRentingMapperPort rentingMapperPort,
        IVehicleMapperPort vehicleMapperPort,
        ICustomerMapperPort customerMapperPort) : IRentingUseCaseOutput<CreateRentingUseCaseOutput>
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
            try
            {
                ArgumentNullException.ThrowIfNull(rentingDto);

                await _vehicleMapperPort.UpdateVehiclesToNoActiveAsync();

                if (await _customerMapperPort.GetCustomerByIdAsync(rentingDto.CustomerId) == null)
                {
                    return ($"Customer with ID {rentingDto.CustomerId} not found.", null);
                }

                var vehicle = await _vehicleMapperPort.GetVehicleByIdAsync(rentingDto.VehicleId);
                if (vehicle == null)
                {
                    return ($"Vehicle with ID {rentingDto.VehicleId} not found.", null);
                }
                else if (vehicle.Active == false)
                {
                    return ($"El vehículo ya no está activo tiene más de 5 años.", null);
                }

                var rentings = await _rentingMapperPort.ValidateCanRentingWithVehicleIdAsync(rentingDto.VehicleId, rentingDto.DateStart);

                var result2 = rentings.Where(r => rentingDto.DateStart >= r.DateStart).ToList();

                if (result2.Count > 0)
                {
                    return ($"The vehicle is already rented and has no end date, RentingsIds:{string.Join(", ", result2.Select(r => r.RentingId).ToList())}", null);
                }

                result2 = rentings.Where(r => rentingDto.DateStart >= r.DateStart && rentingDto.DateStart <= r.DateEnd).ToList();
                if (result2.Count > 0)
                {
                    return ($"The vehicle is already rented during the specified dates. RentingsIds: {string.Join(", ", result2.Select(r => r.RentingId).ToList())}", null);
                }

                rentingDto.Price = await _rentingMapperPort.GetPrice(rentingDto.DateStart, rentingDto.DateEnd);

                rentingDto = await _rentingMapperPort.AddRentingAsync(rentingDto);

                return ("Ok", rentingDto);
            }
            catch (ArgumentNullException ex)
            {
                return (HandleError(ex.Message), null);
            }
            catch (InvalidOperationException ex)
            {
                return (HandleError(ex.Message), null);
            }
        }

        /// <summary>
        /// Handle error.
        /// </summary>
        /// <param name="errorMessage">errorMessage.</param>
        /// <returns>string.</returns>
        public string HandleError(string errorMessage)
        {
            return $"An error occurred while adding the renting. {errorMessage}";
        }
    }
}
