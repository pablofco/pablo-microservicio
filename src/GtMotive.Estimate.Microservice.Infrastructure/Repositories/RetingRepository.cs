using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// RetingRepository.
    /// </summary>
    public class RetingRepository(HexagonalDbContext rentingDbContext) : IRentingRepositoryPort
    {
        private readonly HexagonalDbContext _rentingDbContext = rentingDbContext;

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingsAllAsync()
        {
            return await _rentingDbContext.Rentings
                                            .Include(c => c.Customer)
                                            .Include(v => v.Vehicle)
                                            .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Renting> GetRentingByIdAsync(int rentingId)
        {
            return await _rentingDbContext.Rentings
                                            .Include(c => c.Customer)
                                            .Include(v => v.Vehicle)
                                            .FirstOrDefaultAsync(r => r.RentingId == rentingId);
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingsStillAliveAsync()
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(r => r.DateEndReal == null)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingStillAliveByCustomerIdAsync(int customerId)
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(r => r.CustomerId == customerId && r.DateEndReal == null)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingByVehicleIdAsync(int vehicleId)
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(v => v.VehicleId == vehicleId)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingsVehicleActiveAsync()
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(r => r.Vehicle.Active)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingsVehicleNoActiveAsync()
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(x => !x.Vehicle.Active)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingsDatesBetweenAsync(DateTime dateBetween)
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(r => r.DateStart <= dateBetween && (r.DateEnd >= dateBetween || (r.DateEndReal != null && r.DateEndReal >= dateBetween)))
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Renting>> GetRentingsByCustomerIdDatesBetweenAsync(int customerId, DateTime dateBetween)
        {
            return await _rentingDbContext.Rentings
                                        .Include(c => c.Customer)
                                        .Include(v => v.Vehicle)
                                        .Where(r => r.DateStart <= dateBetween && (r.DateEnd >= dateBetween || (r.DateEndReal != null && r.DateEndReal >= dateBetween)))
                                        .Where(r => r.CustomerId == customerId)
                                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Renting> AddRentingAsync(Renting renting)
        {
            await _rentingDbContext.Rentings.AddAsync(renting);
            await _rentingDbContext.SaveChangesAsync();
            return renting;
        }

        /// <inheritdoc/>
        public async Task<Renting> UpdateRentingAsync(Renting renting)
        {
            var existingRenting = await _rentingDbContext.Rentings
                .FirstOrDefaultAsync(c => c.RentingId == renting.RentingId);

            _rentingDbContext.Entry(existingRenting).CurrentValues.SetValues(renting);

            await _rentingDbContext.SaveChangesAsync();
            return existingRenting;
        }

        /// <inheritdoc/>
        public async Task DeleteRentingAsync(int rentingId)
        {
            var renting = await _rentingDbContext.Rentings.FindAsync(rentingId);
            _rentingDbContext.Rentings.Remove(renting);
            await _rentingDbContext.SaveChangesAsync();
        }
    }
}
