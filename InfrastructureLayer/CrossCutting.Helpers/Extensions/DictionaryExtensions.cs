using System.Collections.Generic;

namespace CrossCutting.Helpers.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds new a value to the dictionary or updates if the key already exists
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Target dictionary</param>
        /// <param name="key">Key to add or lookup</param>
        /// <param name="value">Value to add or update</param>
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Gets the value associated with the specified key or the value type default value
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Target dictionary</param>
        /// <param name="key">Key to lookup</param>
        /// <returns>The existing value for the specified key or the value type default value</returns>
        public static TValue TryGetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : default;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the specified default value
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Target dictionary</param>
        /// <param name="key">Key to lookup</param>
        /// <param name="defaultValue">The value to return in case the key isn't present at the dictionary</param>
        /// <returns>The existing value for the specified key or the specified default value</returns>
        public static TValue TryGetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        public static T ToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            T someObject = new T();
            System.Type someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, object> item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        /// <summary>
        /// Convert a IDictionary to Array
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T2[] ToValueArray<T1, T2>(this Dictionary<T1, T2> value)
        {
            T2[] values = new T2[value.Values.Count];
            value.Values.CopyTo(values, 0);

            return values;
        }

    }
}
