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
        #region timeEquals: compares two time spans on almost equality
        /// <summary>
        /// compares two time spans on almost equality
        /// </summary>
        /// <param name="timeSpan1">the first time span</param>
        /// <param name="timeSpan2">the second time span</param>
        /// <param name="numbersBehindComma">the precision</param>
        /// <returns>if the numbers in terms of precision match</returns>
        private static bool timeEquals(TimeSpan timeSpan1, TimeSpan timeSpan2, int numbersBehindComma = 5)
        {
            bool result = false;

            double diff = Math.Abs((timeSpan1 - timeSpan2).TotalSeconds);
            double roundedDiff = Math.Round(diff, numbersBehindComma);

            if (roundedDiff.Equals(0))
            {
                result = true;
            }

            return result;
        }
        #endregion timeEquals

        #region timeEquals
        private static bool timeEquals(TimeSpan timeSpan1, TimeSpan timeSpan2, TimeSpan abbreviation)
        {
            bool result = false;

            if (((timeSpan1 - abbreviation) < timeSpan2) && ((timeSpan1 + abbreviation) > timeSpan2))
            {
                result = true;
            }

            return result;
        }
        #endregion timeEquals

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

                timeService.Align();
                timeService.Play();

                //TODO: address issue that this does not work in 'bunch run mode'

                Assert.AreEqual(1, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.IsTrue(StandardTimeServiceTests.timeEquals(timeService.ElapsedTimeAbsolute, timeService.ElapsedTimeRelative, new TimeSpan(0, 0, 1)));
            }
            #endregion Play

            #region FastForward
            [TestMethod]
            public void FastForward()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                timeService.Play();

                timeService.FastForward(2);
                Thread.Sleep(100);

                Assert.AreEqual(2, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.IsTrue(timeService.ElapsedTimeAbsolute < timeService.ElapsedTimeRelative);
            }
            #endregion FastForward

            #region FastForwardWithTarget
            [TestMethod]
            public void FastForwardWithTarget()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                timeService.Play();

                timeService.FastForward(2, new TimeSpan(1, 0, 0));
                Thread.Sleep(100);

                Assert.AreEqual(2, timeService.CurrentMultiplicator);
                Assert.AreEqual(new TimeSpan(1, 0, 0), timeService.CurrentTarget);
                Assert.IsTrue(timeService.ElapsedTimeAbsolute < timeService.ElapsedTimeRelative);
            }
            #endregion FastForwardWithTarget

            #region Rewind
            [TestMethod]
            public void Rewind()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                timeService.Play();

                timeService.Rewind(2);
                Thread.Sleep(100);

                Assert.AreEqual(-2, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.IsTrue(timeService.ElapsedTimeAbsolute > timeService.ElapsedTimeRelative);
            }
            #endregion Rewind

            #region RewindWithTarget
            [TestMethod]
            public void RewindWithTarget()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                timeService.Play();

                timeService.Rewind(2, new TimeSpan(0, 0, 0)); ;
                Thread.Sleep(100);

                Assert.AreEqual(-2, timeService.CurrentMultiplicator);
                Assert.AreEqual(new TimeSpan(0, 0, 0), timeService.CurrentTarget);
                Assert.IsTrue(timeService.ElapsedTimeAbsolute > timeService.ElapsedTimeRelative);
            }
            #endregion RewindWithTarget

            #region Pause
            [TestMethod]
            public void Pause()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                timeService.Play();
                var testTime = timeService.ElapsedTimeRelative;

                timeService.Pause();
                Thread.Sleep(100);

                Assert.AreEqual(0, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.AreEqual(testTime, timeService.ElapsedTimeRelative);
            }
            #endregion Pause

            #region PauseNegative
            [TestMethod]
            public void PauseNegative()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();
                timeService.Align();
                timeService.Play();
                var testTime = timeService.ElapsedTimeRelative;

                //timeService.Pause();  //<- do NOT pause here
                Thread.Sleep(100);

                Assert.AreNotEqual(0, timeService.CurrentMultiplicator);
                Assert.IsNull(timeService.CurrentTarget);
                Assert.IsTrue(testTime < timeService.ElapsedTimeRelative);  //TODO: faisl in batch mode
            }
            #endregion PauseNegative

            #region JumpTo
            [TestMethod]
            public void JumpTo()
            {
                DiSetup.Tests();

                ITimeService timeService = DiHelper.GetService<ITimeService>();

                TimeSpan target = new TimeSpan(3, 2, 1, 59, 999);

                timeService.JumpTo(target);

                Assert.IsTrue(StandardTimeServiceTests.timeEquals(target, timeService.ElapsedTimeRelative, new TimeSpan(0, 0, 1)));
            }
            #endregion JumpTo
        }
        #endregion StandardTimeServiceMethods
    }
}
