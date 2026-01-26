using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class VehiclesController(IVehicleMapperPort vehicleMapperPort, IVehicleUseCaseOutput<CreateVehicleUseCaseOutput> createVehiclesUseCase,
        IVehicleUseCaseOutput<EditVehicleUseCaseOutput> editVehicleUseCase) : ControllerBase
    {
        private readonly IVehicleMapperPort _vehicleMapperPort = vehicleMapperPort;
        private readonly IVehicleUseCaseOutput<CreateVehicleUseCaseOutput> _createVehiclesUseCase = createVehiclesUseCase;
        private readonly IVehicleUseCaseOutput<EditVehicleUseCaseOutput> _editVehicleUseCase = editVehicleUseCase;

        /// <summary>
        /// Get the list of all Vehicles.
        /// </summary>
        /// <returns>list of Vehicles.</returns>
        [HttpGet("todos")]
        public async Task<IActionResult> GetVehicles()
        {
            return Ok(await _vehicleMapperPort.GetVehiclesAllAsync());
        }

        /// <summary>
        /// Get one Vehicle.
        /// </summary>
        /// <param name="vehiculoId">id of Vehicle.</param>
        /// <returns>Vehicle.</returns>
        [HttpGet("vehiculo/{vehiculoId}")]
        public async Task<IActionResult> GetVehicle(int vehiculoId)
        {
            var vehicle = await _vehicleMapperPort.GetVehicleByIdAsync(vehiculoId);

            return vehicle == null ? NotFound() : Ok(vehicle);
        }

        /// <summary>
        /// PostVehicle, Add Vehicle.
        /// </summary>
        /// <param name="vehicle">vehicle.</param>
        /// <returns>Create Vehicle.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> PostVehicle(VehicleDto vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            try
            {
                var (message, model) = await _createVehiclesUseCase.ExecuteAsync(vehicle);
                if (message != "Ok")
                {
                    return BadRequest(message);
                }
                else
                {
                    return CreatedAtAction(nameof(GetVehicle), new { vehicleId = model.VehicleId }, model);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(_createVehiclesUseCase.HandleError(ex.Message));
            }
        }

        /// <summary>
        /// PutVehicle, Update Vehicle.
        /// </summary>
        /// <param name="vehicle">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpPut("editar")]
        public async Task<IActionResult> PutVehicle(VehicleDto vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            try
            {
                var (message, model) = await _editVehicleUseCase.ExecuteAsync(vehicle);
                if (message != "Ok")
                {
                    return BadRequest(message);
                }
                else
                {
                    return Ok(model);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(_editVehicleUseCase.HandleError(ex.Message));
            }
        }

        /// <summary>
        /// PutNoActivateVehicle.
        /// </summary>
        /// <param name="vehicleId">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpDelete("eliminar/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            await _vehicleMapperPort.DeleteVehicleAsync(vehicleId);

            return NoContent();
        }
    }
}
