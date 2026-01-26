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
    /// VehicleRepository.
    /// </summary>
    public class VehicleRepository(HexagonalDbContext rentingDbContext,
        IParameterRepositoryPort parameterRepository) : IVehicleRepositoryPort
    {
        private readonly HexagonalDbContext _rentingDbContext = rentingDbContext;
        private readonly IParameterRepositoryPort _parameterRepository = parameterRepository;

        /// <inheritdoc/>
        public async Task<List<Vehicle>> GetVehiclesAllAsync()
        {
            return await _rentingDbContext.Vehicles.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            return await _rentingDbContext.Vehicles.FindAsync(vehicleId);
        }

        /// <inheritdoc/>
        public async Task<Vehicle> GetVehicleByNumberIdAsync(string numberId)
        {
            return await _rentingDbContext.Vehicles
                                .Where(v => v.NumberId == numberId)
                                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            await _rentingDbContext.Vehicles.AddAsync(vehicle);
            await _rentingDbContext.SaveChangesAsync();
            return vehicle;
        }

        /// <inheritdoc/>
        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            var existingVehicle = await _rentingDbContext.Vehicles
                .FirstOrDefaultAsync(c => c.VehicleId == vehicle.VehicleId);

            _rentingDbContext.Entry(existingVehicle).CurrentValues.SetValues(vehicle);

            await _rentingDbContext.SaveChangesAsync();
            return existingVehicle;
        }

        /// <inheritdoc/>
        public async Task UpdateVehiclesAsync(IList<Vehicle> vehicles)
        {
            _rentingDbContext.Vehicles.UpdateRange(vehicles);
            await _rentingDbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Vehicle>> UpdateVehiclesToNoActiveAsync()
        {
            var parameters = await _parameterRepository.GetParametersAsync();

            var yearsAgo = DateTime.Now.AddYears(-parameters.YearsToNoActiveVehicle);
            var vehicles = await _rentingDbContext.Vehicles.Where(v => v.AdquisitionDate < yearsAgo && v.Active).ToListAsync();

            vehicles.ForEach(v => v.Active = false);
            await UpdateVehiclesAsync(vehicles);

            return vehicles;
        }

        /// <inheritdoc/>
        public async Task DeleteVehicleAsync(int vehicleId)
        {
            var vehicle = await _rentingDbContext.Vehicles.FindAsync(vehicleId);
            _rentingDbContext.Vehicles.Remove(vehicle);
            await _rentingDbContext.SaveChangesAsync();
        }
    }
}
