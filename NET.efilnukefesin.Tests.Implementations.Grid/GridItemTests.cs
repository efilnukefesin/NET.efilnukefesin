using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseTest;
using NET.efilnukefesin.Contracts.Grid;
using NET.efilnukefesin.Implementations.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Grid
{
    [TestClass]
    public class GridItemTests : BaseSimpleTest
    {
        #region GridItemProperties
        [TestClass]
        public class GridItemProperties : GridItemTests
        {

        }
        #endregion GridItemProperties

        #region GridItemConstruction
        [TestClass]
        public class GridItemConstruction : GridItemTests
        {
            #region ConstructWithObject
            [TestMethod]
            public void ConstructWithObject()
            {
                IGridItem<object> item = new GridItem<object>(1, 2, new object());

                Assert.IsNotNull(item);
            }
            #endregion ConstructWithObject
        }
        #endregion GridItemConstruction

        #region GridItemMethods
        [TestClass]
        public class GridItemMethods : GridItemTests
        {

        }
        #endregion GridItemMethods
    }
}
