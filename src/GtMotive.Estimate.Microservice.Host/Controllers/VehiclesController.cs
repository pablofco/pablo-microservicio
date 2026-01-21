using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class VehiclesController(IVehicleMapperPort vehicleMapperPort) : ControllerBase
    {
        private readonly IVehicleMapperPort _vehicleMapperPort = vehicleMapperPort;

        /// <summary>
        /// Get the list of all Vehicles.
        /// </summary>
        /// <returns>list of Vehicles.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetVehicles()
        {
            return Ok(await _vehicleMapperPort.GetVehiclesAllAsync());
        }

        /// <summary>
        /// Get one Vehicle.
        /// </summary>
        /// <param name="vehicleId">id of Vehicle.</param>
        /// <returns>Vehicle.</returns>
        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetVehicle(int vehicleId)
        {
            var vehicle = await _vehicleMapperPort.GetVehicleByIdAsync(vehicleId);

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
            if (vehicle == null)
            {
                return NotFound();
            }

            if (!_vehicleMapperPort.ValidateColor(vehicle))
            {
                return BadRequest($"Invalid Color:{vehicle.Color}. Have to be: Red = 1, Blue = 2, Green = 3.");
            }

            if (!_vehicleMapperPort.ValidatePort(vehicle))
            {
                return BadRequest($"Invalid Port:{vehicle.Ports}. Have to be: Three = 3, Five = 5.");
            }

            if (await _vehicleMapperPort.GetVehicleByNumberIdAsync(vehicle.NumberId) != null)
            {
                return BadRequest($"Vehicle already exist {vehicle.NumberId}.");
            }

            try
            {
                vehicle = await _vehicleMapperPort.AddVehicleAsync(vehicle);
                return CreatedAtAction(nameof(GetVehicle), new { vehicleId = vehicle.VehicleId }, vehicle);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while adding the vehicle. {ex.Message}");
            }
        }

        /// <summary>
        /// PutVehicle, Update Vehicle.
        /// </summary>
        /// <param name="vehicle">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpPut("edit")]
        public async Task<IActionResult> PutVehicle(VehicleDto vehicle)
        {
            if (vehicle == null)
            {
                return NotFound();
            }

            if (await _vehicleMapperPort.GetVehicleByIdAsync(vehicle.VehicleId) == null)
            {
                return NotFound($"Vehicle with VehicleId:{vehicle.VehicleId} not found.");
            }

            if (!_vehicleMapperPort.ValidateColor(vehicle))
            {
                return BadRequest($"Invalid Color:{vehicle.Color}. Have to be: Red = 1, Blue = 2, Green = 3.");
            }

            if (!_vehicleMapperPort.ValidatePort(vehicle))
            {
                return BadRequest($"Invalid Port:{vehicle.Ports}. Have to be: Three = 3, Five = 5.");
            }

            if (await _vehicleMapperPort.GetVehicleByNumberIdAsync(vehicle.NumberId) != null)
            {
                return BadRequest($"Vehicle already exist {vehicle.NumberId}.");
            }

            var vehicleValidate = await _vehicleMapperPort.GetVehicleByNumberIdAsync(vehicle.NumberId);
            if (vehicleValidate != null && vehicleValidate.VehicleId != vehicle.VehicleId)
            {
                return BadRequest($"Vehicle with NumberId:{vehicle.NumberId} and NumberId:{vehicle.NumberId} already exist in other VehicleId:{vehicleValidate.VehicleId}.");
            }

            try
            {
                vehicle = await _vehicleMapperPort.UpdateVehicleAsync(vehicle);
                return Ok(vehicle);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while updating the vehicle. {ex.Message}");
            }
        }

        /// <summary>
        /// PutVehicleActive, Active one Vehicle in the fleet.
        /// </summary>
        /// <param name="vehicleId">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpPut("active/{vehicleId}")]
        public async Task<IActionResult> PutVehicleActive(int vehicleId)
        {
            var vehicle = await _vehicleMapperPort.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound($"Vehicle with VehicleId:{vehicle.VehicleId} not found.");
            }

            try
            {
                vehicle = await _vehicleMapperPort.UpdateVehicleByIdToActiveAsync(vehicleId);
                return Ok(vehicle);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while updating the vehicle to active. {ex.Message}");
            }
        }

        /// <summary>
        /// PutVehiclesNoActive, No Active Vehicles in the fleet.
        /// </summary>
        /// <returns>NoContent.</returns>
        [HttpPut("noactive")]
        public async Task<IActionResult> PutVehiclesNoActive()
        {
            try
            {
                var vehicles = await _vehicleMapperPort.UpdateVehiclesToNoActiveAsync();

                return vehicles == null || vehicles.Count == 0
                    ? NotFound("No vehicles found to update to No active.")
                    : Ok($"List of vehicles updated to no active: {string.Join(", ", vehicles.Select(v => v.VehicleId + "-" + v.NumberId).ToList())}");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while updating the vehicles to No active. {ex.Message}");
            }
        }

        /// <summary>
        /// PutNoActivateVehicle.
        /// </summary>
        /// <param name="vehicleId">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpDelete("delete/{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            await _vehicleMapperPort.DeleteVehicleAsync(vehicleId);

            return NoContent();
        }
    }
}
