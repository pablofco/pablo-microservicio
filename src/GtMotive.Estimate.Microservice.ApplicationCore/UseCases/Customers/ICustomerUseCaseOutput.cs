using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers
{
    /// <summary>
    /// Interface customer output.
    /// </summary>
    /// <typeparam name="TResult">generic.</typeparam>
    public interface ICustomerUseCaseOutput<TResult>
    {
        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="customerDto">dto.</param>
        /// <returns>vehiculo.</returns>
        Task<(string Message, CustomerDto Model)> ExecuteAsync(CustomerDto customerDto);

        /// <summary>
        /// HandleError.
        /// </summary>
        /// <param name="errorMessage">error.</param>
        /// <returns>string.</returns>
        string HandleError(string errorMessage);
    }
}
