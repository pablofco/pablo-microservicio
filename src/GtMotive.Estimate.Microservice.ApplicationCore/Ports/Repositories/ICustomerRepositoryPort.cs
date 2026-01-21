using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories
{
    /// <summary>
    /// Interface ICustomerRepository.
    /// </summary>
    public interface ICustomerRepositoryPort
    {
        /// <summary>
        /// Get the list of all Customers <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<Customer>> GetCustomersAllAsync();

        /// <summary>
        /// Get one Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to be deleted.</param>
        /// <returns>Customer.</returns>
        Task<Customer> GetCustomerByIdAsync(int customerId);

        /// <summary>
        /// Get one Customer per Document <see cref="Customer"/>.
        /// </summary>
        /// <param name="document">document of customer.</param>
        /// <returns>Customer.</returns>
        Task<Customer> GetCustomerByDocumentAsync(string document);

        /// <summary>
        /// Get the list of Cutomers that have rentings <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<Customer>> GetCustomersWithRentingsAsync();

        /// <summary>
        /// Get the list of Customers that have rentings and vehicle is still active in the fleet <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<Customer>> GetCustomersWithRentingsAndVehicleActive();

        /// <summary>
        /// Get the list of Customers that have rentings and vehicle is not active in the fleet <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<Customer>> GetCustomersWithRentingsAndVehicleNoActiveAsync();

        /// <summary>
        /// Get the list of Customers that have rentings and vehicle is not return yet <see cref="Customer"/>.
        /// </summary>
        /// <returns>list of Customer.</returns>
        Task<List<Customer>> GetCustomersWithRentingsAndVehicleNoReturnYet();

        /// <summary>
        /// Add Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customer">The unique identifier of the customer to be deleted.</param>
        /// <returns>Customer.</returns>
        Task<Customer> AddCustomerAsync(Customer customer);

        /// <summary>
        /// Update Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customer">The unique identifier of the customer to be deleted.</param>
        /// <returns>Customer.</returns>
        Task<Customer> UpdateCustomerAsync(Customer customer);

        /// <summary>
        /// Delete Customer <see cref="Customer"/>.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to be deleted.</param>
        /// <returns>task.</returns>
        Task DeleteCustomerAsync(int customerId);
    }
}
