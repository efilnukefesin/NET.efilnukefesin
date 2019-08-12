using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Timing;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NET.efilnukefesin.Tests.Implementations.Timing
{
    [TestClass]
    public class StandardTimeServiceTests : BaseSimpleTest
    {
        #region StandardTimeServiceProperties
        [TestClass]
        public class StandardTimeServiceProperties : StandardTimeServiceTests
        {
            #region ElapsedTime
            [DataTestMethod]
            [DataRow(100, 25, true)]
            [DataRow(100, 1, false)]
            [DataRow(200, 25, true)]
            [DataRow(500, 25, true)]
            public void ElapsedTime(int IntervalInMilliseconds, int Variance, bool IsExpectedToBeSuccessful)
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                var startTime = timeService.ElapsedTimeAbsolute.TotalMilliseconds;

                Thread.Sleep(IntervalInMilliseconds);

                var endTime = timeService.ElapsedTimeAbsolute.TotalMilliseconds;

                var difference = endTime - startTime;

                if (IsExpectedToBeSuccessful)
                {
                    Assert.IsTrue(difference > IntervalInMilliseconds - Variance);
                    Assert.IsTrue(difference < IntervalInMilliseconds + Variance);
                }
                else
                {
                    Assert.IsTrue((difference > IntervalInMilliseconds - Variance) || (difference < IntervalInMilliseconds + Variance));
                }
            }
            #endregion ElapsedTime
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
            //#region PublishBlocker
            //[TestMethod]
            //public void PublishBlocker()
            //{
            //    throw new NotImplementedException();
            //}
            //#endregion PublishBlocker
        }
        #endregion StandardTimeServiceMethods
    }
}
