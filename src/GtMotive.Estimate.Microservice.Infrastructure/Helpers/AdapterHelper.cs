using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Models.Dtos;
using GtMotive.Estimate.Microservice.Domain.Enums;

namespace GtMotive.Estimate.Microservice.Infrastructure.Helpers
{
    public static class AdapterHelper
    {
        /// <summary>
        /// Convert List Entity/Dto to List Dto/Entity.
        /// </summary>
        /// <typeparam name="TSource">TSource.</typeparam>
        /// <typeparam name="TDestination">TDestination.</typeparam>
        /// <param name="mapper">mapper.</param>
        /// <param name="source">source.</param>
        /// <returns>list of Dto.</returns>
        public static IList<TDestination> ConvertToList<TSource, TDestination>(IMapper mapper, IList<TSource> source)
        {
            ArgumentNullException.ThrowIfNull(mapper);

            return mapper.Map<IList<TDestination>>(source);
        }

        /// <summary>
        /// Convert Entity to Dto.
        /// </summary>
        /// <typeparam name="TSource">TSource.</typeparam>
        /// <typeparam name="TDestination">TDestination.</typeparam>
        /// <param name="mapper">mapper.</param>
        /// <param name="source">source.</param>
        /// <returns>return Dto.</returns>
        public static TDestination ConvertToDto<TSource, TDestination>(IMapper mapper, TSource source)
        {
            ArgumentNullException.ThrowIfNull(mapper);

            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Convert Dto to Entity.
        /// </summary>
        /// <typeparam name="TSource">TSource.</typeparam>
        /// <typeparam name="TDestination">TDestination.</typeparam>
        /// <param name="mapper">mapper.</param>
        /// <param name="source">source.</param>
        /// <returns>return Entity.</returns>
        public static TDestination ConvertToEntity<TSource, TDestination>(IMapper mapper, TSource source)
        {
            ArgumentNullException.ThrowIfNull(mapper);

            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// ValidateDocumentType.
        /// </summary>
        /// <param name="customerDto">customerDto.</param>
        /// <returns>true or false if exist documentType.</returns>
        public static bool ValidateDocumentType(CustomerDto customerDto)
        {
            var documentTypes = Enum.GetValues<DocumentType>()
                                    .Select(dt => new
                                    {
                                        Id = (int)dt,
                                        Name = dt.ToString()
                                    })
                                    .ToList();

            return documentTypes.Any(dt => (int)customerDto.DocumentType == dt.Id);
        }

        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist color.</returns>
        public static bool ValidateColor(VehicleDto vehicleDto)
        {
            var colors = Enum.GetValues<Colors>()
                                    .Select(dt => new
                                    {
                                        Id = (int)dt,
                                        Name = dt.ToString()
                                    })
                                    .ToList();

            return colors.Any(dt => (int)vehicleDto.Color == dt.Id);
        }

        /// <summary>
        /// ValidateColor.
        /// </summary>
        /// <param name="vehicleDto">vehicleDto.</param>
        /// <returns>true or false if exist color.</returns>
        public static bool ValidatePort(VehicleDto vehicleDto)
        {
            var ports = Enum.GetValues<Ports>()
                                    .Select(dt => new
                                    {
                                        Id = (int)dt,
                                        Name = dt.ToString()
                                    })
                                    .ToList();

            return ports.Any(dt => (int)vehicleDto.Ports == dt.Id);
        }
    }
}
