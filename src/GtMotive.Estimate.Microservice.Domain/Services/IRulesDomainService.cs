using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.Domain.Services
{
    /// <summary>
    /// IVehicleDomainService.
    /// </summary>
    public interface IRulesDomainService
    {
        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist color.</returns>
        bool ValidateColor(Vehicle vehicleDto);

        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist color.</returns>
        bool ValidatePort(Vehicle vehicleDto);

        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="customerDto">customerDto.</param>
        /// <returns>true or false.</returns>
        bool ValidateDocumentType(Customer customerDto);
    }
}
