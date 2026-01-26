using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CustomersController(ICustomerMapperPort customerMapperPort,
            ICustomerUseCaseOutput<CreateCustomerUseCaseOutput> createCustomerUseCase,
            ICustomerUseCaseOutput<EditCustomerUseCaseOutput> editCustomerUseCase) : ControllerBase
    {
        private readonly ICustomerMapperPort _customerMapperPort = customerMapperPort;
        private readonly ICustomerUseCaseOutput<CreateCustomerUseCaseOutput> _createCustomerUseCase = createCustomerUseCase;
        private readonly ICustomerUseCaseOutput<EditCustomerUseCaseOutput> _editCustomerUseCase = editCustomerUseCase;

        /// <summary>
        /// Get the list of all Customers.
        /// </summary>
        /// <returns>list of Cutomers.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _customerMapperPort.GetCustomersAllAsync());
        }

        /// <summary>
        /// Get one Customer.
        /// </summary>
        /// <param name="customerId">customerId.</param>
        /// <returns>Customer.</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var customer = await _customerMapperPort.GetCustomerByIdAsync(customerId);

            return customer == null ? NotFound() : Ok(customer);
        }

        /// <summary>
        /// PostCustomer, Add Customer.
        /// </summary>
        /// <param name="customer">customer.</param>
        /// <returns>Create customer.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerDto customer)
        {
            ArgumentNullException.ThrowIfNull(customer);

            try
            {
                var (message, model) = await _createCustomerUseCase.ExecuteAsync(customer);
                if (message != "Ok")
                {
                    return BadRequest(message);
                }
                else
                {
                    return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.CustomerId }, model);
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(_createCustomerUseCase.HandleError(ex.Message));
            }
        }

        /// <summary>
        /// PutCustomer, Update Customer.
        /// </summary>
        /// <param name="customer">customer.</param>
        /// <returns>No Content.</returns>
        [HttpPut("edit")]
        public async Task<IActionResult> PutCustomer([FromBody] CustomerDto customer)
        {
            ArgumentNullException.ThrowIfNull(customer);

            try
            {
                var (message, model) = await _editCustomerUseCase.ExecuteAsync(customer);
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
                return BadRequest(_editCustomerUseCase.HandleError(ex.Message));
            }
        }

        /// <summary>
        /// DeleteCustomer, Delete Customer.
        /// </summary>
        /// <param name="customerId">customerId.</param>
        /// <returns>NoContent.</returns>
        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _customerMapperPort.DeleteCustomerAsync(customerId);

            return NoContent();
        }
    }
}
