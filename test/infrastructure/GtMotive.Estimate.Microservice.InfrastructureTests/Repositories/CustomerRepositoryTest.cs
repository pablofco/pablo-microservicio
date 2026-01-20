using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using GtMotive.Estimate.Microservice.InfrastructureTests.DataTest;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="CustomerRepository"/> class.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CustomerRepositoryTest"/> class.
    /// </remarks>
    public class CustomerRepositoryTest
    {
        /// <summary>
        /// Tests the GetCustomerByIdShouldReturnCustomerWhenCustomerExists method of the CustomerRepository.
        /// </summary>
        /// <returns>Assert.</returns>
        [Fact]
        public async Task GetCustomerByIdShouldReturnCustomerWhenCustomerExists()
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
                    fixture.Inject(context);

                    var customerId = Data.GetCustomers().FirstOrDefault().CustomerId;
                    var repository = fixture.Build<CustomerRepository>().OmitAutoProperties().Create();

                    // Act
                    var customer = await repository.GetCustomerByIdAsync(customerId);

                    // Assert
                    customer.Should().NotBeNull();
                    customer.CustomerId.Should().Be(customerId);
                }
            }
        }
    }
}
