using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Adapters;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    /// <summary>
    /// Prueba Unitaria.
    /// </summary>
    public class CustomersPortTest
    {
        [Fact]
        public async Task GetCustomerByIdReturnsDtoWhenCustomerExists()
        {
            // Arrange
            var repoPortMock = new Mock<ICustomerRepositoryPort>();
            var mapperPortMock = new Mock<ICustomerMapperPort>();
            var mapperMock = new Mock<IMapper>();

            repoPortMock
                .Setup(r => r.GetCustomerByIdAsync(1))
                .ReturnsAsync(new Customer { CustomerId = 1 });

            mapperPortMock
                .Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CustomerDto { CustomerId = 1 });

            mapperMock
                .Setup(m => m.Map<CustomerDto>(It.IsAny<Customer>()))
                .Returns(new CustomerDto
                {
                    CustomerId = 1,
                    Name = "Test"
                });

            var customerAdapter = new CustomerMapper(repoPortMock.Object, mapperMock.Object);

            // Act
            var result = await customerAdapter.GetCustomerByIdAsync(1);

            // Assert
            result.CustomerId.Should().Be(1);
        }
    }
}
