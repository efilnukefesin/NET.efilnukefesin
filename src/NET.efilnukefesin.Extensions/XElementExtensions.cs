using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Extensions
{
    public static class XElementExtensions
    {
        #region FromXElement
        public static T FromXElement<T>(this XElement xElement)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }
        #endregion FromXElement

        #region FromXElement
        public static object FromXElement(this XElement xElement)
        {
            string typeName = xElement.Name.ToString();
            Type targetType = Type.GetType(typeName);
            XmlSerializer xmlSerializer = new XmlSerializer(targetType);
            return xmlSerializer.Deserialize(xElement.CreateReader());
        }
        #endregion FromXElement
    }
}
