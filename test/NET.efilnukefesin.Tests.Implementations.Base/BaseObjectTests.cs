using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Base
{
    [TestClass]
    public class BaseObjectTests : BaseSimpleTest
    {
        #region UserServiceProperties
        [TestClass]
        public class BaseObjectProperties : BaseObjectTests
        {
            #region CreationIndex
            [TestMethod]
            public void CreationIndex()
            {
                throw new NotImplementedException();
            }
            #endregion CreationIndex
        }
        #endregion UserServiceProperties

        #region UserServiceConstruction
        [TestClass]
        public class BaseObjectConstruction : BaseObjectTests
        {

        }
        #endregion UserServiceConstruction

        #region UserServiceMethods
        [TestClass]
        public class BaseObjectMethods : BaseObjectTests
        {
            #region JsonSerialize
            [TestMethod]
            public void JsonSerialize()
            {
                ABitComplexClass aBitComplexClass = new ABitComplexClass(Guid.NewGuid(), "Hello World");
                BaseObject<ABitComplexClass> result = new BaseObject<ABitComplexClass>(aBitComplexClass);

                var resultString = JsonConvert.SerializeObject(result);
                var otherWayRound = JsonConvert.DeserializeObject<BaseObject<ABitComplexClass>>(resultString);

                Assert.IsNotNull(otherWayRound);
                Assert.IsInstanceOfType(otherWayRound, typeof(BaseObject<ABitComplexClass>));
            }
            #endregion JsonSerialize
        }
        #endregion UserServiceMethods
    }
}
