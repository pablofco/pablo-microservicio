using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
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
        public async Task<Renting> AddRentingAsync(Renting renting)
        {
            await _rentingDbContext.Rentings.AddAsync(renting);
            await _rentingDbContext.SaveChangesAsync();
            return renting;
        }

        public async Task<Renting> UpdateRentingAsync(Renting renting, DateTime dateEnd)
        {
            var existingRenting = await _rentingDbContext.Rentings
                .FirstOrDefaultAsync(c => c.RentingId == renting.RentingId);

            _rentingDbContext.Entry(existingRenting).CurrentValues.SetValues(renting);

            // Sobrescribe la fecha con el parámetro recibido
            existingRenting.DateEnd = dateEnd;

            await _rentingDbContext.SaveChangesAsync();
            return existingRenting;
        }

        public async Task<bool> ExistsFutureRentingAsync(RentingCustomerVehicleDto renting, RentingDto dto)
        {
            var newRentings = await GetNextRentingByVehicleAsync(renting.VehicleId);

            return await _rentingDbContext.Rentings.AnyAsync(r =>
                r.VehicleId == renting.VehicleId &&
                r.RentingId != renting.RentingId &&
                dto.DateEnd > newRentings.DateStart);
        }

        public async Task<Renting> GetNextRentingByVehicleAsync(int vehicleId)
        {
            var today = DateTime.UtcNow.Date;

            return await _rentingDbContext.Rentings
                .Where(r => r.VehicleId == vehicleId && r.DateStart >= today)
                .OrderBy(r => r.DateStart)
                .FirstOrDefaultAsync();
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
