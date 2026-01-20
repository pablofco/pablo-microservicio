using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GtMotive.Estimate.Microservice.Domain.Models
{
    /// <summary>
    /// Renting Model.
    /// </summary>
    public class Renting
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
        [ForeignKey("CustomerId")]

        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the VehicleId.
        /// </summary>
        [ForeignKey("VehicleId")]

        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the Customer Navigation properties.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Navigation properties.
        /// </summary>
        public Vehicle Vehicle { get; set; }
    }
}
