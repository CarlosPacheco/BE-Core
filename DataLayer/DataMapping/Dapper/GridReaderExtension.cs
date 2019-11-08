
/*
 * This file is auto-generated, do not edit.
 */

using System;
using Dapper;
using System.Collections.Generic;

 namespace Data.Mapping.Dapper
{
	public static class GridReaderExtension
	{
		 
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					,(T11)objects[10]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					,(T11)objects[10]
					,(T12)objects[11]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					,(T11)objects[10]
					,(T12)objects[11]
					,(T13)objects[12]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					,(T11)objects[10]
					,(T12)objects[11]
					,(T13)objects[12]
					,(T14)objects[13]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					,(T11)objects[10]
					,(T12)objects[11]
					,(T13)objects[12]
					,(T14)objects[13]
					,(T15)objects[14]
					), splitOn, buffered);
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

            return (IList<TReturn>)reader.Read(types, objects => func((TFirst)objects[0], (TSecond)objects[1], (TThird)objects[2], (TFourth)objects[3], (TFifth)objects[4], (TSixth)objects[5], (TSeventh)objects[6]
                    ,(T8)objects[7]
					,(T9)objects[8]
					,(T10)objects[9]
					,(T11)objects[10]
					,(T12)objects[11]
					,(T13)objects[12]
					,(T14)objects[13]
					,(T15)objects[14]
					,(T16)objects[15]
					), splitOn, buffered);
        }	  
	}
}