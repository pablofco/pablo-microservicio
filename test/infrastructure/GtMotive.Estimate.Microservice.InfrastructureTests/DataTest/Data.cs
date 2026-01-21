using System;
using System.Collections.Generic;
using System.Globalization;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Models;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.DataTest
{
    /// <summary>
    /// Data.
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// GetCustomers.
        /// </summary>
        /// <returns>list of Customers.</returns>
        public static IList<Customer> GetCustomers()
        {
            return new List<Customer>
           {
               new() { CustomerId = 1, Name = "Pablo", LastName = "Garcia", DocumentType = DocumentType.DNI, Document = "44714852M", BirthDate = DateTime.Parse("1993/02/06", new CultureInfo("es-ES")) },
               new() { CustomerId = 2, Name = "Andrea", LastName = "Garcia", DocumentType = DocumentType.Passport, Document = "44693254L", BirthDate = null },
               new() { CustomerId = 3, Name = "Pablo Antonio", LastName = "Garcia", DocumentType = DocumentType.DNI, Document = "44965142V", BirthDate = null }
           };
        }

        /// <summary>
        /// GetVehicles.
        /// </summary>
        /// <returns>list of Vehicles.</returns>
        public static IList<Vehicle> GetVehicles()
        {
            return new List<Vehicle>
           {
               new() { VehicleId = 1, NumberId = "VH001", Color = Colors.Red, Ports = Ports.Three, AdquisitionDate = DateTime.Parse("2015/05/10", new CultureInfo("es-ES")), Active = true },
               new() { VehicleId = 2, NumberId = "VH002", Color = Colors.Blue, Ports = Ports.Five, AdquisitionDate = DateTime.Parse("2025/02/12", new CultureInfo("es-ES")), Active = true }
           };
        }

        /// <summary>
        /// GetRentings.
        /// </summary>
        /// <returns>list of rentings.</returns>
        public static IList<Renting> GetRentings()
        {
            return new List<Renting>
           {
               new() { RentingId = 1, CustomerId = 1, VehicleId = 1, DateStart = DateTime.Parse("2025/07/12", new CultureInfo("es-ES")), DateEnd = DateTime.Parse("2025/07/18", new CultureInfo("es-ES")), Price = 240, DateEndReal = DateTime.Parse("2025/07/18", new CultureInfo("es-ES")), PriceReal = 240 },
               new() { RentingId = 2, CustomerId = 2, VehicleId = 2, DateStart = DateTime.Parse("2025/08/08", new CultureInfo("es-ES")), DateEnd = DateTime.Parse("2025/08/10", new CultureInfo("es-ES")), Price = 80 },
               new() { RentingId = 3, CustomerId = 3, VehicleId = 2, DateStart = DateTime.Parse("2025/07/20", new CultureInfo("es-ES")), DateEnd = DateTime.Parse("2025/07/24", new CultureInfo("es-ES")), Price = 160, DateEndReal = DateTime.Parse("2025/07/25", new CultureInfo("es-ES")), PriceReal = 200 }
           };
        }

        /// <summary>
        /// GetParameters.
        /// </summary>
        /// <returns>parameter object.</returns>
        public static Parameter GetParameter()
        {
            return new Parameter
            {
                ParameterId = 1,
                PreciPerDay = 40,
                YearsToNoActiveVehicle = 5
            };
        }
    }
}
