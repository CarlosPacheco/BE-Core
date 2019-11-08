namespace CrossCutting.SearchFilters
{
    public interface ISearchFilter
    {
        /// <summary>
        /// Page number
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets the rows offset value based on selected page and page size.
        /// </summary>
        int Offset { get; }

        /// <summary>
        /// Number of entities per page
        /// </summary>
        int PageSize { get; set; }
        
        /// <summary>
        /// Sorting field name
        /// </summary>
        string OrderBy { get; set; }

        /// <summary>
        /// Language (unique identifier) in which results will be retrieved 
        /// </summary>
        int? ContentLanguage { get; set; }

        /// <summary>
        /// Indicates if results metadata such as total record count should be calculated and returned.
        /// </summary>
        bool IncludeMetadata { get; set; }

        /// <summary>
        /// Returns the full SQL ORDER BY clause expression for the current values
        /// </summary>
        /// <returns>The full SQL ORDER BY clause expression</returns>
        string SqlOrderByExpression { get; }
    }
}
