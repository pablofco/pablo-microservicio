using GtMotive.Estimate.Microservice.Domain.Enums;
using System;
using GtMotive.Estimate.Microservice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GtMotive.Estimate.Microservice.Infrastructure.Database
{
    public class HexagonalDbContext(DbContextOptions<HexagonalDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Renting> Rentings { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder?.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Document).HasMaxLength(12);

                entity.HasIndex(e => e.Document).IsUnique();
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasMany(v => v.Rentings)
                      .WithOne(r => r.Vehicle)
                      .HasForeignKey(r => r.VehicleId);

                entity.HasIndex(e => e.NumberId).IsUnique();
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasKey(e => e.ParameterId);
                entity.Property(e => e.PreciPerDay).HasColumnType("numeric(18,2)").IsRequired();
            });

            // Seed data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Pablo", LastName = "Garcia", DocumentType = DocumentType.DNI, Document = "44714852M", BirthDate = DateTime.Parse("1993/02/06", new CultureInfo("es-ES")) },
                new Customer { CustomerId = 2, Name = "Andrea", LastName = "Garcia", DocumentType = DocumentType.Passport, Document = "44693254L", BirthDate = null },
                new Customer { CustomerId = 3, Name = "Pablo Antonio", LastName = "Garcia", DocumentType = DocumentType.DNI, Document = "44965142V", BirthDate = null });

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VehicleId = 1, NumberId = "3729-FDS", Color = Colors.Pink, Doors = Doors.Three, AdquisitionDate = DateTime.Parse("2025/05/10", new CultureInfo("es-ES")), Active = true },
                new Vehicle { VehicleId = 2, NumberId = "0445-BCV", Color = Colors.Blue, Doors = Doors.Five, AdquisitionDate = DateTime.Parse("2025/02/12", new CultureInfo("es-ES")), Active = true },
                new Vehicle { VehicleId = 3, NumberId = "6647-CSM", Color = Colors.Yellow, Doors = Doors.Five, AdquisitionDate = DateTime.Parse("2022/08/15", new CultureInfo("es-ES")), Active = true });

            modelBuilder.Entity<Renting>().HasData(
                new Renting { RentingId = 1, CustomerId = 1, VehicleId = 1, DateStart = DateTime.Parse("2025/07/12", new CultureInfo("es-ES")), DateEnd = DateTime.Parse("2025/07/18", new CultureInfo("es-ES")), Price = 240 },
                new Renting { RentingId = 2, CustomerId = 2, VehicleId = 2, DateStart = DateTime.Parse("2025/08/08", new CultureInfo("es-ES")), DateEnd = DateTime.Parse("2025/08/10", new CultureInfo("es-ES")), Price = 80 },
                new Renting { RentingId = 3, CustomerId = 3, VehicleId = 2, DateStart = DateTime.Parse("2025/07/20", new CultureInfo("es-ES")), DateEnd = DateTime.Parse("2025/07/24", new CultureInfo("es-ES")), Price = 160 });

            modelBuilder.Entity<Parameter>().HasData(
                new Parameter { ParameterId = 1, PreciPerDay = 40, YearsToNoActiveVehicle = 5 });

            // Add-Migration InitialCreate
            // Update-Database
        }
    }
}
