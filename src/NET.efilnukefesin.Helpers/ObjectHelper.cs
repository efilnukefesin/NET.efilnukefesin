using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    public static class ObjectHelper
    {
        #region ConvertArrayToString
        public static string ConvertArrayToString(object[] Parameters)
        {
            string result = string.Empty;
            int paramNumber = Parameters.Count();
            if (paramNumber > 0)
            {
                for (int i = 0; i < Parameters.Count(); ++i)
                {
                    result += Parameters[i];
                    if (i < paramNumber - 1)
                    {
                        result += ", ";
                    }
                }
            }
            return result;
        }
        #endregion ConvertArrayToString
    }
}
