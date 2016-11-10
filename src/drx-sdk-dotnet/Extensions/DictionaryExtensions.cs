using System.Collections.Generic;

namespace Net.Dreceiptx.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the given key if it exists otherwise returns null
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key">The key value to lookup</param>
        /// <returns>The value if it exists otherwise null. Note: it is possible that the value exists and it is null</returns>
        public static TValue GetOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : class
        {
            TValue value;
            dict.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the ky if it exsits otherwise returns the defaultValue
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">The default value to be return if the given Key can not be found in the dictionary</param>
        /// <returns>The value associated with the key if it can be found otherwise the defaultValue is returned</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue) where TValue : class
        {
            TValue value ;
            if(!dict.TryGetValue(key, out value))
            {
                value = defaultValue;
            }
            return value;
        }
    }
}
