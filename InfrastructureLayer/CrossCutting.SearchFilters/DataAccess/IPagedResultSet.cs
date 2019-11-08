using System.Collections.Generic;

namespace CrossCutting.SearchFilters.DataAccess
{
    public interface IPagedCollection<TEntity> : IEnumerable<TEntity>
    {
        IReadOnlyCollection<TEntity> Items { get; set; }

        int Page { get; set; }

        int PageSize { get; set; }

        int? TotalCount { get; set; }

        TEntity this[int index] { get; }

        IPagedCollection<T> Map<T>();
    }
}