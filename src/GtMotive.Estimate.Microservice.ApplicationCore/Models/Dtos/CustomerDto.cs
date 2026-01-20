using System;
using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos
{
    /// <summary>
    /// CustomerDto Model.
    /// </summary>
    public class CustomerDto
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
        /// Gets or sets Document.
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Gets or sets BirthDate.
        /// </summary>
        public DateTime? BirthDate { get; set; }
    }
}
