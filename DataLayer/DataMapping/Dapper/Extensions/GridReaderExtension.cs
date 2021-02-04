
/*
 * This file is auto-generated, do not edit.
 */

using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

 namespace Data.Mapping.Dapper.Extensions
{
    public class DapperManyToMany<TKey, T>
    {
        public TKey ParentId { get; set; }

        public T Child { get; set; }
    }
	
    public static class GridReaderExtension
	{
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TParentKey, TReturn>(this SqlMapper.GridReader reader, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child) =>
            {
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }

        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
	    public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TParentKey, TReturn>(this SqlMapper.GridReader reader, Type[] typesChild, Func<object[], TReturn> mapChild, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();
            typesChild.Append(typeof(DapperManyToMany<TParentKey, TReturn>));

            reader.Read(typesChild, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects.Last();

                object[] newArray = new object[objects.Length - 1];
                Array.Copy(objects, 0, newArray, 0, newArray.Length);

                wrapper.Child = mapChild(newArray);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TReturn, T2, TParentKey>(this SqlMapper.GridReader reader, Action<TReturn, T2> func, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, T2, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child, arg2) =>
            {
                func(child, arg2);
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TReturn, T2, T3, TParentKey>(this SqlMapper.GridReader reader, Action<TReturn, T2, T3> func, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, T2, T3, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child, arg2, arg3) =>
            {
                func(child, arg2, arg3);
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TReturn, T2, T3, T4, TParentKey>(this SqlMapper.GridReader reader, Action<TReturn, T2, T3, T4> func, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, T2, T3, T4, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child, arg2, arg3, arg4) =>
            {
                func(child, arg2, arg3, arg4);
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TReturn, T2, T3, T4, T5, TParentKey>(this SqlMapper.GridReader reader, Action<TReturn, T2, T3, T4, T5> func, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, T2, T3, T4, T5, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child, arg2, arg3, arg4, arg5) =>
            {
                func(child, arg2, arg3, arg4, arg5);
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TReturn, T2, T3, T4, T5, T6, TParentKey>(this SqlMapper.GridReader reader, Action<TReturn, T2, T3, T4, T5, T6> func, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, T2, T3, T4, T5, T6, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child, arg2, arg3, arg4, arg5, arg6) =>
            {
                func(child, arg2, arg3, arg4, arg5, arg6);
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }
    	 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
        public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TReturn, T2, T3, T4, T5, T6, T7, TParentKey>(this SqlMapper.GridReader reader, Action<TReturn, T2, T3, T4, T5, T6, T7> func, string splitOn = "Id", bool buffered = true)
        {
            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read<DapperManyToMany<TParentKey, TReturn>, TReturn, T2, T3, T4, T5, T6, T7, DapperManyToMany<TParentKey, TReturn>>
            ((wrapper, child, arg2, arg3, arg4, arg5, arg6, arg7) =>
            {
                func(child, arg2, arg3, arg4, arg5, arg6, arg7);
                wrapper.Child = child;

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper;
            }, splitOn, buffered);
            return childMap;
        }
    		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[8];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[9];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[10];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[11];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[12];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[13];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(T14)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[14];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12], (T14)objects[13]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(T14)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12], (T14)objects[13]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(T14)
				,typeof(T15)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[15];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12], (T14)objects[13], (T15)objects[14]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(T14)
				,typeof(T15)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12], (T14)objects[13], (T15)objects[14]), splitOn, buffered);
        }	  
		 
        /// <summary>
        /// Many to many (N -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// Example:
        /// SELECT Parent.ID AS ParentId, TC.* FROM TableChild TC 
        /// INNER JOIN 
        /// ( 
        ///     SELECT ID From TableParent
        /// ) Parent ON Parent.ID = TC.ParentID
        /// 
        /// </summary>
    	public static Dictionary<TParentKey, IList<TReturn>> ReadChild<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn, TParentKey>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> func, string splitOn = "Id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(T14)
				,typeof(T15)
				,typeof(T16)
				,typeof(DapperManyToMany<TParentKey, TReturn>)               
            };

            Dictionary<TParentKey, IList<TReturn>> childMap = new Dictionary<TParentKey, IList<TReturn>>();

            reader.Read(types, objects =>
            {
                DapperManyToMany<TParentKey, TReturn> wrapper = (DapperManyToMany<TParentKey, TReturn>)objects[16];

                wrapper.Child = func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12], (T14)objects[13], (T15)objects[14], (T16)objects[15]);

                if (childMap.ContainsKey(wrapper.ParentId))
                {
                    childMap[wrapper.ParentId].Add(wrapper.Child);
                }
                else
                {
                    childMap.Add(wrapper.ParentId, new List<TReturn>
                    {
                        wrapper.Child
                    });
                }

                return wrapper.Child;
            }, splitOn, buffered);
            return childMap;
        }

	    public static IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(this SqlMapper.GridReader reader, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> func, string splitOn = "id", bool buffered = true)
        {
            Type[] types = {
                typeof (TFirst),
                typeof (TSecond),
                typeof (TThird),
                typeof (TFourth),
                typeof (TFifth),
                typeof (TSixth),
                typeof (TSeventh)
				,typeof(T8)
				,typeof(T9)
				,typeof(T10)
				,typeof(T11)
				,typeof(T12)
				,typeof(T13)
				,typeof(T14)
				,typeof(T15)
				,typeof(T16)
        
            };

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6], (T8)objects[7], (T9)objects[8], (T10)objects[9], (T11)objects[10], (T12)objects[11], (T13)objects[12], (T14)objects[13], (T15)objects[14], (T16)objects[15]), splitOn, buffered);
        }	  
	}
}