// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "Pending migration to LoggerMessage.", Scope = "member", Target = "~T:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1")]
[assembly: SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "Pending migration to LoggerMessage.", Scope = "member", Target = "~T:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not null. From dependency injection.", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.MongoDb.MongoService.#ctor(Microsoft.Extensions.Options.IOptions{GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings.MongoDbSettings})")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "No Static Method", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Adapters.CustomerMapper.ValidateDocumentType(GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos.CustomerDto)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Conditional", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Adapters.RentingMapper.ValidateRentingDates(System.DateTime,System.Nullable{System.DateTime})~System.ValueTuple{System.Boolean,System.String}")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Conditional", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Adapters.RentingMapper.ValidateCanRentingWithVehicleIdAsync(System.Int32,System.DateTime)~System.Threading.Tasks.Task{System.ValueTuple{System.Boolean,System.String}}")]
[assembly: SuppressMessage("Style", "IDE0305:Convert to conditional expression", Justification = "Conditional", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Adapters.RentingMapper.ValidateCanRentingWithVehicleIdAsync(System.Int32,System.DateTime)~System.Threading.Tasks.Task{System.ValueTuple{System.Boolean,System.String}}")]
