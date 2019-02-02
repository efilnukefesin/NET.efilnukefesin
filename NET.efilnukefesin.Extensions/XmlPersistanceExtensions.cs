using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Extensions
{
    public static class XmlPersistanceExtensions
    {
        #region LoadFromXml
        public static T LoadFromXml<T>(this XElement element)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream msXml = new MemoryStream();
            element.Save(msXml);
            msXml.Position = 0;
            T result = (T)xmlSerializer.Deserialize(msXml);
            msXml.Close();
            return result;
        }
        #endregion LoadFromXml

        #region SaveToXml
        public static XElement SaveToXml(this object Input)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Input.GetType());
            MemoryStream msXml = new MemoryStream();
            xmlSerializer.Serialize(msXml, Input);
            msXml.Position = 0;
            XElement result = XElement.Load(msXml);
            msXml.Close();
            return result;
        }
        #endregion SaveToXml
    }
}
