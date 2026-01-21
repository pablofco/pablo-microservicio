using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Mappers;
using GtMotive.Estimate.Microservice.ApplicationCore.Ports.Repositories;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.Infrastructure.Adapters
{
    /// <summary>
    /// ParameterService.
    /// </summary>
    public class ParameterMapper(IParameterRepositoryPort parameterRepository) : IParameterMapperPort
    {
        private readonly IParameterRepositoryPort _parameterRepository = parameterRepository;

        /// <inheritdoc/>
        public async Task<Parameter> GetParametersAsync()
        {
            return await _parameterRepository.GetParametersAsync();
        }
    }
}
