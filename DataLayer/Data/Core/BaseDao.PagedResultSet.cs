/*
 * This file is auto-generated, do not edit.
*/
using System;
using Dapper;
using System.Collections.Generic;
using CrossCutting.SearchFilters;
using CrossCutting.SearchFilters.DataAccess;

namespace Data.Core
{
	public partial class BaseDao
	{
	    /// <summary>
        /// Get PagedResultSet from a execute query with not nested object with/without TotalCount using the IncludeMetada
        /// </summary>
        /// <param name="fullSqlQuery">Sql Query to execute</param>
        /// <param name="filter">The SearchFilter </param>
        /// <param name="param">Parameters to blind with the sql query</param>
		/// <param name="buffered"></param>
        /// <returns></returns>
        public IPagedList<TReturn> ExecutePagedQuery<TReturn>(string fullSqlQuery, ISearchFilter filter, object param = null, bool buffered = true)
        {
            // Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);

            IEnumerable<TReturn> entity = reader.Read<TReturn>(buffered);
            int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

            return new PagedList<TReturn>(entity, filter, totalCount);
        }
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, TReturn>(Func<T1, T2, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(func, splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, TReturn>(Func<T1, T2, T3, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(func, splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, TReturn>(Func<T1, T2, T3, T4, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(func, splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, TReturn>(Func<T1, T2, T3, T4, T5, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(func, splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, TReturn>(Func<T1, T2, T3, T4, T5, T6, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(func, splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(func, splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10),
				typeof(T11)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9],
				(T11)objects[10]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10),
				typeof(T11),
				typeof(T12)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9],
				(T11)objects[10],
				(T12)objects[11]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10),
				typeof(T11),
				typeof(T12),
				typeof(T13)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9],
				(T11)objects[10],
				(T12)objects[11],
				(T13)objects[12]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10),
				typeof(T11),
				typeof(T12),
				typeof(T13),
				typeof(T14)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9],
				(T11)objects[10],
				(T12)objects[11],
				(T13)objects[12],
				(T14)objects[13]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10),
				typeof(T11),
				typeof(T12),
				typeof(T13),
				typeof(T14),
				typeof(T15)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9],
				(T11)objects[10],
				(T12)objects[11],
				(T13)objects[12],
				(T14)objects[13],
				(T15)objects[14]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
		 
	    public IPagedList<TReturn> ExecutePagedQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> func, string fullSqlQuery, ISearchFilter filter, object param = null, string splitOn = "id", bool buffered = true)
        {
			// Paging construct helper
            string sqlGetQuery = PagedQueryBuilder.PagedQuery(fullSqlQuery, filter, ref param);

            SqlMapper.GridReader reader = DbConnection.QueryMultiple(sqlGetQuery, param, CurrentTransaction);
			 
            Type[] types = {             
				typeof(T1),
				typeof(T2),
				typeof(T3),
				typeof(T4),
				typeof(T5),
				typeof(T6),
				typeof(T7),
				typeof(T8),
				typeof(T9),
				typeof(T10),
				typeof(T11),
				typeof(T12),
				typeof(T13),
				typeof(T14),
				typeof(T15),
				typeof(T16)        
            };
			
            IList<TReturn> entity = (IList<TReturn>)reader.Read(types, objects => func((T1)objects[0],
				(T2)objects[1],
				(T3)objects[2],
				(T4)objects[3],
				(T5)objects[4],
				(T6)objects[5],
				(T7)objects[6],
				(T8)objects[7],
				(T9)objects[8],
				(T10)objects[9],
				(T11)objects[10],
				(T12)objects[11],
				(T13)objects[12],
				(T14)objects[13],
				(T15)objects[14],
				(T16)objects[15]), splitOn, buffered);

			int? totalCount = filter.IncludeMetadata ? reader.ReadSingleOrDefault<int?>() : null;

			return new PagedList<TReturn>(entity, filter, totalCount);
        }	  
	}
} 