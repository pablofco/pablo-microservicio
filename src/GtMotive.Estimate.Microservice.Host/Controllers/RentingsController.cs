using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RentingsController(IRentingMapperPort rentingService,
        ICustomerMapperPort customerService,
        IVehicleMapperPort vehicleService) : ControllerBase
    {
        private readonly IRentingMapperPort _rentingService = rentingService;
        private readonly ICustomerMapperPort _customerService = customerService;
        private readonly IVehicleMapperPort _vehicleService = vehicleService;

        /// <summary>
        /// Get the list of all rentings.
        /// </summary>
        /// <returns>list of rentings.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetRentings()
        {
            return Ok(await _rentingService.GetRentingsAllAsync());
        }

        /// <summary>
        /// Get one Renting.
        /// </summary>
        /// <param name="rentingId">id of renting.</param>
        /// <returns>Renting.</returns>
        [HttpGet("renting/{rentingId}")]
        public async Task<IActionResult> GetRenting(int rentingId)
        {
            var renting = await _rentingService.GetRentingByIdAsync(rentingId);

            return renting == null ? NotFound() : Ok(renting);
        }

        /// <summary>
        /// Get the list of Rentings that have vehicles no return yet..
        /// </summary>
        /// <returns>list of Rentings.</returns>
        [HttpGet("alive")]
        public async Task<IActionResult> GetRentingsWithVehicleNoReturnYetAsync()
        {
            var rentings = await _rentingService.GetStillAliveAsync();

            return Ok(rentings);
        }

        /// <summary>
        /// Get the list of Rentings where vehicles are active in the fleet.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        [HttpGet("vehicleactive")]
        public async Task<IActionResult> GetRentingsVehicleActive()
        {
            var rentings = await _rentingService.GetRentingsVehicleActiveAsync();

            return Ok(rentings);
        }

        /// <summary>
        /// Get list of Rentings where vehicles are no active in the fleet.
        /// </summary>
        /// <returns>list of Rentings.</returns>
        [HttpGet("vehiclenoactive")]
        public async Task<IActionResult> GetRentingsVehicleNoActive()
        {
            var rentings = await _rentingService.GetRentingsVehicleNoActiveAsync();

            return Ok(rentings);
        }

        /// <summary>
        /// Get list of Rentings where date is between dateStart and dateEnd.
        /// </summary>
        /// <param name="dateBetween">date between start and end of renting.</param>
        /// <returns>list of Rentings.</returns>
        [HttpGet("datesbetween/{dateBetween}")]
        public async Task<IActionResult> GetRentingsDatesBetween(DateTime dateBetween)
        {
            var rentings = await _rentingService.GetRentingsDatesBetweenAsync(dateBetween);

            return Ok(rentings);
        }

        /// <summary>
        /// Get list of Rentings per customerId where date is more or equals to input date.
        /// </summary>
        /// <param name="customerId">customerId.</param>
        /// <param name="date">dateStart.</param>
        /// <returns>list of Rentings.</returns>
        [HttpGet("renting/customer/{customerId}/datesbetween/{date}")]
        public async Task<IActionResult> GetRentingsByCustomerIdDatesBetween(int customerId, DateTime date)
        {
            var rentings = await _rentingService.GetRentingsByCustomerIdDatesBetweenAsync(customerId, date);

            return Ok(rentings);
        }

        /// <summary>
        /// PostRenting, Add Renting.
        /// </summary>
        /// <param name="rentingNew">renting.</param>
        /// <returns>Create Renting.</returns>
        [HttpPost]
        public async Task<IActionResult> PostRenting([FromBody] RentingNewDto rentingNew)
        {
            if (rentingNew == null)
            {
                return NotFound();
            }

            if (await _customerService.GetCustomerByIdAsync(rentingNew.CustomerId) == null)
            {
                return NotFound($"Customer with ID {rentingNew.CustomerId} not found.");
            }

            if (await _vehicleService.GetVehicleByIdAsync(rentingNew.VehicleId) == null)
            {
                return NotFound($"Vehicle with ID {rentingNew.VehicleId} not found.");
            }

            var (result, message) = _rentingService.ValidateRentingDates(rentingNew.DateStart, rentingNew.DateEnd);
            if (!result)
            {
                return BadRequest(message);
            }

            var (resultStillAlive, rentingsId) = await _rentingService.ValidateRentingStillAliveAsync(rentingNew.CustomerId);
            if (resultStillAlive)
            {
                return BadRequest($"This Customer has another renting alive CustomerId:{rentingNew.CustomerId}, RentingId:{string.Join(" - ", rentingsId)}");
            }

            var (resultVehicle, messageVehicle) = await _rentingService.ValidateCanRentingWithVehicleIdAsync(rentingNew.VehicleId, rentingNew.DateStart);
            if (!resultVehicle)
            {
                return BadRequest($"{messageVehicle}");
            }

            try
            {
                var renting = _rentingService.ConvertDtoToRenting(rentingNew);
                renting.Price = await _rentingService.GetPrice(rentingNew.DateStart, rentingNew.DateEnd);

                renting = await _rentingService.AddRentingAsync(renting);
                return CreatedAtAction(nameof(GetRenting), new { rentingId = rentingNew.RentingId }, renting);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while adding the renting. {ex.Message}");
            }
        }

        /// <summary>
        /// PutRenting, Update Renting.
        /// </summary>
        /// <param name="renting">renting.</param>
        /// <returns>NoContent.</returns>
        [HttpPut("NoActive")]
        public async Task<IActionResult> PutRenting(RentingDto renting)
        {
            if (renting == null)
            {
                return NotFound();
            }

            if (await _customerService.GetCustomerByIdAsync(renting.CustomerId) == null)
            {
                return NotFound($"Customer with ID {renting.CustomerId} not found.");
            }

            if (await _vehicleService.GetVehicleByIdAsync(renting.VehicleId) == null)
            {
                return NotFound($"Vehicle with ID {renting.VehicleId} not found.");
            }

            var (result, message) = _rentingService.ValidateRentingDates(renting.DateStart, renting.DateEnd);
            if (!result)
            {
                return BadRequest(message);
            }

            try
            {
                renting.Price = await _rentingService.GetPrice(renting.DateStart, renting.DateEnd);
                renting = await _rentingService.UpdateRentingAsync(renting);
                return Ok(renting);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while updating the renting. {ex.Message}");
            }
        }

        /// <summary>
        /// PutRentingClose, Update Renting, end of the renting dateEnd.
        /// </summary>
        /// <param name="rentingId">rentingId.</param>
        /// <param name="dateEnd">dateEnd.</param>
        /// <returns>NoContent.</returns>
        [HttpPut("close/rentingId/{rentingId}/dateEnd/{dateEnd}")]
        public async Task<IActionResult> PutRentingClose(int rentingId, DateTime dateEnd)
        {
            var rentingUpdate = await _rentingService.GetRentingByIdAsync(rentingId);
            if (rentingUpdate == null)
            {
                return NotFound($"This renting not exist {rentingUpdate.RentingId} not found.");
            }

            var (result, message) = _rentingService.ValidateRentingDates(rentingUpdate.DateStart, dateEnd);
            if (!result)
            {
                return BadRequest(message);
            }

            try
            {
                var renting = await _rentingService.UpdateRentingCloseAsync(rentingId, dateEnd);
                return Ok(renting);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while updating the renting. {ex.Message}");
            }
        }

        /// <summary>
        /// DeleteRenting, Delete Renting.
        /// </summary>
        /// <param name="rentingId">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpDelete("delete/{rentingId}")]
        public async Task<IActionResult> DeleteRenting(int rentingId)
        {
            await _rentingService.DeleteRentingAsync(rentingId);

            return NoContent();
        }
    }
}
