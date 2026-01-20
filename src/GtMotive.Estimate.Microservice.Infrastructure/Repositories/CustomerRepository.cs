using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// CustomerRepository.
    /// </summary>
    public class CustomerRepository(HexagonalDbContext rentingDbContext) : ICustomerRepository
    {
        private readonly HexagonalDbContext _rentingDbContext = rentingDbContext;

        /// <inheritdoc/>
        public async Task<List<Customer>> GetCustomersAllAsync()
        {
            return await _rentingDbContext.Customers.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _rentingDbContext.Customers.FindAsync(customerId);
        }

        /// <inheritdoc/>
        public async Task<Customer> GetCustomerByDocumentAsync(string document)
        {
            return await _rentingDbContext.Customers
                                    .Where(c => c.Document == document)
                                    .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Customer>> GetCustomersWithRentingsAsync()
        {
            return await _rentingDbContext.Customers
                                        .Include(c => c.Rentings)
                                        .ThenInclude(v => v.Vehicle)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Customer>> GetCustomersWithRentingsAndVehicleActive()
        {
            return await _rentingDbContext.Customers
                                        .Include(c => c.Rentings)
                                        .ThenInclude(v => v.Vehicle)
                                        .Where(c => c.Rentings.Any(r => r.Vehicle.Active))
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Customer>> GetCustomersWithRentingsAndVehicleNoActiveAsync()
        {
            return await _rentingDbContext.Customers
                                        .Include(c => c.Rentings)
                                        .ThenInclude(v => v.Vehicle)
                                        .Where(c => c.Rentings.Any(r => !r.Vehicle.Active))
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Customer>> GetCustomersWithRentingsAndVehicleNoReturnYet()
        {
            return await _rentingDbContext.Customers
                                        .Include(c => c.Rentings)
                                        .ThenInclude(v => v.Vehicle)
                                        .Where(c => c.Rentings.Any(r => r.DateEndReal == null))
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _rentingDbContext.Customers.AddAsync(customer);
            await _rentingDbContext.SaveChangesAsync();
            return customer;
        }

        /// <inheritdoc/>
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _rentingDbContext.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);

            _rentingDbContext.Entry(existingCustomer).CurrentValues.SetValues(customer);

            await _rentingDbContext.SaveChangesAsync();
            return existingCustomer;
        }

        /// <inheritdoc/>
        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _rentingDbContext.Customers.FindAsync(customerId);
            _rentingDbContext.Customers.Remove(customer);
            await _rentingDbContext.SaveChangesAsync();
        }
    }
}
