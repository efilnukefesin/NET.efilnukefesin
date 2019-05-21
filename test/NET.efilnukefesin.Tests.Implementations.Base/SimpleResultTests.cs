using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
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
                DiSetup.Tests();
                IUserService userService = DiHelper.GetService<IUserService>();
                userService.CreateTestData();

                User user = userService.GetUserBySubject("88421113");

                SimpleResult<object> result = new SimpleResult<object>(user);

                string output = JsonConvert.SerializeObject(result);

            }
            #endregion JsonSerialize
        }
        #endregion UserServiceMethods
    }
}
