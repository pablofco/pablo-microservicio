using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    /// <summary>
    /// EditVehicleUseCase.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EditVehicleUseCaseOutput"/> class.
    /// Constructor.
    /// </remarks>
    /// <param name="vehicleMapperPort">vehicle.</param>
    public class EditVehicleUseCaseOutput(
        IVehicleMapperPort vehicleMapperPort) : IVehicleUseCaseOutput<EditVehicleUseCaseOutput>
    {
        private readonly IVehicleMapperPort _vehicleMapperPort = vehicleMapperPort;

        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="vehicleDto">objeto.</param>
        /// <returns>rules.</returns>
        public async Task<(string Message, VehicleDto Model)> ExecuteAsync(VehicleDto vehicleDto)
        {
            ArgumentNullException.ThrowIfNull(vehicleDto);

            var vehicle = new Vehicle
            {
                VehicleId = vehicleDto.VehicleId,
                NumberId = vehicleDto.NumberId,
                Color = vehicleDto.Color,
                Ports = vehicleDto.Ports,
                AdquisitionDate = vehicleDto.AdquisitionDate,
                Active = true
            };

            if (await _vehicleMapperPort.GetVehicleByNumberIdAsync(vehicle.NumberId) != null)
            {
                return ($"Vehicle already exist {vehicle.NumberId}.", null);
            }

            var vehicleValidate = await _vehicleMapperPort.GetVehicleByNumberIdAsync(vehicle.NumberId);
            if (vehicleValidate != null && vehicleValidate.VehicleId != vehicle.VehicleId)
            {
                return ($"Vehicle with NumberId:{vehicle.NumberId} and NumberId:{vehicle.NumberId} already exist in other VehicleId:{vehicleValidate.VehicleId}.", null);
            }

            vehicleDto = await _vehicleMapperPort.UpdateVehicleAsync(vehicleDto);
            return ("Ok", vehicleDto);
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
