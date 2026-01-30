using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentings;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Adapters;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IVehicleRepositoryPort, VehicleRepository>();
            services.AddScoped<ICustomerRepositoryPort, CustomerRepository>();
            services.AddScoped<IRentingRepositoryPort, RetingRepository>();
            services.AddScoped<IParameterRepositoryPort, ParameterRepository>();
            services.AddScoped<IVehicleMapperPort, VehicleMapper>();
            services.AddScoped<ICustomerMapperPort, CustomerMapper>();
            services.AddScoped<IRentingMapperPort, RentingMapper>();
            services.AddScoped<IParameterMapperPort, ParameterMapper>();

            services.AddScoped<IVehicleUseCaseOutput<CreateVehicleUseCaseOutput>, CreateVehicleUseCaseOutput>();
            services.AddScoped<IVehicleUseCaseOutput<EditVehicleUseCaseOutput>, EditVehicleUseCaseOutput>();

            services.AddScoped<ICustomerUseCaseOutput<CreateCustomerUseCaseOutput>, CreateCustomerUseCaseOutput>();
            services.AddScoped<ICustomerUseCaseOutput<EditCustomerUseCaseOutput>, EditCustomerUseCaseOutput>();

            services.AddScoped<IRentingUseCaseOutput<CreateRentingUseCaseOutput>, CreateRentingUseCaseOutput>();
            services.AddScoped<IRentingUseCaseOutput<EditRentingUseCaseOutput>, EditRentingUseCaseOutput>();

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            return new InfrastructureBuilder(services);
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
