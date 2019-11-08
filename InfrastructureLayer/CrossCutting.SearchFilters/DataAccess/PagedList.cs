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
    public sealed class PagedList<T> : IPagedList<T>
    {
        public PagedList(IList<T> list, int pageCurrent, int pageSize, int? totalCount = null)
        {
            Items = new ReadOnlyCollection<T>(list);
            PageCurrent = pageCurrent;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public PagedList(IList<T> list, ISearchFilter filter, int? totalCount = null)
            : this(list, filter.Page, filter.PageSize, totalCount)
        {
        }

        public PagedList(IEnumerable<T> enumerable, ISearchFilter filter, int? totalCount = null)
            : this(enumerable.ToList(), filter, totalCount)
        {
        }

        public PagedList(IEnumerable<T> enumerable, int page, int pageSize, int? totalCount = null)
            : this(enumerable.ToList(), page, pageSize, totalCount)
        {
        }

        public IReadOnlyCollection<T> Items { get; set; }

        public int PageCurrent { get; set; }

        public int PageSize { get; set; }

        public int? TotalCount { get; set; }

        public T this[int index] => ((ReadOnlyCollection<T>)Items)[index];

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
