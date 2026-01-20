using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Services
{
    /// <summary>
    /// Interface IParameterService.
    /// </summary>
    public interface IParameterService
    {
        /// <summary>
        /// GetParametersAsync.
        /// </summary>
        /// <returns>paremeters.</returns>
        Task<Parameter> GetParametersAsync();
    }
}
