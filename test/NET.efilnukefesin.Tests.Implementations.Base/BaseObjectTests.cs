﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Tests.Implementations.Base.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Base
{
    [TestClass]
    public class BaseObjectTests : BaseSimpleTest
    {
        #region BaseObjectProperties
        [TestClass]
        public class BaseObjectProperties : BaseObjectTests
        {
            #region CreationIndex
            [TestMethod]
            public void CreationIndex()
            {
                IBaseObject object1 = new StraightFromBaseObject();
                IBaseObject object2 = new StraightFromBaseObject();

                Assert.IsNotNull(object1);
                Assert.IsNotNull(object2);
                Assert.AreEqual(object1.CreationIndex + 1, object2.CreationIndex);
            }
            #endregion CreationIndex
        }
        #endregion BaseObjectProperties

        #region BaseObjectConstruction
        [TestClass]
        public class BaseObjectConstruction : BaseObjectTests
        {

        }
        #endregion BaseObjectConstruction

        #region BaseObjectMethods
        [TestClass]
        public class BaseObjectMethods : BaseObjectTests
        {
            #region SaveAndRestore
            [TestMethod]
            public void SaveAndRestore()
            {
                SomeBaseObjectClass x = new SomeBaseObjectClass();

                x.Save();
                x.TestString = "123";
                x.Restore();

                Assert.AreEqual("Hello World", x.TestString);
            }
            #endregion SaveAndRestore

            #region DiffersFromMemory
            [TestMethod]
            public void DiffersFromMemory()
            {
                SomeBaseObjectClass x = new SomeBaseObjectClass();

                x.Save();
                x.TestString = "123";
                bool isDifferent = x.DiffersFromMemory();
                x.Restore();
                bool isEqual= x.DiffersFromMemory();

                Assert.AreEqual(true, isDifferent);
                Assert.AreEqual(false, isEqual);
            }
            #endregion DiffersFromMemory
        }
        #endregion BaseObjectMethods
    }
}
