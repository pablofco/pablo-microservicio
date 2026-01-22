using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.FunctionalTests.DataTest;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="RentingRepositoryTest"/> class.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentingRepositoryTest"/> class.
    /// </remarks>
    public class RentingRepositoryTest
    {
        /// <summary>
        /// Tests the GetRentingByIdShouldReturnVehiclesWhenVehiclesEmpty method of the RentingRepository.
        /// </summary>
        /// <returns>Assert.</returns>
        [Fact]
        public async Task GetRentingByIdShouldReturnVehiclesWhenVehiclesEmpty()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };

            using (var connection = new SqliteConnection(connectionStringBuilder!.ToString()))
            {
                var options = new DbContextOptionsBuilder<HexagonalDbContext>().UseSqlite(connection).Options;

                using (var context = new HexagonalDbContext(options))
                {
                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    // Arrange
                    var vehicleId = Data.GetVehicles().Max(v => v.VehicleId) + 1;

                    var fixture = new Fixture();
                    fixture.Inject(context);

                    var repository = fixture.Build<RetingRepository>().OmitAutoProperties().Create();

                    // Act
                    var vehicles = await repository.GetRentingByVehicleIdAsync(vehicleId);

                    // Assert
                    vehicles.Should().BeNullOrEmpty();
                    vehicles.Should().BeOfType<List<Renting>>();
                }
            }
        }
    }
}
