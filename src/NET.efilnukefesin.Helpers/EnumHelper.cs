using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    public static class EnumHelper
    {
        #region Parse: parses an enum
        /// <summary>
        /// parses an enum
        /// </summary>
        /// <typeparam name="T">the resulting enum type</typeparam>
        /// <param name="value">the string to be parsed</param>
        /// <returns>a strongly-typed result</returns>
        public static T Parse<T>(string value)
        {
            T result;
            result = (T)Enum.Parse(typeof(T), value, true);
            return result;
        }
        #endregion Parse

        #region Parse: parses an enum with a fallback value
        /// <summary>
        /// parses an enum with a fallback value
        /// </summary>
        /// <typeparam name="T">the resulting enum type</typeparam>
        /// <param name="value">the string to be parsed</param>
        /// <param name="defaultValue">the fallback value</param>
        /// <returns>a strongly-typed result</returns>
        public static T Parse<T>(string value, T defaultValue)
        {
            T result;
            if (string.IsNullOrEmpty(value))
            {
                result = defaultValue;
            }
            result = (T)Enum.Parse(typeof(T), value, true);
            return result;
        }
        #endregion Parse
    }
}
