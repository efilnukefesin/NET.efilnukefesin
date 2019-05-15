using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Tests.Helpers.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Helpers
{
    [TestClass]
    public class TypeHelperTests : BaseSimpleTest
    {
        #region TypeHelperMethods
        [TestClass]
        public class TypeHelperMethods : TypeHelperTests
        {
            #region CreateInstance
            [TestMethod]
            public void CreateInstance()
            {
                var result = TypeHelper.CreateInstance<object>();

                Assert.IsNotNull(result);
            }
            #endregion CreateInstance

            #region CreateInstanceWithParameters
            [TestMethod]
            public void CreateInstanceWithParameters()
            {
                var result = TypeHelper.CreateInstance<TestClass>("MySomeString");

                Assert.IsNotNull(result);
                Assert.AreEqual("MySomeString", result.SomeString);
            }
            #endregion CreateInstanceWithParameters
        }
        #endregion TypeHelperMethods
    }
}
