using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CrossCutting.SearchFilters.DataAccess
{
    /// <summary>
    /// Represents a paged result set.
    /// Encapsulates a list of a business entity objects and paging metadata.
    /// </summary>
    /// <typeparam name="T">Buisiness entity type</typeparam>
    public sealed class PaginatedList<T> : IPaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }

        // better name? PageIndex? PageNumber?
        public int PageCurrent { get; }

        public int PageSize { get; }

        public int? TotalPages { get; }

        public int? TotalCount { get; }

        public T this[int index] => ((ReadOnlyCollection<T>)Items)[index];

        public bool HasPreviousPage => PageCurrent > 1;

        public bool HasNextPage => PageCurrent < TotalPages;

        public PaginatedList(IList<T> list, int pageCurrent, int pageSize, int? totalCount = null)
        {
            Items = new ReadOnlyCollection<T>(list);
            PageCurrent = pageCurrent;
            PageSize = pageSize;
            TotalPages = totalCount.HasValue ? (int?)Math.Ceiling(totalCount.Value / (double)pageSize) : null;
            TotalCount = totalCount;
        }

        public PaginatedList(IList<T> list, ISearchFilter filter, int? totalCount = null)
            : this(list, filter.Page, filter.PageSize, totalCount)
        {
        }

        public PaginatedList(IEnumerable<T> enumerable, ISearchFilter filter, int? totalCount = null)
            : this(enumerable.AsList(), filter, totalCount)
        {
        }

        public PaginatedList(IEnumerable<T> enumerable, int page, int pageSize, int? totalCount = null)
            : this(enumerable.AsList(), page, pageSize, totalCount)
        {
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
