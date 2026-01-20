using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Services;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Services
{
    /// <summary>
    /// CustomerService.
    /// </summary>
    public class CustomerService(ICustomerRepository customerRepository,
        IMapper mapper) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<List<CustomerDto>> GetCustomersAllAsync()
        {
            var customers = await _customerRepository.GetCustomersAllAsync();
            var customersDto = ServiceHelper.ConvertToList<Customer, CustomerDto>(_mapper, customers);

            return (List<CustomerDto>)customersDto;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            var customerDto = ServiceHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDto;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> GetCustomerByDocumentAsync(string document)
        {
            var customer = await _customerRepository.GetCustomerByDocumentAsync(document);
            var customerDto = ServiceHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDto;
        }

        /// <inheritdoc/>
        public async Task<List<CustomerRentingsDto>> GetCustomersWithRentingsAsync()
        {
            var customers = await _customerRepository.GetCustomersWithRentingsAsync();
            var customerWithRentingsDto = ServiceHelper.ConvertToList<Customer, CustomerRentingsDto>(_mapper, customers);

            return (List<CustomerRentingsDto>)customerWithRentingsDto;
        }

        /// <inheritdoc/>
        public async Task<List<CustomerRentingsDto>> GetCustomersWithRentingsAndVehicleActiveAsync()
        {
            var customers = await _customerRepository.GetCustomersWithRentingsAndVehicleActive();
            var customersWithRentingsAndVehicleActiveFleetDto = ServiceHelper.ConvertToList<Customer, CustomerRentingsDto>(_mapper, customers);

            return (List<CustomerRentingsDto>)customersWithRentingsAndVehicleActiveFleetDto;
        }

        /// <inheritdoc/>
        public async Task<List<CustomerRentingsDto>> GetCustomersWithRentingsAndVehicleNoActiveAsync()
        {
            var customers = await _customerRepository.GetCustomersWithRentingsAndVehicleNoActiveAsync();
            var customersWithRentingsAndVehicleNoActiveFleetDto = ServiceHelper.ConvertToList<Customer, CustomerRentingsDto>(_mapper, customers);

            return (List<CustomerRentingsDto>)customersWithRentingsAndVehicleNoActiveFleetDto;
        }

        /// <inheritdoc/>
        public async Task<List<CustomerRentingsDto>> GetCustomersWithRentingsAndVehicleNoReturnYetAsync()
        {
            var customers = await _customerRepository.GetCustomersWithRentingsAndVehicleNoReturnYet();
            var customersWithRentingsAndVehicleNoReturnYetDto = ServiceHelper.ConvertToList<Customer, CustomerRentingsDto>(_mapper, customers);

            return (List<CustomerRentingsDto>)customersWithRentingsAndVehicleNoReturnYetDto;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = ServiceHelper.ConvertToEntity<CustomerDto, Customer>(_mapper, customerDto);
            customer = await _customerRepository.AddCustomerAsync(customer);
            var customerDtoAdded = ServiceHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDtoAdded;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = ServiceHelper.ConvertToEntity<CustomerDto, Customer>(_mapper, customerDto);
            customer = await _customerRepository.UpdateCustomerAsync(customer);
            var customerDtoUpdated = ServiceHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
        }

        /// <inheritdoc/>
        public bool ValidateDocumentType(CustomerDto customerDto)
        {
            return ServiceHelper.ValidateDocumentType(customerDto);
        }
    }
}
