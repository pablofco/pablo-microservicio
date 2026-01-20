using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class VehiclesController(IVehicleService vehicleService) : ControllerBase
    {
        private readonly IVehicleService _vehicleService = vehicleService;

        /// <summary>
        /// Get the list of all Vehicles.
        /// </summary>
        /// <returns>list of Vehicles.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetVehicles()
        {
            return Ok(await _vehicleService.GetVehiclesAllAsync());
        }

        /// <summary>
        /// Get one Vehicle.
        /// </summary>
        /// <param name="vehicleId">id of Vehicle.</param>
        /// <returns>Vehicle.</returns>
        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetVehicle(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);

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

            if (!_vehicleService.ValidateColor(vehicle))
            {
                return BadRequest($"Invalid Color:{vehicle.Color}. Have to be: Red = 1, Blue = 2, Green = 3.");
            }

            if (!_vehicleService.ValidatePort(vehicle))
            {
                return BadRequest($"Invalid Port:{vehicle.Ports}. Have to be: Three = 3, Five = 5.");
            }

            if (await _vehicleService.GetVehicleByNumberIdAsync(vehicle.NumberId) != null)
            {
                return BadRequest($"Vehicle already exist {vehicle.NumberId}.");
            }

            try
            {
                vehicle = await _vehicleService.AddVehicleAsync(vehicle);
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

            if (await _vehicleService.GetVehicleByIdAsync(vehicle.VehicleId) == null)
            {
                return NotFound($"Vehicle with VehicleId:{vehicle.VehicleId} not found.");
            }

            if (!_vehicleService.ValidateColor(vehicle))
            {
                return BadRequest($"Invalid Color:{vehicle.Color}. Have to be: Red = 1, Blue = 2, Green = 3.");
            }

            if (!_vehicleService.ValidatePort(vehicle))
            {
                return BadRequest($"Invalid Port:{vehicle.Ports}. Have to be: Three = 3, Five = 5.");
            }

            if (await _vehicleService.GetVehicleByNumberIdAsync(vehicle.NumberId) != null)
            {
                return BadRequest($"Vehicle already exist {vehicle.NumberId}.");
            }

            var vehicleValidate = await _vehicleService.GetVehicleByNumberIdAsync(vehicle.NumberId);
            if (vehicleValidate != null && vehicleValidate.VehicleId != vehicle.VehicleId)
            {
                return BadRequest($"Vehicle with NumberId:{vehicle.NumberId} and NumberId:{vehicle.NumberId} already exist in other VehicleId:{vehicleValidate.VehicleId}.");
            }

            try
            {
                vehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
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
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound($"Vehicle with VehicleId:{vehicle.VehicleId} not found.");
            }

            try
            {
                vehicle = await _vehicleService.UpdateVehicleByIdToActiveAsync(vehicleId);
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
                var vehicles = await _vehicleService.UpdateVehiclesToNoActiveAsync();

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
            await _vehicleService.DeleteVehicleAsync(vehicleId);

            return NoContent();
        }
    }
}
