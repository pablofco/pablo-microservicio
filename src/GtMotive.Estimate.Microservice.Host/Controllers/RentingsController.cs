using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RentingsController(IRentingMapperPort rentingMapperPort,
        IRentingUseCaseOutput<CreateRentingUseCaseOutput> createRentingUseCase,
        IRentingUseCaseOutput<EditRentingUseCaseOutput> editRentingUseCase) : ControllerBase
    {
        private readonly IRentingMapperPort _rentingMapperPort = rentingMapperPort;
        private readonly IRentingUseCaseOutput<CreateRentingUseCaseOutput> _createRentingUseCase = createRentingUseCase;
        private readonly IRentingUseCaseOutput<EditRentingUseCaseOutput> _editRentingUseCase = editRentingUseCase;

        /// <summary>
        /// Get the list of all rentings.
        /// </summary>
        /// <returns>list of rentings.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetRentings()
        {
            return Ok(await _rentingMapperPort.GetRentingsAllAsync());
        }

        /// <summary>
        /// Get one Renting.
        /// </summary>
        /// <param name="rentingId">id of renting.</param>
        /// <returns>Renting.</returns>
        [HttpGet("renting/{rentingId}")]
        public async Task<IActionResult> GetRenting(int rentingId)
        {
            var renting = await _rentingMapperPort.GetRentingByIdAsync(rentingId);

            return renting == null ? NotFound() : Ok(rentingId);
        }

        /// <summary>
        /// PostRenting, Add Renting.
        /// </summary>
        /// <param name="renting">renting.</param>
        /// <returns>Create Renting.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> PostRenting([FromBody] RentingDto renting)
        {
            ArgumentNullException.ThrowIfNull(renting);

            try
            {
                var (message, model) = await _createRentingUseCase.ExecuteAsync(renting);
                if (message != "Ok")
                {
                    return BadRequest(message);
                }
                else
                {
                    return CreatedAtAction(nameof(GetRenting), new { rentingId = renting.RentingId }, model);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(_createRentingUseCase.HandleError(ex.Message));
            }
        }

        /// <summary>
        /// PutRentingClose, Update Renting, end of the renting dateEnd.
        /// </summary>
        /// <param name="rentingId">rentingId.</param>
        /// <param name="dateEnd">dateEnd.</param>
        /// <returns>NoContent.</returns>
        [HttpPut("return/rentingId/{rentingId}/dateEnd/{dateEnd}")]
        public async Task<IActionResult> PutRentingClose(int rentingId, DateTime dateEnd)
        {
            try
            {
                var renting = new RentingDto { RentingId = rentingId, DateEnd = dateEnd };
                var (message, model) = await _editRentingUseCase.ExecuteAsync(renting);
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
                return BadRequest(_editRentingUseCase.HandleError(ex.Message));
            }
        }

        /// <summary>
        /// Delete Renting.
        /// </summary>
        /// <param name="rentingId">vehicle.</param>
        /// <returns>NoContent.</returns>
        [HttpDelete("delete/{rentingId}")]
        public async Task<IActionResult> DeleteRenting(int rentingId)
        {
            await _rentingMapperPort.DeleteRentingAsync(rentingId);

            return NoContent();
        }
    }
}
