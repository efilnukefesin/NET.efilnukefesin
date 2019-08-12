using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Timing;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Timing
{
    [TestClass]
    public class StandardTimeServiceTests : BaseSimpleTest
    {
        #region StandardTimeServiceProperties
        [TestClass]
        public class StandardTimeServiceProperties : StandardTimeServiceTests
        {

        }
        #endregion StandardTimeServiceProperties

        #region StandardTimeServiceConstruction
        [TestClass]
        public class StandardTimeServiceConstruction : StandardTimeServiceTests
        {
            #region IsNotNull
            [TestMethod]
            public void IsNotNull()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                Assert.IsNotNull(timeService);
                Assert.IsInstanceOfType(timeService, typeof(NET.efilnukefesin.Implementations.Timing.StandardTimeService));
            }
            #endregion IsNotNull
        }
        #endregion StandardTimeServiceConstruction

        #region StandardTimeServiceMethods
        [TestClass]
        public class StandardTimeServiceMethods : StandardTimeServiceTests
        {
            #region PublishBlocker
            [TestMethod]
            public void PublishBlocker()
            {
                throw new NotImplementedException();
            }
            #endregion PublishBlocker
        }
        #endregion StandardTimeServiceMethods
    }
}
