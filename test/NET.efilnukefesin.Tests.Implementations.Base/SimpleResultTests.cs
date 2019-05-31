using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.Base.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Base
{
    [TestClass]
    public class SimpleResultTests : BaseSimpleTest
    {
        #region SimpleResultProperties
        [TestClass]
        public class SimpleResultProperties : SimpleResultTests
        {

        }
        #endregion SimpleResultProperties

        #region SimpleResultConstruction
        [TestClass]
        public class SimpleResultConstruction : SimpleResultTests
        {

        }
        #endregion SimpleResultConstruction

        #region SimpleResultMethods
        [TestClass]
        public class SimpleResultMethods : SimpleResultTests
        {
            #region JsonSerialize
            [TestMethod]
            public void JsonSerialize()
            {
                ABitComplexClass aBitComplexClass = new ABitComplexClass(Guid.NewGuid(), "Hello World"); 
                SimpleResult<ABitComplexClass> result = new SimpleResult<ABitComplexClass>(aBitComplexClass);

                var resultString = JsonConvert.SerializeObject(result);
                var otherWayRound = JsonConvert.DeserializeObject<SimpleResult<ABitComplexClass>>(resultString);

                Assert.IsNotNull(otherWayRound);
                Assert.IsInstanceOfType(otherWayRound, typeof(SimpleResult<ABitComplexClass>));
            }
            #endregion JsonSerialize
        }
        #endregion SimpleResultMethods
    }
}
