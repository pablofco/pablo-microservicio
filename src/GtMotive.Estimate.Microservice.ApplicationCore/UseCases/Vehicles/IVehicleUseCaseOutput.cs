using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    /// <summary>
    /// Interface vehicle output.
    /// </summary>
    /// <typeparam name="TResult">generic.</typeparam>
    public interface IVehicleUseCaseOutput<TResult>
    {
        /// <summary>
        /// ExecuteAsync.
        /// </summary>
        /// <param name="vehicleDto">dto.</param>
        /// <returns>vehiculo.</returns>
        Task<(string Message, VehicleDto Model)> ExecuteAsync(VehicleDto vehicleDto);

        /// <summary>
        /// HandleError.
        /// </summary>
        /// <param name="errorMessage">error.</param>
        /// <returns>string.</returns>
        string HandleError(string errorMessage);
    }
}
