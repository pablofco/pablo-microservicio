using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.Domain.Models
{
    /// <summary>
    /// Vehicle Model.
    /// </summary>
    public class Vehicle
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
        /// Gets or sets the ports of the vehicle.
        /// </summary>
        public Ports Ports { get; set; }

        /// <summary>
        /// Gets or sets the date of adquisition of the vehicle.
        /// </summary>
        public DateTime AdquisitionDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the vehicle is active in the fleet.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets Retings Navigation properties.
        /// </summary>
        public ICollection<Renting> Rentings { get; }
    }
}
