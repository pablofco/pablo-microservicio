using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Domain.Models
{
    /// <summary>
    /// Parameter.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Gets or sets ParameterId.
        /// </summary>
        [Key]
        public int ParameterId { get; set; }

        /// <summary>
        /// Gets or sets PreciPerDay.
        /// </summary>
        public double PreciPerDay { get; set; }

        /// <summary>
        /// Gets or sets YearsToNoActiveVehicle.
        /// </summary>
        public int YearsToNoActiveVehicle { get; set; }
    }
}
