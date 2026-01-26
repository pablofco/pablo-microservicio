using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
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

            var createVehicleUseCaseMock = new Mock<IVehicleUseCaseOutput<CreateVehicleUseCaseOutput>>();
            var editVehicleUseCaseMock = new Mock<IVehicleUseCaseOutput<EditVehicleUseCaseOutput>>();
            var mapperPortMock = new Mock<IVehicleMapperPort>();

            mapperPortMock
                .Setup(s => s.GetVehicleByNumberIdAsync(It.IsAny<string>()))
                .ReturnsAsync(valueNull);

            vehicleDto.VehicleId = 50;

            mapperPortMock
                .Setup(s => s.AddVehicleAsync(It.IsAny<VehicleDto>()))
                .ReturnsAsync(vehicleDto);

            createVehicleUseCaseMock.Setup(s => s.ExecuteAsync(It.IsAny<VehicleDto>()))
                .ReturnsAsync(("Ok", new VehicleDto { VehicleId = 50 }));

            var controller = new VehiclesController(mapperPortMock.Object, createVehicleUseCaseMock.Object, editVehicleUseCaseMock.Object);

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

            var createVehicleUseCaseMock = new Mock<IVehicleUseCaseOutput<CreateVehicleUseCaseOutput>>();
            var editVehicleUseCaseMock = new Mock<IVehicleUseCaseOutput<EditVehicleUseCaseOutput>>();
            var mapperPortMock = new Mock<IVehicleMapperPort>();

            mapperPortMock
                .Setup(s => s.GetVehicleByNumberIdAsync(It.IsAny<string>()))
                .ReturnsAsync(valueNull);

            vehicleDto.VehicleId = 50;

            mapperPortMock
                .Setup(s => s.AddVehicleAsync(It.IsAny<VehicleDto>()))
                .ReturnsAsync(vehicleDto);

            createVehicleUseCaseMock.Setup(s => s.ExecuteAsync(It.IsAny<VehicleDto>()))
                .ReturnsAsync(("Vehicle already exist 2884-FDS.", null));

            var controller = new VehiclesController(mapperPortMock.Object, createVehicleUseCaseMock.Object, editVehicleUseCaseMock.Object);

            // Act
            var result = await controller.PostVehicle(vehicleDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest!.Value.Should().Be("Vehicle already exist 2884-FDS.");
        }
    }
}
