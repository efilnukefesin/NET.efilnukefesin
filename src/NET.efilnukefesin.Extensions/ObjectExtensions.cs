using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Extensions
{
    public static class ObjectExtensions
    {
        #region ToXElement
        public static XElement ToXElement<T>(this object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, obj);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
        }
        #endregion ToXElement

        #region ToXElement
        public static XElement ToXElement(this object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(streamWriter, obj);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
        }
        #endregion ToXElement

        #region ClearEventInvocations
        public static void ClearEventInvocations(this object obj, string eventName)
        {
            FieldInfo eventField = obj.GetType().GetEventField(eventName);
            if (eventField == null)
            {
                return;
            }
            eventField.SetValue(obj, null);
        }
        #endregion ClearEventInvocations
    }
}
