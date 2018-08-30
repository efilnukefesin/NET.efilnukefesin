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
            #region Indexer
            [TestMethod]
            public void Indexer()
            {
                IGrid<object> item = new Grid<object>(new Size(10, 20));
                item[5, 5] = "Something";

                Assert.AreEqual("Something", item[5, 5]);

                item[5, 5] = 5;

                Assert.AreEqual(5, item[5, 5]);
            }
            #endregion Indexer

            #region Fill
            [DataTestMethod]
            [DataRow(1000)]
            public void Fill(int numberOfIterations)
            {
                Size mapSize = new Size(10, 20);
                string demoText = "DemoText";
                Random random = new Random();

                IGrid<object> item = new Grid<object>(mapSize);
                item.Fill(demoText);

                for (int i = 0; i < numberOfIterations; i++)
                {
                    Assert.AreEqual(demoText, item[random.Next(mapSize.Width), random.Next(mapSize.Height)]);
                }
            }
            #endregion Fill

            #region Clear
            [DataTestMethod]
            [DataRow(1000)]
            public void Clear(int numberOfIterations)
            {
                Size mapSize = new Size(10, 20);
                string demoText = "DemoText";
                Random random = new Random();

                IGrid<object> item = new Grid<object>(mapSize);
                item.Fill(demoText);

                item.Clear();

                for (int i = 0; i < numberOfIterations; i++)
                {
                    Assert.AreEqual(null, item[random.Next(mapSize.Width), random.Next(mapSize.Height)]);
                }
            }
            #endregion Clear

            #region EqualsNegative
            [TestMethod]
            public void EqualsNegative()
            {
                Size mapSize = new Size(10, 20);
                string demoText = "DemoText";
                Random random = new Random();

                IGrid<object> item = new Grid<object>(mapSize);
                IGrid<object> item2 = new Grid<object>(mapSize);

                item.Fill(demoText);

                Assert.AreEqual(false, item.Equals(item2));
            }
            #endregion EqualsNegative

            #region EqualsPositive
            [TestMethod]
            public void EqualsPositive()
            {
                Size mapSize = new Size(10, 20);
                string demoText = "DemoText";
                Random random = new Random();

                IGrid<object> item = new Grid<object>(mapSize);
                IGrid<object> item2 = new Grid<object>(mapSize);

                item.Fill(demoText);
                item2.Fill(demoText);

                Assert.AreEqual(true, item.Equals(item2));
            }
            #endregion EqualsPositive

            #region Copy
            [TestMethod]
            public void Copy()
            {
                Size mapSize = new Size(10, 20);
                string demoText = "DemoText";
                Random random = new Random();

                IGrid<object> item = new Grid<object>(mapSize);
                IGrid<object> item2 = new Grid<object>(mapSize);

                item.Fill(demoText);
                item2.CopyFrom(item);

                Assert.AreEqual(true, item.Equals(item2));

                item.Clear();
                Assert.AreEqual(false, item.Equals(item2));
            }
            #endregion Copy
        }
        #endregion GridMethods
    }
}
