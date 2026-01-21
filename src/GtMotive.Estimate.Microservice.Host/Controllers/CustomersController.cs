using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomersController"/> class.
    /// CustomersController.
    /// </summary>
    /// <param name="customerService">customerService.</param>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CustomersController(ICustomerMapperPort customerService) : ControllerBase
    {
        private readonly ICustomerMapperPort _customerService = customerService;

        /// <summary>
        /// Get the list of all Customers.
        /// </summary>
        /// <returns>list of Cutomers.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _customerService.GetCustomersAllAsync());
        }

        /// <summary>
        /// Get one Customer.
        /// </summary>
        /// <param name="customerId">customerId.</param>
        /// <returns>Customer.</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            return customer == null ? NotFound() : Ok(customer);
        }

        /// <summary>
        /// Get the list of Cutomers that have or had rentings.
        /// </summary>
        /// <returns>list of Cutomers.</returns>
        [HttpGet("rentings")]
        public async Task<IActionResult> GetCustomersWithRentings()
        {
            var customers = await _customerService.GetCustomersWithRentingsAsync();

            return Ok(customers);
        }

        /// <summary>
        /// Get the list of Customers that have rentings and vehicle is still active in the fleet.
        /// </summary>
        /// <returns>list of customers.</returns>
        [HttpGet("rentings/vehicleactive")]
        public async Task<IActionResult> GetCustomersWithRentingsAndVehicleActive()
        {
            var customers = await _customerService.GetCustomersWithRentingsAndVehicleActiveAsync();

            return Ok(customers);
        }

        /// <summary>
        /// Get the list of Customers that have rentings and vehicle is not active in the fleet.
        /// </summary>
        /// <returns>list of customers.</returns>
        [HttpGet("rentings/vehiclenoactive")]
        public async Task<IActionResult> GetCustomersWithRentingsAndVehicleNoActive()
        {
            var customers = await _customerService.GetCustomersWithRentingsAndVehicleNoActiveAsync();

            return Ok(customers);
        }

        /// <summary>
        /// Get the list of Customers that have rentings and vehicle is not return yet.
        /// </summary>
        /// <returns>list of customers.</returns>
        [HttpGet("rentings/vehiclenoreturn")]
        public async Task<IActionResult> GetCustomersWithRentingsAndVehicleNoReturnYet()
        {
            var customers = await _customerService.GetCustomersWithRentingsAndVehicleNoReturnYetAsync();

            return Ok(customers);
        }

        /// <summary>
        /// PostCustomer, Add Customer.
        /// </summary>
        /// <param name="customer">customer.</param>
        /// <returns>Create customer.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerDto customer)
        {
            if (customer == null)
            {
                return NotFound();
            }

            if (!_customerService.ValidateDocumentType(customer))
            {
                return BadRequest($"Invalid DocumentType:{customer.DocumentType}. Have to be: DNI = 1, Passport = 2.");
            }

            if (await _customerService.GetCustomerByDocumentAsync(customer.Document) != null)
            {
                return BadRequest($"Customer with Document Number:{customer.Document}, already exist.");
            }

            try
            {
                customer = await _customerService.AddCustomerAsync(customer);
                return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.CustomerId }, customer);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while adding the customer. {ex.Message}");
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
            if (customer == null)
            {
                return NotFound();
            }

            if (await _customerService.GetCustomerByIdAsync(customer.CustomerId) == null)
            {
                return NotFound($"Customer with CustomerId:{customer.CustomerId} not found.");
            }

            if (!_customerService.ValidateDocumentType(customer))
            {
                return BadRequest($"Invalid document type {customer.DocumentType}. Have to be: DNI = 1, Passport = 2.");
            }

            var customerValidate = await _customerService.GetCustomerByDocumentAsync(customer.Document);
            if (customerValidate != null && customerValidate.CustomerId != customer.CustomerId)
            {
                return BadRequest($"Customer with Document:{customer.Document} and CustomerId:{customer.CustomerId} already exist in other CustomerId:{customerValidate.CustomerId}.");
            }

            try
            {
                customer = await _customerService.UpdateCustomerAsync(customer);
                return Ok(customer);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"An error occurred while updating the customer. {ex.Message}");
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
            await _customerService.DeleteCustomerAsync(customerId);

            return NoContent();
        }
    }
}
