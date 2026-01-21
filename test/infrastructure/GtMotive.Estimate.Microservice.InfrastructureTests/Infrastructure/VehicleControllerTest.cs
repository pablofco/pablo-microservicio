using System.Threading.Tasks;
using AutoFixture;
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
    /// Prueba VehicleControllerTest.
    /// </summary>
    public class VehicleControllerTest
    {
        /// <summary>
        /// Test GetCustomer_ReturnsOk_WhenRequestIsValid.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task PostVehicleControllerOK()
        {
            // Arrange
            var fixture = new Fixture();
            var vehicleDto = fixture.Build<VehicleDto>().With(x => x.VehicleId, 0).Create();
            VehicleDto valueNull = null;

            var mapperPortMock = new Mock<IVehicleMapperPort>();
            mapperPortMock
                .Setup(s => s.ValidateColor(It.IsAny<VehicleDto>()))
                .Returns(true);

            mapperPortMock
                .Setup(s => s.ValidatePort(It.IsAny<VehicleDto>()))
                .Returns(true);

            mapperPortMock
                .Setup(s => s.GetVehicleByNumberIdAsync(It.IsAny<string>()))
                .ReturnsAsync(valueNull);

            vehicleDto.VehicleId = 50;

            mapperPortMock
                .Setup(s => s.AddVehicleAsync(It.IsAny<VehicleDto>()))
                .ReturnsAsync(vehicleDto);

            var controller = new VehiclesController(mapperPortMock.Object);

            // Act
            var result = await controller.PostVehicle(vehicleDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        /// <summary>
        /// Test GetCustomer_ReturnsOk_WhenRequestIsValid.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task PostVehicleControllerKO()
        {
            // Arrange
            var fixture = new Fixture();
            var vehicleDto = fixture.Build<VehicleDto>().With(x => x.VehicleId, 0).Create();
            VehicleDto valueNull = null;

            var mapperPortMock = new Mock<IVehicleMapperPort>();
            mapperPortMock
                .Setup(s => s.ValidateColor(It.IsAny<VehicleDto>()))
                .Returns(true);

            mapperPortMock
                .Setup(s => s.ValidatePort(It.IsAny<VehicleDto>()))
                .Returns(false);

            mapperPortMock
                .Setup(s => s.GetVehicleByNumberIdAsync(It.IsAny<string>()))
                .ReturnsAsync(valueNull);

            vehicleDto.VehicleId = 50;

            mapperPortMock
                .Setup(s => s.AddVehicleAsync(It.IsAny<VehicleDto>()))
                .ReturnsAsync(vehicleDto);

            var controller = new VehiclesController(mapperPortMock.Object);

            // Act
            var result = await controller.PostVehicle(vehicleDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
