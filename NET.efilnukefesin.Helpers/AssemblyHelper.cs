using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    public static class AssemblyHelper
    {
        #region GetNamespace
        public static string GetNamespace(string ClassName)
        {
            var temp = ClassName.Split('.');
            string result = string.Empty;
            for (int i = 0; i < temp.Count() - 1; ++i)
            {
                result += temp[i];
                if (i < temp.Count() - 2)
                {
                    result += ".";
                }
            }
            return result;
        }
        #endregion GetNamespace

        #region GetType
        public static Type GetType(string TypeName)
        {
            Type result = null;

            result = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                      from type in asm.GetTypes()
                      where type.IsClass && type.Name == TypeName
                      select type).FirstOrDefault();

            if (result == null)
            {
                result = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                          from type in asm.GetTypes()
                          where type.IsClass && type.FullName == TypeName
                          select type).FirstOrDefault();
            }

            return result;
        }
        #endregion GetType
    }
}
