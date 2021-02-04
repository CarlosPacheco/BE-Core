
/*
 * This file is auto-generated, do not edit.
 */

using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using Data.Mapping.Dapper.Extensions;

namespace Data.Mapping.Dapper.Extensions
{

	public static class IDbConnectionExtension
	{
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParent"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChild, TKey>(this IDbConnection cnn, string sql, Type[] typesParent, Func<object[], TParentReturn> mapParent, Type[] typesChild,
            Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChild>> funcParent, Func<object[], TChild> mapChild,
            object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> first = (IList<TParentReturn>)reader.Read(typesParent, mapParent, splitOn, buffered);

            Dictionary<TKey, IList<TChild>> childMap = reader.ReadChild<TKey, TChild>(typesChild, mapChild, splitOn, buffered);

            foreach (TParentReturn item in first)
            {
                if (childMap.TryGetValue(parentKeySelector(item), out IList<TChild> children))
                {
                    funcParent(item, children);
                }
            }

            return first;
        }

        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="reader"><see cref="GridReader"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParent"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChild, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChild>> funcParent, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> first = (IList<TParentReturn>)reader.Read<TParentReturn>(buffered);

            Dictionary<TKey, IList<TChild>> childMap = reader.ReadChild<TKey, TChild>(splitOn, buffered);

            foreach (TParentReturn item in first)
            {
                if (childMap.TryGetValue(parentKeySelector(item), out IList<TChild> children))
                {
                    funcParent(item, children);
                }
            }

            return first;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> results = (IList<TParentReturn>)reader.Read<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (TParentReturn parent in results)
            {
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, children);
                            
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> results = (IList<TParentReturn>)reader.Read<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (TParentReturn parent in results)
            {
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, children);
                            
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> results = (IList<TParentReturn>)reader.Read<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (TParentReturn parent in results)
            {
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, children);
                            
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> results = (IList<TParentReturn>)reader.Read<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (TParentReturn parent in results)
            {
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, children);
                            
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> results = (IList<TParentReturn>)reader.Read<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (TParentReturn parent in results)
            {
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, children);
                            
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            IList<TParentReturn> results = (IList<TParentReturn>)reader.Read<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (TParentReturn parent in results)
            {
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, children);
                            
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, TChildT1, TChildT2, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6),
				typeof(TParentChildT7)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
				TParentChildT7 parentChild7 = (TParentChildT7)row[7];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, parentChild7, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, TChildT1, TChildT2, TChildT3, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6),
				typeof(TParentChildT7)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
				TParentChildT7 parentChild7 = (TParentChildT7)row[7];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, parentChild7, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, TChildT1, TChildT2, TChildT3, TChildT4, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6),
				typeof(TParentChildT7)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
				TParentChildT7 parentChild7 = (TParentChildT7)row[7];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, parentChild7, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6),
				typeof(TParentChildT7)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
				TParentChildT7 parentChild7 = (TParentChildT7)row[7];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, parentChild7, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6),
				typeof(TParentChildT7)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
				TParentChildT7 parentChild7 = (TParentChildT7)row[7];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, parentChild7, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT * From TableParent
        /// 
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="cnn"><see cref="IDbConnection"/> instance</param>
        /// <param name="parentKeySelector">Parent key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParentReturn"/> objects with mapped children</returns>
        public static IList<TParentReturn> Query<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(this IDbConnection cnn, string sql, Func<TParentReturn, TKey> parentKeySelector, Action<TParentReturn, TParentChildT1, TParentChildT2, TParentChildT3, TParentChildT4, TParentChildT5, TParentChildT6, TParentChildT7, IList<TChildT1>> funcParent, Action<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7> funcChild, object param = null, IDbTransaction transaction = null, string splitOn = "Id", bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlMapper.GridReader reader = cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

            Type[] types = {
                typeof(TParentReturn),
                typeof(TParentChildT1),
				typeof(TParentChildT2),
				typeof(TParentChildT3),
				typeof(TParentChildT4),
				typeof(TParentChildT5),
				typeof(TParentChildT6),
				typeof(TParentChildT7)        
            };

            IList<object[]> first = (IList<object[]>)reader.Read(types, objects => objects, splitOn, buffered);
            IList<TParentReturn> results = new List<TParentReturn>();
                         
            Dictionary<TKey, IList<TChildT1>> childMap = reader.ReadChild<TChildT1, TChildT2, TChildT3, TChildT4, TChildT5, TChildT6, TChildT7, TKey>(funcChild, splitOn, buffered);

            foreach (object[] row in first)
            {
 
                TParentReturn parent = (TParentReturn)row[0];
                TParentChildT1 parentChild1 = (TParentChildT1)row[1];
				TParentChildT2 parentChild2 = (TParentChildT2)row[2];
				TParentChildT3 parentChild3 = (TParentChildT3)row[3];
				TParentChildT4 parentChild4 = (TParentChildT4)row[4];
				TParentChildT5 parentChild5 = (TParentChildT5)row[5];
				TParentChildT6 parentChild6 = (TParentChildT6)row[6];
				TParentChildT7 parentChild7 = (TParentChildT7)row[7];
 
                if (childMap.TryGetValue(parentKeySelector(parent), out IList<TChildT1> children))
                {
                    funcParent(parent, parentChild1, parentChild2, parentChild3, parentChild4, parentChild5, parentChild6, parentChild7, children);
                    results.Add(parent);        
                }
            }

            return results;
        }
    
}
}