using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers
{
    /// <summary>
    /// Interface ICustomerService.
    /// </summary>
    public interface ICustomerMapperPort
    {
        /// <summary>
        /// Get the list of all Customers <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<CustomerDto>> GetCustomersAllAsync();

        /// <summary>
        /// Get one Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to be deleted.</param>
        /// <returns>Customer.</returns>
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);

        /// <summary>
        /// Get one Customer per Document <see cref="Customer"/>.
        /// </summary>
        /// <param name="document">document of customer.</param>
        /// <returns>Customer.</returns>
        Task<CustomerDto> GetCustomerByDocumentAsync(string document);

        /// <summary>
        /// Get the list of Cutomers that have rentings <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<CustomerRentingsDto>> GetCustomersWithRentingsAsync();

        /// <summary>
        /// Add Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customerDto">The unique identifier of the customer to be deleted.</param>
        /// <returns>Customer.</returns>
        Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto);

        /// <summary>
        /// Update Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customerDto">The unique identifier of the customer to be deleted.</param>
        /// <returns>Customer.</returns>
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto);

        /// <summary>
        /// Delete Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to be deleted.</param>
        /// <returns>task.</returns>
        Task DeleteCustomerAsync(int customerId);
    }
}
