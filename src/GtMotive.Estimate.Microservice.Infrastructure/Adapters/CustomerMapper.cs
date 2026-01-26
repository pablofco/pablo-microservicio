using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Helpers;

namespace GtMotive.Estimate.Microservice.Infrastructure.Adapters
{
    /// <summary>
    /// CustomerService.
    /// </summary>
    public class CustomerMapper(ICustomerRepositoryPort customerRepository,
        IMapper mapper) : ICustomerMapperPort
    {
        private readonly ICustomerRepositoryPort _customerRepository = customerRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc/>
        public async Task<List<CustomerDto>> GetCustomersAllAsync()
        {
            var customers = await _customerRepository.GetCustomersAllAsync();
            var customersDto = AdapterHelper.ConvertToList<Customer, CustomerDto>(_mapper, customers);

            return (List<CustomerDto>)customersDto;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            var customerDto = AdapterHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDto;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> GetCustomerByDocumentAsync(string document)
        {
            var customer = await _customerRepository.GetCustomerByDocumentAsync(document);
            var customerDto = AdapterHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDto;
        }

        /// <inheritdoc/>
        public async Task<List<CustomerRentingsDto>> GetCustomersWithRentingsAsync()
        {
            var customers = await _customerRepository.GetCustomersWithRentingsAsync();
            var customerWithRentingsDto = AdapterHelper.ConvertToList<Customer, CustomerRentingsDto>(_mapper, customers);

            return (List<CustomerRentingsDto>)customerWithRentingsDto;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = AdapterHelper.ConvertToEntity<CustomerDto, Customer>(_mapper, customerDto);
            customer = await _customerRepository.AddCustomerAsync(customer);
            var customerDtoAdded = AdapterHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDtoAdded;
        }

        /// <inheritdoc/>
        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = AdapterHelper.ConvertToEntity<CustomerDto, Customer>(_mapper, customerDto);
            customer = await _customerRepository.UpdateCustomerAsync(customer);
            var customerDtoUpdated = AdapterHelper.ConvertToDto<Customer, CustomerDto>(_mapper, customer);

            return customerDtoUpdated;
        }

        /// <inheritdoc/>
        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
        }
    }
}
