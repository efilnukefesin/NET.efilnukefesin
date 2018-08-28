using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseTest;
using NET.efilnukefesin.Contracts.Grid;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Grid
{
    [TestClass]
    public class GridTests : BaseSimpleTest
    {
        #region GridProperties
        [TestClass]
        public class GridProperties : GridTests
        {

        }
        #endregion GridProperties

        #region GridConstruction
        [TestClass]
        public class GridConstruction : GridTests
        {
            #region ConstructWithObject
            [TestMethod]
            public void ConstructWithObject()
            {
                IGrid<object> item = new Grid<object>(10, 20);

                Assert.IsNotNull(item);
            }
            #endregion ConstructWithObject

            #region ConstructWithObject
            [TestMethod]
            public void ConstructWithObjectAndSize()
            {
                IGrid<object> item = new Grid<object>(new Size(10, 20));

                Assert.IsNotNull(item);
            }
            #endregion ConstructWithObject
        }
        #endregion GridConstruction

        #region GridMethods
        [TestClass]
        public class GridMethods : GridTests
        {

        }
        #endregion GridMethods
    }
}
