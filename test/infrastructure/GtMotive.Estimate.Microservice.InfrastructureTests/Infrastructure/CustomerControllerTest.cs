using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.Host.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    /// <summary>
    /// Prueba CustomerController.
    /// </summary>
    public class CustomerControllerTest
    {
        /// <summary>
        /// Test GetCustomer_ReturnsOk_WhenRequestIsValid.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetCustomerReturnsOkWhenRequestIsValid()
        {
            // Arrange
            var customerId = 1;
            var mapperPortMock = new Mock<ICustomerMapperPort>();
            mapperPortMock
                .Setup(s => s.GetCustomerByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CustomerDto { CustomerId = 1 });

            var controller = new CustomersController(mapperPortMock.Object);

            // Act
            var result = await controller.GetCustomer(customerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
