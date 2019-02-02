using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Extensions;
using System.Xml.Linq;
using System.IO;

namespace NET.efilnukefesin.Tests.Extensions
{
    [TestClass]
    public class XmlPersistanceExtensionsTests : BaseSimpleTest
    {
        #region XmlPersistanceExtensionsMethods
        [TestClass]
        public class XmlPersistanceExtensionsMethods : XmlPersistanceExtensionsTests
        {
            #region LoadFromXml
            [TestMethod]
            public void LoadFromXml()
            {
                int i = 0;
                i = XElement.Parse("<int>16</int>").FromXml<int>();
            }
            #endregion LoadFromXml

            #region SaveToXml
            [TestMethod]
            public void SaveToXml()
            {
                int i = 16;

                var x = i.ToXml();
            }
            #endregion SaveToXml
        }
        #endregion XmlPersistanceExtensionsMethods
    }
}
