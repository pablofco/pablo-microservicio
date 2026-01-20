// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Security Hotspot", "S4834:Controlling permissions is security-sensitive", Justification = "Needed for Microsoft.AspNetCore.Authorization.IAuthorizationService", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.Domain.Interfaces.IAuthorizationService")]
[assembly: SuppressMessage("Major Code Smell", "S2326:Unused type parameters should be removed", Justification = "T is necessary for dependency injection.", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.Domain.Interfaces.IAppLogger`1")]
[assembly: SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "No vehicles with 0 Ports", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.Domain.Enums.Ports")]
[assembly: SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "Supress o in DocumentType", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.Domain.Enums.DocumentType")]
[assembly: SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "Colors", Scope = "type", Target = "~T:GtMotive.Estimate.Microservice.Domain.Enums.Colors")]
