using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Extensions
{
    public static class ByteExtensions
    {
        #region ToBase64
        public static string ToBase64(this byte[] plainBytes)
        {
            return Convert.ToBase64String(plainBytes);
        }
        #endregion ToBase64

        #region FromBase64
        public static byte[] FromBase64(this string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData);
        }
        #endregion FromBase64
    }
}
