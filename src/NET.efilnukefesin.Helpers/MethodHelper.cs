using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    public static class MethodHelper
    {
        #region HasAttribute
        public static bool HasAttribute<AttributeType>(Type typeToLookat, string MethodName) where AttributeType : Attribute
        {
            bool result = false;

            MethodBase method = typeToLookat.GetMethod(MethodName);

            var customAttribute = method.GetCustomAttributes(typeof(AttributeType), true).FirstOrDefault() as AttributeType;
            if (customAttribute != null)
            {
                result = true;
            }
            return result;
        }
        #endregion HasAttribute
    }
}
