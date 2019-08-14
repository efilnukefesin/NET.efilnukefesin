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

            #region Play
            [TestMethod]
            public void Play()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                timeService.Play();

                Assert.AreEqual(1, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
            }
            #endregion Play

            #region FastForward
            [TestMethod]
            public void FastForward()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                throw new NotImplementedException();
            }
            #endregion FastForward

            #region FastForwardWithTarget
            [TestMethod]
            public void FastForwardWithTarget()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                throw new NotImplementedException();
            }
            #endregion FastForwardWithTarget

            #region Rewind
            [TestMethod]
            public void Rewind()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                throw new NotImplementedException();
            }
            #endregion Rewind

            #region RewindWithTarget
            [TestMethod]
            public void RewindWithTarget()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                throw new NotImplementedException();
            }
            #endregion RewindWithTarget

            #region Pause
            [TestMethod]
            public void Pause()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                var testTime = timeService.ElapsedTimeRelative;

                timeService.Pause();
                Thread.Sleep(100);

                Assert.AreEqual(0, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.AreEqual(testTime, timeService.ElapsedTimeRelative);
            }
            #endregion Pause

            #region Pause
            [TestMethod]
            public void PauseNegative()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                var testTime = timeService.ElapsedTimeRelative;

                //timeService.Pause();  //<- do NOT pause here
                Thread.Sleep(100);

                //Assert.AreEqual(0, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.AreNotEqual(testTime, timeService.ElapsedTimeRelative);
            }
            #endregion Pause

            #region JumpTo
            [TestMethod]
            public void JumpTo()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                throw new NotImplementedException();
            }
            #endregion JumpTo

        }
        #endregion StandardTimeServiceMethods
    }
}
