using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Extensions
{
    public static class XmlPersistanceExtensions
    {
        #region LoadFromXml
        public static T FromXml<T>(this XElement element)
        {
            DataContractSerializer dcSerializer = new DataContractSerializer(typeof(T));
            MemoryStream msXml = new MemoryStream();
            element.Save(msXml);
            msXml.Position = 0;
            T result = (T)dcSerializer.ReadObject(msXml);
            msXml.Close();
            return result;
        }
        #endregion LoadFromXml

        #region SaveToXml
        public static XElement ToXml(this object Input)
        {
            DataContractSerializer dcSerializer = new DataContractSerializer(Input.GetType());
            MemoryStream msXml = new MemoryStream();
            dcSerializer.WriteObject(msXml, Input);
            msXml.Position = 0;
            XElement result = XElement.Load(msXml);
            msXml.Close();
            return result;
        }
        #endregion SaveToXml
    }
}
