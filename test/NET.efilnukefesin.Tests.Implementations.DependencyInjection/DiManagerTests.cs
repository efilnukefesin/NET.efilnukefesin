using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.DependencyInjection;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DiManager
{
    [TestClass]
    public class DiManagerTests : BaseSimpleTest
    {
        #region DiManagerProperties
        [TestClass]
        public class DiManagerProperties : DiManagerTests
        {

        }
        #endregion DiManagerProperties

        #region DiManagerConstruction
        [TestClass]
        public class DiManagerConstruction : DiManagerTests
        {
            #region Construct
            [TestMethod]
            public void Construct()
            {
                IDependencyManager result = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance();

                Assert.IsNotNull(result);
            }
            #endregion Construct
        }
        #endregion DiManagerConstruction

        #region DiManagerMethods
        [TestClass]
        public class DiManagerMethods : DiManagerTests
        {
            #region Resolve
            [TestMethod]
            public void Resolve()
            {
                var result = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassC>();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(ClassC));
            }
            #endregion Resolve

            #region RegisterWithServiceParameter
            [TestMethod]
            public void RegisterWithServiceParameter()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ClassA, ClassA>(new TestService("abc"));
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ClassB, ClassB>(Contracts.DependencyInjection.Enums.Lifetime.Singleton, new TestService("xyz"));

                var classA = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassA>();
                var classB = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassB>();

                Assert.IsNotNull(classA);
                Assert.IsNotNull(classB);
                Assert.IsNotNull(classA.Service);
                Assert.IsNotNull(classB.Service);
                Assert.AreEqual("abc", classA.Service.SomeString);
                Assert.AreEqual("xyz", classB.Service.SomeString);

            }
            #endregion RegisterWithServiceParameter
        }
        #endregion DiManagerMethods
    }
}
