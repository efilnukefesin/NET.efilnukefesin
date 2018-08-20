using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Extensions
{
    public static class TypeExtensions
    {
        #region GetEventField
        public static FieldInfo GetEventField(this Type type, string eventName)
        {
            FieldInfo result = null;
            while (type != null)
            {
                /* Find events defined as field */
                result = type.GetField(eventName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
                if (result != null && (result.FieldType == typeof(MulticastDelegate) || result.FieldType.IsSubclassOf(typeof(MulticastDelegate))))
                {
                    break;
                }

                /* Find events defined as property { add; remove; } */
                result = type.GetField("EVENT_" + eventName.ToUpper(), BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
                if (result != null)
                {
                    break;
                }
                type = type.BaseType;
            }
            return result;
        }
        #endregion GetEventField

        #region ClearEventInvocations
        public static void ClearEventInvocations(this Type type, string eventName)
        {
            FieldInfo eventField = type.GetEventField(eventName);
            if (eventField == null)
            {
                return;
            }
            eventField.SetValue(null, null);
        }
        #endregion ClearEventInvocations
    }
}
