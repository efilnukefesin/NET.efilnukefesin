using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Nodes
{
    [TestClass]
    public class SimpleNodeManagerTests : BaseSimpleTest
    {
        #region RegisterDependencies
        protected void RegisterDependencies()
        {
            DiManager.GetInstance().RegisterInstance<INodeManager<object, INode<object>>>(new SimpleNodeManager<object, INode<object>>());
        }
        #endregion RegisterDependencies

        #region SimpleNodeManagerProperties
        [TestClass]
        public class SimpleNodeManagerProperties : SimpleNodeManagerTests
        {

        }
        #endregion SimpleNodeManagerProperties

        #region SimpleNodeManagerConstruction
        [TestClass]
        public class SimpleNodeManagerConstruction : SimpleNodeManagerTests
        {

        }
        #endregion SimpleNodeManagerConstruction

        #region SimpleNodeManagerMethods
        [TestClass]
        public class SimpleNodeManagerMethods : SimpleNodeManagerTests
        {
            #region AddRoot
            [TestMethod]
            public void AddRoot()
            {
                this.RegisterDependencies();
                INodeManager<object, INode<object>> nodeManager = DiManager.GetInstance().Resolve<INodeManager<object, INode<object>>>();
                INode<object> node = new SimpleNode("SomeObject", null);

                nodeManager.AddRoot(node);

                Assert.IsNotNull(nodeManager.Root);
                Assert.AreEqual(node.Id, nodeManager.Root.Id);
            }
            #endregion AddRoot

            #region GetCurrentNode
            [TestMethod]
            public void GetCurrentNode()
            {
                this.RegisterDependencies();
                INodeManager<object, INode<object>> nodeManager = DiManager.GetInstance().Resolve<INodeManager<object, INode<object>>>();
                INode<object> node = new SimpleNode("SomeObject", null);

                nodeManager.AddRoot(node);
                INode<object> currentNode = nodeManager.GetCurrentNode();

                Assert.IsNotNull(currentNode);
                Assert.AreEqual(node.Id, currentNode.Id);
            }
            #endregion GetCurrentNode

            #region SetCurrentNode
            [TestMethod]
            public void SetCurrentNode()
            {
                this.RegisterDependencies();
                INodeManager<object, INode<object>> nodeManager = DiManager.GetInstance().Resolve<INodeManager<object, INode<object>>>();
                INode<object> node = new SimpleNode("SomeObject", null);
                node.AddChild(new SimpleNode("SomeObject2", node));
                node.AddChild(new SimpleNode("SomeObject3", node));
                node.AddChild(new SimpleNode("SomeObject4", node));

                nodeManager.AddRoot(node);
                INode<object> currentNode = nodeManager.GetCurrentNode();
                nodeManager.SetCurrentNode(node.Children.ToList()[1]);
                INode<object> currentNodeNew = nodeManager.GetCurrentNode();

                Assert.IsNotNull(currentNodeNew);
                Assert.AreEqual(node.Children.ToList()[1].Id, currentNodeNew.Id);
            }
            #endregion SetCurrentNode
        }
        #endregion SimpleNodeManagerMethods
    }

}
