// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    ///     The <see cref="Dictionary{TKey,TValue}"/> extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Adds a new <see cref="KeyValuePair{TKey,TValue}"/> to the <paramref name="dictionary"/>, or updates an
        ///     existing <see cref="KeyValuePair{TKey,TValue}"/> with the <paramref name="value"/> where the keys
        ///     match.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of the value.
        /// </typeparam>
        /// <param name="dictionary">
        ///     The <see cref="Dictionary{TKey,TValue}"/>.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
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
    }
}
