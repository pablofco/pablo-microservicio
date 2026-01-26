using System;
using System.Linq;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.Domain.Services
{
    /// <summary>
    /// VehicleDomainService.
    /// </summary>
    public class RulesDomainService : IRulesDomainService
    {
        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist color.</returns>
        public bool ValidateColor(Vehicle vehicleDto)
        {
            var colors = Enum.GetValues<Colors>()
                                    .Select(dt => new
                                    {
                                        Id = (int)dt,
                                        Name = dt.ToString()
                                    })
                                    .ToList();

            return colors.Any(dt => (int)vehicleDto.Color == dt.Id);
        }

        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist color.</returns>
        public bool ValidatePort(Vehicle vehicleDto)
        {
            var ports = Enum.GetValues<Ports>()
                                    .Select(dt => new
                                    {
                                        Id = (int)dt,
                                        Name = dt.ToString()
                                    })
                                    .ToList();

            return ports.Any(dt => (int)vehicleDto.Ports == dt.Id);
        }

        /// <summary>
        /// ValidateDocumentType.
        /// </summary>
        /// <param name="customerDto">customerDto.</param>
        /// <returns>true or false if exist documentType.</returns>
        public bool ValidateDocumentType(Customer customerDto)
        {
            var documentTypes = Enum.GetValues<DocumentType>()
                                    .Select(dt => new
                                    {
                                        Id = (int)dt,
                                        Name = dt.ToString()
                                    })
                                    .ToList();

            return documentTypes.Any(dt => (int)customerDto.DocumentType == dt.Id);
        }
    }
}
