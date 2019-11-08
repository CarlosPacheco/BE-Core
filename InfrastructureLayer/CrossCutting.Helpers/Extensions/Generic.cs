using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace CrossCutting.Helpers.Extensions
{
    public static class Generic
    {
        /// <summary>
        /// Checks if an object is a <see cref="DBNull"/> and if true returns null
        /// </summary>
        /// <param name="value">Target object to evaluate</param>
        /// <returns>Null if object equals to <see cref="DBNull"/> or the object itself if different</returns>
		public static object IgnoreDBNull(this object value)
        {
            return value.IgnoreDBNull(null);
        }

        /// <summary>
        /// Checks if an object is a <see cref="DBNull"/> and if true returns the value passed as parameter
        /// </summary>
        /// <param name="value">Target object to evaluate</param>
        /// <param name="defaultValue">The desired return value when object is <see cref="DBNull"/></param>
        /// <returns>The value passed as a parameter if object equals to <see cref="DBNull"/> or the object itself if different</returns>
        public static object IgnoreDBNull(this object value, object defaultValue)
        {
            object result = value == DBNull.Value ? defaultValue : value;

            return result;
        }

        /// <summary>
        /// Checks an <see cref="object"/> for null value and returns <see cref="DBNull"/> if true.
        /// </summary>
        /// <param name="object">Target object to evaluate.</param>
        /// <returns><see cref="DBNull.Value"/> if object is null, current object value otherwise.</returns>
        public static object UseDBNull(this object obj)
        {
            object value = obj ?? DBNull.Value;

            return value;
        }

        public static dynamic ToExpandoObject(this object obj)
        {
            return ToDynamic(obj);
        }

        public static dynamic ToDynamic<T>(this T obj)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                expando.Add(propertyInfo.Name, propertyInfo.GetValue(obj));
            }

            return expando as ExpandoObject;
        }

        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            foreach (PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                ret.Add(prop.Name, prop.GetValue(obj, null));
            }

            return ret;
        }
    }
}
