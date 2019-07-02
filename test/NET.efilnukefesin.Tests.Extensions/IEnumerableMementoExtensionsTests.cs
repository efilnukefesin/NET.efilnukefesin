using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Tests.Extensions.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Extensions;

namespace NET.efilnukefesin.Tests.Extensions
{
    [TestClass]
    public class IEnumerableMementoExtensionsTests : BaseSimpleTest
    {
        #region IEnumerableMementoExtensionsProperties
        [TestClass]
        public class IEnumerableMementoExtensionsProperties : IEnumerableMementoExtensionsTests
        {

        }
        #endregion IEnumerableMementoExtensionsProperties

        #region IEnumerableMementoExtensionsConstruction
        [TestClass]
        public class IEnumerableMementoExtensionsConstruction : IEnumerableMementoExtensionsTests
        {

        }
        #endregion IEnumerableMementoExtensionsConstruction

        #region IEnumerableMementoExtensionsMethods
        [TestClass]
        public class IEnumerableMementoExtensionsMethods : IEnumerableMementoExtensionsTests
        {
            #region SaveAndRestore
            [TestMethod]
            public void SaveAndRestore()
            {
                List<SomeClass> list = new List<SomeClass>() { new SomeClass(), new SomeClass(), new SomeClass(), new SomeClass(), new SomeClass() };

                list.Save(nameof(list));
                int numberbefore = list.Count;
                list.Add(new SomeClass());
                int numberAfterAddition = list.Count;
                list.Restore(nameof(list));
                int numerAfterRestoration = list.Count;

                Assert.AreEqual(numberbefore, numerAfterRestoration);
                Assert.AreEqual(numberAfterAddition, numberbefore + 1);
            }
            #endregion SaveAndRestore
        }
        #endregion IEnumerableMementoExtensionsMethods
    }
}
