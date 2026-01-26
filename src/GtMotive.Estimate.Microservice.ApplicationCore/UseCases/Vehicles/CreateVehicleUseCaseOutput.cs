using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    /// <summary>
    /// CreateVehicleUseCase.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleUseCaseOutput"/> class.
    /// Constructor.
    /// </remarks>
    /// <param name="vehicleMapperPort">vehicle.</param>
    public class CreateVehicleUseCaseOutput(
        IVehicleMapperPort vehicleMapperPort) : IVehicleUseCaseOutput<CreateVehicleUseCaseOutput>
    {
        private readonly IVehicleMapperPort _vehicleMapperPort = vehicleMapperPort;

        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="vehicleDto">objeto.</param>
        /// <returns>rules.</returns>
        public async Task<(string Message, VehicleDto Model)> ExecuteAsync(VehicleDto vehicleDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(vehicleDto);

                var vehicle = new Vehicle
                {
                    VehicleId = vehicleDto.VehicleId,
                    NumberId = vehicleDto.NumberId,
                    Color = vehicleDto.Color,
                    Doors = vehicleDto.Doors,
                    AdquisitionDate = vehicleDto.AdquisitionDate,
                    Active = true
                };

                if (await _vehicleMapperPort.GetVehicleByNumberIdAsync(vehicle.NumberId) != null)
                {
                    return ($"Vehicle already exist {vehicle.NumberId}.", null);
                }

                vehicleDto = await _vehicleMapperPort.AddVehicleAsync(vehicleDto);

                return ("Ok", vehicleDto);
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
            return $"An error occurred while adding the vehicle. {errorMessage}";
        }
    }
}
