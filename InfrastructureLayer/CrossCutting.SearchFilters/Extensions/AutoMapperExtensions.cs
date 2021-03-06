﻿using AutoMapper;
using CrossCutting.SearchFilters.DataAccess;
using System.Collections.Generic;

namespace CrossCutting.SearchFilters.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IEnumerable<TDestination> MapPagedCollection<TDestination, TSource>(this IMapper mapper, IPaginatedList<TSource> source)
        {
            return new PaginatedList<TDestination>(mapper.Map<IList<TDestination>>(source.Items), source.PageCurrent, source.PageSize, source.TotalCount);
        }

        public static IEnumerable<TDestination> MapPaged<TDestination, TSource>(this IMapper mapper, IEnumerable<TSource> source)
        {
            return mapper.MapPagedCollection<TDestination, TSource>((IPaginatedList<TSource>)source);
        }
    }
}
