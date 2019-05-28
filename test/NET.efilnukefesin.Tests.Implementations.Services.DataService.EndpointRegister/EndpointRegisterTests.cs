using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.EndpointRegister
{
    [TestClass]
    public class EndpointRegisterTests : BaseSimpleTest
    {
        #region EndpointRegisterProperties
        [TestClass]
        public class EndpointRegisterProperties : EndpointRegisterTests
        {

        }
        #endregion EndpointRegisterProperties

        #region EndpointRegisterConstruction
        [TestClass]
        public class EndpointRegisterConstruction : EndpointRegisterTests
        {

        }
        #endregion EndpointRegisterConstruction

        #region EndpointRegisterMethods
        [TestClass]
        public class EndpointRegisterMethods : EndpointRegisterTests
        {
            #region GetEndpoint
            [TestMethod]
            public void GetEndpoint()
            {
                DiSetup.Tests();
                IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
                bool wasSuccessfullyAdded = endpointRegister.AddEndpoint("SomeEndpointTest", "TestString");

                string result = endpointRegister.GetEndpoint("SomeEndpointTest");

                Assert.AreEqual(true, wasSuccessfullyAdded);
                Assert.IsNotNull(result);
                Assert.AreEqual("TestString", result);
            }
            #endregion GetEndpoint
        }
        #endregion EndpointRegisterMethods
    }
}
