using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos
{
    /// <summary>
    /// VehicleDto Model.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// Gets or sets VehicleId.
        /// </summary>
        [Key]
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets NumberId unique of the vehicle.
        /// </summary>
        public string NumberId { get; set; }

        /// <summary>
        /// Gets or sets the color of the vehicle.
        /// </summary>
        public Colors Color { get; set; }

        /// <summary>
        /// Gets or sets the doors of the vehicle.
        /// </summary>
        public Doors Doors { get; set; }

        /// <summary>
        /// Gets or sets the date of adquisition of the vehicle.
        /// </summary>
        public DateTime AdquisitionDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the vehicle is active in the float.
        /// </summary>
        public bool? Active { get; set; }
    }
}
