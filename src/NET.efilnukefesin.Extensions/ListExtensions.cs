using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Extensions
{
    public static class ListExtensions
    {
        #region Methods

        #region ToIEnumerable
        public static IEnumerable<TSource> ToIEnumerable<TSource>(this List<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }

            foreach (TSource value in source)
            {
                yield return value;
            }
        }
        #endregion ToIEnumerable

        #endregion Methods
    }
}
