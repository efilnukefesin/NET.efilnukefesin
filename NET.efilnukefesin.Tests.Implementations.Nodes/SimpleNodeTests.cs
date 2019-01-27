using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Nodes
{
    [TestClass]
    public class SimpleNodeTests : BaseSimpleTest
    {
        #region SimpleNodeProperties
        [TestClass]
        public class SimpleNodeProperties : SimpleNodeTests
        {

        }
        #endregion SimpleNodeProperties

        #region SimpleNodeConstruction
        [TestClass]
        public class SimpleNodeConstruction : SimpleNodeTests
        {

        }
        #endregion SimpleNodeConstruction

        #region SimpleNodeMethods
        [TestClass]
        public class SimpleNodeMethods : SimpleNodeTests
        {
            #region OnEnter
            [TestMethod]
            public void OnEnter()
            {
                INode node = new SimpleNode();
                int counter = 0;

                node.OnEnter += (o, e) => { counter++; };

                node.Enter();
                node.Enter();

                Assert.AreEqual(2, counter);
            }
            #endregion OnEnter

            #region OnExit
            [TestMethod]
            public void OnExit()
            {
                INode node = new SimpleNode();
                int counter = 0;

                node.OnExit += (o, e) => { counter++; };

                node.Exit();
                node.Exit();

                Assert.AreEqual(2, counter);
            }
            #endregion OnExit
        }
        #endregion SimpleNodeMethods
    }
}
