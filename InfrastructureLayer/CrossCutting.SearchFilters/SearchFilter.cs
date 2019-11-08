using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrossCutting.SearchFilters.Attributes;
using CrossCutting.SearchFilters.DataAccess;

namespace CrossCutting.SearchFilters
{
    /// <summary>
    /// Filtering and ordering core definitions for readable and searchable business entities
    /// </summary>
    public class SearchFilter : ISearchFilter
    {
        /// <summary>
        /// A list of properties public declared on derived type.
        /// This list is used to filter out invalid properties from the order/sort paramenter value.
        /// </summary>
        protected static Dictionary<string, PropertyInfo> DerivedTypeProperties;

        /// <summary>
        /// Database table alias used in search/find/get queries for the corresponding business entity
        /// </summary>
        private static DbTableAliasAttribute _dbTableAliasAttribute;

        /// <summary>
        /// Page number backing field
        /// </summary>
        private int _page;

        /// <summary>
        /// Page size backing field
        /// </summary>
        private int _pageSize;

        /// <summary>
        /// OrderBy backing field
        /// </summary>
        private string _orderParameterValue;

        /// <summary>
        /// Structured ordering expression
        /// </summary>
        private SqlOrderByExpression _sqlOrderByExpression;

        /// <summary>
        /// Default page size
        /// </summary>
        protected const int DefaultPageSize = 10;

        /// <summary>
        /// Maximum page size
        /// </summary>
        protected virtual int MaxPageSize => 50;

        /// <summary>
        /// Raw descending order indicator char on order by clause tokens
        /// </summary>
        private const char RawDescendingDirectionPrefix = '-';


        /// <summary>
        /// Initializes an instance of <see cref="SearchFilter"/> with the default values
        /// </summary>
        public SearchFilter()
        {
            ProcessInstanceMetadata();
            //ValidateSearchFilter();
            ProcessOrderingParameter(null);

            Page = 1;
            PageSize = DefaultPageSize;
        }

        public SearchFilter(int pageSize = DefaultPageSize, int page = 1)
        {
            ProcessInstanceMetadata();
            //ValidateSearchFilter();
            ProcessOrderingParameter(null);

            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// Validates instance configurations/values
        /// </summary>
        protected virtual void ValidateSearchFilter()
        {
            if (DerivedTypeProperties.Count == 0)
            {
                return;
            }

            bool searchFilterHasDefaultOrderAttr = DerivedTypeProperties.Count(p => p.Value.GetCustomAttributes(typeof(DefaultOrderingPropertyAttribute), false).Any()) > 0;
            
            if (!searchFilterHasDefaultOrderAttr)
            {
                throw new ApplicationException($"SearchFilter derived type ({GetType().Name}) missing default ordering attribute");
            }
        }

        /// <summary>
        /// Gets the current instance of <see cref="SearchFilter"/> derived type metadata.
        /// Statically stores a list of the public properties and class attributes declared in the derived type.
        /// </summary>
        protected virtual void ProcessInstanceMetadata()
        {
            Type thisType = GetType();
            _dbTableAliasAttribute = thisType.GetCustomAttributes(typeof(DbTableAliasAttribute), true).FirstOrDefault() as DbTableAliasAttribute;
            DerivedTypeProperties = thisType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToDictionary(prop => prop.Name.ToLower(), prop => prop);
        }

        /// <summary>
        /// Sets current instance values based on another instance
        /// </summary>
        /// <param name="filter">Filter instance from which values should be copied</param>
        public void SetValues(SearchFilter filter)
        {
            _page = filter._page;
            _pageSize = filter._pageSize;
            ContentLanguage = filter.ContentLanguage;
            IncludeMetadata = filter.IncludeMetadata;
            OrderBy = filter._orderParameterValue;
        }

        /// <summary>
        /// Page number
        /// </summary>
        public int Page
        {
            get => _page;
            set {
                if (value < 1)
                {
                    throw new Exception($"Page number must be greater than 0");
                }

                _page = value;
            }
        }

        /// <summary>
        /// Number of entities per page
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1 || value > MaxPageSize)
                {
                    throw new Exception($"Page size must be an integer between 1 and {MaxPageSize}");
                }

                _pageSize = value;
            }
        }

        /// <summary>
        /// Order by expression
        /// </summary>
        public string OrderBy
        {
            get => _orderParameterValue;
            set => ProcessOrderingParameter(value);
        }
        
        /// <summary>
        /// Gets the rows offset value based on selected page and page size.
        /// </summary>
        public int Offset => (_page - 1) * _pageSize;

        /// <summary>
        /// Language (unique identifier) in which results will be retrieved 
        /// </summary>
        public int? ContentLanguage { get; set; }

        /// <summary>
        /// Indicates if results metadata such as total record count should be calculated and returned.
        /// </summary>
        public bool IncludeMetadata { get; set; }

        /// <summary>
        /// Returns the full SQL ORDER BY clause expression for the current values
        /// </summary>
        /// <returns>The full SQL ORDER BY clause expression</returns>
        public string SqlOrderByExpression => _sqlOrderByExpression.ToSqlOrderByExpression();

        /// <summary>
        /// Processes the sorting parameter to a structured representation and keeps the original value
        /// </summary>
        /// <param name="sortingParameter">OrderBy expression</param>
        /// <example>Id,-Name</example>
        protected void ProcessOrderingParameter(string sortingParameter)
        {
            _sqlOrderByExpression = _sqlOrderByExpression ?? new SqlOrderByExpression();
            _sqlOrderByExpression.Clear();
            
            // No derived type properties declared, no ordering.
            if (DerivedTypeProperties.Count == 0)
            {
                _orderParameterValue = string.Empty;
                return;
            }

            Tuple<string, SqlOrderDirection, PropertyInfo>[] sqlOrderingTokenInfo;

            // If expression passed on the query string is NOT null or empty, process input
            if (!string.IsNullOrWhiteSpace(sortingParameter))
            {
                sqlOrderingTokenInfo = sortingParameter
                    .ToLower()
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(token =>
                    {
                        token = token.Trim();
                        SqlOrderDirection orderDirection = token.StartsWith(RawDescendingDirectionPrefix.ToString()) ? SqlOrderDirection.Desc : SqlOrderDirection.Asc;
                        token = orderDirection == SqlOrderDirection.Desc ? token.TrimStart(RawDescendingDirectionPrefix) : token;
                        // Check if order-by token exists on search filter (properties) and does not have a PrivateFilteringProperty attribute.
                        bool isAllowedFilterProperty = DerivedTypeProperties.TryGetValue(token, out PropertyInfo propertyInfo) 
                                                       &&
                                                       !propertyInfo.GetCustomAttributes(typeof(PrivateFilteringProperty)).Any();

                        return new
                        {
                            PropertyInfo = propertyInfo,
                            IsAllowedFilterProperty = isAllowedFilterProperty,
                            Direction = orderDirection
                        };
                    })
                    .Where(t => t.IsAllowedFilterProperty)
                    .Select(t => new Tuple<string, SqlOrderDirection, PropertyInfo>(t.PropertyInfo.Name, t.Direction, t.PropertyInfo))
                    .ToArray();
            }
            else // else: Try to get default sorting information from attributes from (derived) filter class
            {
                sqlOrderingTokenInfo = DerivedTypeProperties
                    .Where(kvp => kvp.Value.CustomAttributes.Any(a => a.AttributeType == typeof(DefaultOrderingPropertyAttribute)))
                    .Select(p => new 
                    {
                        PropertyInfo = p.Value,
                        DefaultOrderAttr = (DefaultOrderingPropertyAttribute) p.Value.GetCustomAttributes(typeof(DefaultOrderingPropertyAttribute)).First()
                    })
                    .OrderBy(p => p.DefaultOrderAttr.Position)
                    .Select(p => new Tuple<string, SqlOrderDirection, PropertyInfo>(p.PropertyInfo.Name, p.DefaultOrderAttr.Direction, p.PropertyInfo))
                    .ToArray();
            }

            // Finish with common processing of sorting tokens by reading alias for table and/or column
            foreach (Tuple<string, SqlOrderDirection, PropertyInfo> tokenInfo in sqlOrderingTokenInfo)
            {
                DbTableAliasAttribute dbTableAlias = tokenInfo.Item3.GetCustomAttributes(typeof(DbTableAliasAttribute)).FirstOrDefault() as DbTableAliasAttribute ?? _dbTableAliasAttribute;
                string tokenSqlColName = tokenInfo.Item3.GetCustomAttributes(typeof(DbTableColumnAttribute)).FirstOrDefault() is DbTableColumnAttribute dbTableColumn ? dbTableColumn.DbColumnName : tokenInfo.Item3.Name;

                _sqlOrderByExpression.AddOrderToken(tokenSqlColName, tokenInfo.Item2, dbTableAlias?.SqlTableAlias);
            }
            
            _orderParameterValue = sortingParameter;
        }
    }

    /// <summary>
    /// Represents an Order By Sql clause expression by it's tokens.
    /// Each token represents a column and an order direction (Asc or Desc).
    /// </summary>
    internal sealed class SqlOrderByExpression
    {
        /// <summary>
        /// The ordering tokens list
        /// </summary>
        private List<SqlOrderByToken> OrderTokens { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="SqlOrderByExpression"/> with the default values
        /// </summary>
        public SqlOrderByExpression()
        {
            OrderTokens = new List<SqlOrderByToken>();
        }

        /// <summary>
        /// Adds a new order by token to the expression
        /// </summary>
        /// <param name="token">Property/Column name</param>
        /// <param name="table">Table name/alias</param>
        public void AddOrderToken(string token, string table = null)
        {
            SqlOrderByToken sqlOrderToken = new SqlOrderByToken(token, table);
            OrderTokens.Add(sqlOrderToken);
        }

        /// <summary>
        /// Adds a new order by token to the expression
        /// </summary>
        /// <param name="token">Property/Column name</param>
        /// <param name="direction">Order by direction, Asc or Desc</param>
        /// <param name="table">Table name/alias</param>
        public void AddOrderToken(string token, SqlOrderDirection direction = SqlOrderDirection.Asc, string table = null)
        {
            SqlOrderByToken sqlOrderToken = new SqlOrderByToken(token, direction, table);
            OrderTokens.Add(sqlOrderToken);
        }

        /// <summary>
        /// Clears all tokens stored
        /// </summary>
        public void Clear()
        {
            OrderTokens.Clear();
        }

        /// <summary>
        /// Gets a string representation of all ordering tokens, compatible with Sql
        /// </summary>
        /// <returns></returns>
        public string ToSqlOrderByExpression()
        {
            string sqlOrderByExpression = string.Join(",", OrderTokens.Select(t => $"{(!string.IsNullOrWhiteSpace(t.Table) ? $"{t.Table}." : null)}{t.Column} {t.SortDirection}"));

            return sqlOrderByExpression;
        }

        /// <summary>
        /// Represents an Order By Sql clause expression token.
        /// Each token represents a column (name) and an order direction (Asc or Desc).
        /// </summary>
        public sealed class SqlOrderByToken
        {
            /// <summary>
            /// Initializes an instance of <see cref="SqlOrderByToken"/> from a string.
            /// </summary>
            /// <param name="orderByToken">Order token to add to the collection</param>
            /// <param name="tokenTable"></param>
            /// <example>"Id", "-Date" or "-Date, Name"</example>
            public SqlOrderByToken(string orderByToken, string tokenTable = null)
            {
                Table = tokenTable;
                SortDirection = orderByToken.StartsWith("-") ? SqlOrderDirection.Desc : SqlOrderDirection.Asc;
                Column = (SortDirection == SqlOrderDirection.Desc) ? orderByToken.TrimStart('-') : orderByToken;
            }

            /// <summary>
            /// Initializes an instance of <see cref="SqlOrderByToken"/> from a string.
            /// </summary>
            /// <param name="orderByToken">Order token to add to the collection</param>
            /// <param name="direction">Order by direction, Asc or Desc</param>
            /// <param name="tokenTable"></param>
            /// <example>"Id", "-Date" or "-Date, Name"</example>
            public SqlOrderByToken(string orderByToken, SqlOrderDirection direction = SqlOrderDirection.Asc, string tokenTable = null)
            {
                Table = tokenTable;
                SortDirection = direction;
                Column = orderByToken.TrimStart('-');
            }

            /// <summary>
            /// The oredering direction
            /// </summary>
            public SqlOrderDirection SortDirection { get; }

            /// <summary>
            /// The property/column name
            /// </summary>
            public string Column { get; }

            /// <summary>
            /// Table name/alias
            /// </summary>
            public string Table { get; }
        }
    }
}