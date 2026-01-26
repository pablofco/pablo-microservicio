using System;
using System.Collections.Generic;
using AutoMapper;

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
    }
}
