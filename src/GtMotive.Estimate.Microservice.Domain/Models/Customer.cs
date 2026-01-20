using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.Domain.Models
{
    /// <summary>
    /// Customer Model.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets customerId.
        /// </summary>
        [Key]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets DocumentType.
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Gets or sets Document DNI/ Passport.
        /// </summary>
        [StringLength(12)]
        public string Document { get; set; }

        /// <summary>
        /// Gets or sets BirthDate.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets Retings Navigation properties.
        /// </summary>
        public ICollection<Renting> Rentings { get; }
    }
}
