using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Services;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Services
{
    /// <summary>
    /// ParameterService.
    /// </summary>
    public class ParameterService(IParameterRepository parameterRepository) : IParameterService
    {
        private readonly IParameterRepository _parameterRepository = parameterRepository;

        /// <inheritdoc/>
        public async Task<Parameter> GetParametersAsync()
        {
            return await _parameterRepository.GetParametersAsync();
        }
    }
}
