using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Extensions
{
    public static class StringExtensions
    {
        #region ToSha256
        public static string ToSha256(this string text)
        {
            var sha = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
        #endregion ToSha256

        #region ToBase64
        public static string ToBase64(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        #endregion ToBase64

        #region FromBase64
        public static string FromBase64(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        #endregion FromBase64
    }
}
