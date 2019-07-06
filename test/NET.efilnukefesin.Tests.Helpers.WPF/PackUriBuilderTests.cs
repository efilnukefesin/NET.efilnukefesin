using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Helpers.WPF;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Helpers.WPF
{
    [TestClass]
    public class PackUriBuilderTests : BaseSimpleTest
    {
        #region PackUriBuilderProperties
        [TestClass]
        public class PackUriBuilderProperties : PackUriBuilderTests
        {

        }
        #endregion PackUriBuilderProperties

        #region PackUriBuilderConstruction
        [TestClass]
        public class PackUriBuilderConstruction : PackUriBuilderTests
        {

        }
        #endregion PackUriBuilderConstruction

        #region PackUriBuilderMethods
        [TestClass]
        public class PackUriBuilderMethods : PackUriBuilderTests
        {
            #region LocalAssemblyResourceFile
            [TestMethod]
            public void LocalAssemblyResourceFile()
            {
                string result = PackUriBuilder.LocalAssemblyResourceFile("/test.bmp").ToString();

                Assert.AreEqual("pack://application:,,,/test.bmp", result);
            }
            #endregion LocalAssemblyResourceFile

            #region ReferencedAssemblyResourceFile
            [TestMethod]
            public void ReferencedAssemblyResourceFile()
            {
                string result = PackUriBuilder.ReferencedAssemblyResourceFile("/test.bmp", "MyAssembly").Version("v2.3.0.1").ToString();

                Assert.AreEqual("pack://application:,,,/MyAssembly;v2.3.0.1;component/test.bmp", result);
            }
            #endregion ReferencedAssemblyResourceFile

            #region SiteOfOrigin
            [TestMethod]
            public void SiteOfOrigin()
            {
                string result = PackUriBuilder.SiteOfOrigin("/test.bmp").Relative().ToString();

                Assert.AreEqual("/test.bmp", result);
            }
            #endregion SiteOfOrigin
        }
        #endregion PackUriBuilderMethods
    }

}
