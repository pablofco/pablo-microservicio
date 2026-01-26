using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers
{
    /// <summary>
    /// CreatecustomerUseCase.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateCustomerUseCaseOutput"/> class.
    /// Constructor.
    /// </remarks>
    /// <param name="customerMapperPort">customer.</param>
    public class CreateCustomerUseCaseOutput(
        ICustomerMapperPort customerMapperPort) : ICustomerUseCaseOutput<CreateCustomerUseCaseOutput>
    {
        private readonly ICustomerMapperPort _customerMapperPort = customerMapperPort;

        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="customerDto">objeto.</param>
        /// <returns>rules.</returns>
        public async Task<(string Message, CustomerDto Model)> ExecuteAsync(CustomerDto customerDto)
        {
            ArgumentNullException.ThrowIfNull(customerDto);

            var customer = new Customer
            {
                CustomerId = customerDto.CustomerId,
                Name = customerDto.Name,
                LastName = customerDto.LastName,
                DocumentType = customerDto.DocumentType,
                Document = customerDto.Document,
                BirthDate = customerDto.BirthDate,
            };

            if (await _customerMapperPort.GetCustomerByDocumentAsync(customer.Document) != null)
            {
                return ($"Customer with Document Number:{customer.Document}, already exist.", null);
            }

            customerDto = await _customerMapperPort.AddCustomerAsync(customerDto);

            return ("Ok", customerDto);
        }

        /// <summary>
        /// Handle error.
        /// </summary>
        /// <param name="errorMessage">errorMessage.</param>
        /// <returns>string.</returns>
        public string HandleError(string errorMessage)
        {
            return $"An error occurred while adding the customer. {errorMessage}";
        }
    }
}
