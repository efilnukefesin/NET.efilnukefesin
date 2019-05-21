using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Base
{
    [TestClass]
    public class SimpleResultTests : BaseSimpleTest
    {
        #region UserServiceProperties
        [TestClass]
        public class SimpleResultProperties : SimpleResultTests
        {

        }
        #endregion UserServiceProperties

        #region UserServiceConstruction
        [TestClass]
        public class SimpleResultConstruction : SimpleResultTests
        {

        }
        #endregion UserServiceConstruction

        #region UserServiceMethods
        [TestClass]
        public class SimpleResultMethods : SimpleResultTests
        {
            #region JsonSerialize
            [TestMethod]
            public void JsonSerialize()
            {
                string testString = "Hello World";

                SimpleResult<string> result = new SimpleResult<string>(testString);

                string output = JsonConvert.SerializeObject(result);

                //TODO: add asserts and stuff

            }
            #endregion JsonSerialize
        }
        #endregion UserServiceMethods
    }
}
