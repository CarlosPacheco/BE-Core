using System.Collections.Generic;

namespace CrossCutting.SearchFilters.DataAccess
{
    public interface IPaginatedList<T> : IEnumerable<T>
    {
        IReadOnlyCollection<T> Items { get; }

        int PageCurrent { get; }

        int PageSize { get; }

        int? TotalPages { get; }

        int? TotalCount { get; }

        T this[int index] { get; }
    }
}