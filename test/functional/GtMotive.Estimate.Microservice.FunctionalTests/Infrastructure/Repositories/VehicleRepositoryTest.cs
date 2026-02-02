using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.FunctionalTests.DataTest;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="VehicleRepositoryTest"/> class.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VehicleRepositoryTest"/> class.
    /// </remarks>
    public class VehicleRepositoryTest
    {
        /// <summary>
        /// Tests the GetVehiclesShouldReturnVehiclesWhenVehiclesExists method of the VehicleRepository.
        /// </summary>
        /// <returns>Assert.</returns>
        [Fact]
        public async Task GetVehiclesShouldReturnVehiclesWhenVehiclesExists()
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
                    var fixture = new Fixture();
                    fixture.Customize(new AutoMoqCustomization());

                    fixture.Inject(context);

                    var parameterRepositoryMock = fixture.Freeze<Mock<IParameterRepositoryPort>>();
                    fixture.Inject(parameterRepositoryMock);

                    var repository = fixture.Build<VehicleRepository>().OmitAutoProperties().Create();

                    // Act
                    var vehicles = await repository.GetVehiclesAllAsync();

                    // Assert
                    vehicles.Should().NotBeNull();
                    vehicles.Should().BeOfType<List<Vehicle>>();
                    vehicles.Select(v => v.VehicleId).Should().Contain(Data.GetVehicles().Select(x => x.VehicleId).ToList());
                }
            }
        }
    }
}
