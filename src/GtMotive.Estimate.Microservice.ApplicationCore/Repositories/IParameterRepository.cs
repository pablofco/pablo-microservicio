using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Repositories
{
    /// <summary>
    /// Interface IParameterRepository.
    /// </summary>
    public interface IParameterRepository
    {
        /// <summary>
        /// Get the list of all parameters <see cref="Customer"/>.
        /// </summary>
        /// <returns>parameters.</returns>
        Task<Parameter> GetParametersAsync();
    }
}
