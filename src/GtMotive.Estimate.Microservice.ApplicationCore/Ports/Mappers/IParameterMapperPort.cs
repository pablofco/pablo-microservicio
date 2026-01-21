using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers
{
    /// <summary>
    /// Interface IParameterService.
    /// </summary>
    public interface IParameterMapperPort
    {
        /// <summary>
        /// GetParametersAsync.
        /// </summary>
        /// <returns>paremeters.</returns>
        Task<Parameter> GetParametersAsync();
    }
}
