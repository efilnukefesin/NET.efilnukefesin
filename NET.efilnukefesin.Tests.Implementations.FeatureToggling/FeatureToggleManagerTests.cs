using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.FeatureToggling;
using NET.efilnukefesin.Implementations.FeatureToggling;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NET.efilnukefesin.Tests.Implementations.FeatureToggling
{
    [TestClass]
    public class FeatureToggleManagerTests : BaseSimpleTest
    {
        #region FeatureToggleManagerProperties
        [TestClass]
        public class FeatureToggleManagerProperties : FeatureToggleManagerTests
        {

        }
        #endregion FeatureToggleManagerProperties

        #region FeatureToggleManagerConstruction
        [TestClass]
        public class FeatureToggleManagerConstruction : FeatureToggleManagerTests
        {

        }
        #endregion FeatureToggleManagerConstruction

        #region FeatureToggleManagerMethods
        [TestClass]
        public class FeatureToggleManagerMethods : FeatureToggleManagerTests
        {
            #region Exists
            [TestMethod]
            public void Exists()
            {
                DiSetup.Tests();

                IFeatureToggleManager featureToggleManager = DiHelper.GetService<IFeatureToggleManager>();
                featureToggleManager.Clear();
                featureToggleManager.Add(new StaticFeatureToggle("TestFeature", true));

                Assert.AreEqual(true, featureToggleManager.Exists("TestFeature"));
                Assert.AreEqual(false, featureToggleManager.Exists("TestFeature2"));
            }
            #endregion Exists

            #region AddStaticFeatureToggle
            [TestMethod]
            public void AddStaticFeatureToggle()
            {
                DiSetup.Tests();

                IFeatureToggleManager featureToggleManager = DiHelper.GetService<IFeatureToggleManager>();
                featureToggleManager.Clear();
                featureToggleManager.Add(new StaticFeatureToggle("TestFeature", true));
                featureToggleManager.Add(new StaticFeatureToggle("TestFeature2", false));

                Assert.AreEqual(true, featureToggleManager.GetValue("TestFeature"));
                Assert.AreEqual(false, featureToggleManager.GetValue("TestFeature2"));
            }
            #endregion AddStaticFeatureToggle

            #region AddTimebasedFeatureToggle
            [TestMethod]
            public void AddTimebasedFeatureToggle()
            {
                DiSetup.Tests();

                IFeatureToggleManager featureToggleManager = DiHelper.GetService<IFeatureToggleManager>();
                featureToggleManager.Clear();
                featureToggleManager.Add(new TimebasedFeatureToggle("TestFeature", DateTimeOffset.Now + TimeSpan.FromMilliseconds(10)));
                featureToggleManager.Add(new TimebasedFeatureToggle("TestFeature2", DateTimeOffset.Now + TimeSpan.FromDays(1)));

                Thread.Sleep(20);

                Assert.AreEqual(true, featureToggleManager.GetValue("TestFeature"));
                Assert.AreEqual(false, featureToggleManager.GetValue("TestFeature2"));
            }
            #endregion AddTimebasedFeatureToggle

            #region AddVersionbasedFeatureToggle
            [TestMethod]
            public void AddVersionbasedFeatureToggle()
            {
                DiSetup.Tests();

                IFeatureToggleManager featureToggleManager = DiHelper.GetService<IFeatureToggleManager>();
                featureToggleManager.Clear();
                featureToggleManager.Add(new VersionbasedFeatureToggle("TestFeature", new Version(0, 8), this.GetType()));
                featureToggleManager.Add(new VersionbasedFeatureToggle("TestFeature2", new Version(1, 0), this.GetType()));

                Assert.AreEqual(true, featureToggleManager.GetValue("TestFeature"));
                Assert.AreEqual(false, featureToggleManager.GetValue("TestFeature2"));
            }
            #endregion AddVersionbasedFeatureToggle
        }
        #endregion FeatureToggleManagerMethods
    }

}
