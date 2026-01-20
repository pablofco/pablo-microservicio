using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos
{
    /// <summary>
    /// CustomerRentingsDto Model.
    /// </summary>
    public class CustomerRentingsDto
    {
        /// <summary>
        /// Gets or sets Customer.
        /// </summary>
        public CustomerDto Customer { get; set; }

        /// <summary>
        /// Gets or sets a list of Rentings for the Customer.
        /// </summary>
        public IList<RentingCustomerVehicleDto> Rentings { get; set; } = [];
    }
}
