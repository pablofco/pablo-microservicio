using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Models;
using GtMotive.Estimate.Microservice.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    /// <summary>
    /// ParameterRepository.
    /// </summary>
    public class ParameterRepository(HexagonalDbContext rentingDbContext) : IParameterRepository
    {
        private readonly HexagonalDbContext _rentingDbContext = rentingDbContext;

        /// <inheritdoc/>
        public async Task<Parameter> GetParametersAsync()
        {
            return await _rentingDbContext.Parameters.FirstOrDefaultAsync();
        }
    }
}
