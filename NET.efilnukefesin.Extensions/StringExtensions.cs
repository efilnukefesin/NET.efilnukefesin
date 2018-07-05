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
    }
}
