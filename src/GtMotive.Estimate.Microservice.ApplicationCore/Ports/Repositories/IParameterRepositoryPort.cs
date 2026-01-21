using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories
{
    /// <summary>
    /// Interface IParameterRepository.
    /// </summary>
    public interface IParameterRepositoryPort
    {
        /// <summary>
        /// Get the list of all parameters <see cref="Customer"/>.
        /// </summary>
        /// <returns>parameters.</returns>
        Task<Parameter> GetParametersAsync();
    }
}
