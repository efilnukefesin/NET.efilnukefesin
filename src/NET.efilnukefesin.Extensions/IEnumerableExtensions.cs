using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Extensions
{
    public static class IEnumerableExtensions
    {
        #region Methods

        #region Add: adds an item to the end of the enumerable
        /// <summary>
        /// adds an item to the end of the enumerable
        /// </summary>
        /// <typeparam name="T">the type enumerated</typeparam>
        /// <param name="enumerable">the existing ienumerable variable</param>
        /// <param name="value">the value to add</param>
        /// <returns>a new ienumerable with the value at the end.</returns>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T value)
        {
            foreach (var item in enumerable)
            {
                yield return item;
            }
            yield return value;
        }
        #endregion Add

        #region Insert
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> enumerable, int index, T value)
        {
            int current = 0;
            foreach (var item in enumerable)
            {
                if (current == index)
                {
                    yield return value;
                }
                yield return item;
                current++;
            }
        }
        #endregion Insert

        #region Replace
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, int index, T value)
        {
            int current = 0;
            foreach (var item in enumerable)
            {
                yield return current == index ? value : item;
                current++;
            }
        }
        #endregion Replace

        #region Remove
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> enumerable, int index)
        {
            int current = 0;
            foreach (var item in enumerable)
            {
                if (current != index)
                {
                    yield return item;
                }
                current++;
            }
        }
        #endregion Remove

        #endregion Methods
    }
}
