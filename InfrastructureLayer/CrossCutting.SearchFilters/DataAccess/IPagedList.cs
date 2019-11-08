using System.Collections.Generic;

namespace CrossCutting.SearchFilters.DataAccess
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        IReadOnlyCollection<T> Items { get; set; }

        int PageCurrent { get; set; }

        int PageSize { get; set; }

        int? TotalCount { get; set; }

        T this[int index] { get; }
    }
}