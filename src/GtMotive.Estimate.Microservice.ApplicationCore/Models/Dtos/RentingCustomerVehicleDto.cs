using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos
{
    /// <summary>
    /// RentingCustomerVehicleDto Model.
    /// </summary>
    public class RentingCustomerVehicleDto
    {
        /// <summary>
        /// Gets or sets RentingId.
        /// </summary>
        [Key]
        public int RentingId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the DateStart of the renting.
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the DateEnd of the renting.
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Price with DateEnd of the renting.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the DateEndReal of the renting.
        /// </summary>
        public DateTime? DateEndReal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the PriceReal with DateEndReal of the renting.
        /// </summary>
        public double? PriceReal { get; set; }

        /// <summary>
        /// Gets or sets the CustomerId.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a vulue of Customer.
        /// </summary>
        public CustomerDto Customer { get; set; }

        /// <summary>
        /// Gets or sets the VehicleId.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets a vulue of Vehicle.
        /// </summary>
        public VehicleDto Vehicle { get; set; }
    }
}
