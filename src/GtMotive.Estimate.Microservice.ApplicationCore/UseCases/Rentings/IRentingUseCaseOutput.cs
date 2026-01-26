using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentings
{
    /// <summary>
    /// Interface vehicle output.
    /// </summary>
    /// <typeparam name="TResult">generic.</typeparam>
    public interface IRentingUseCaseOutput<TResult>
    {
        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="rentingDto">dto.</param>
        /// <returns>renting.</returns>
        Task<(string Message, RentingDto Model)> ExecuteAsync(RentingDto rentingDto);

        /// <summary>
        /// HandleError.
        /// </summary>
        /// <param name="errorMessage">error.</param>
        /// <returns>string.</returns>
        string HandleError(string errorMessage);
    }
}
