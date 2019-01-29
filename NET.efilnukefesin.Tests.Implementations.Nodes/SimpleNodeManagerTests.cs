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
        protected void RegisterDependencies()
        {
            DiManager.GetInstance().RegisterInstance<INodeManager<INode>>(new SimpleNodeManager<INode>());
        }

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
                INodeManager<INode> nodeManager = DiManager.GetInstance().Resolve<INodeManager<INode>>();
                INode node = new SimpleNode(null);

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
                INodeManager<INode> nodeManager = DiManager.GetInstance().Resolve<INodeManager<INode>>();
                INode node = new SimpleNode(null);

                nodeManager.AddRoot(node);
                INode currentNode = nodeManager.GetCurrentNode();

                Assert.IsNotNull(currentNode);
                Assert.AreEqual(node.Id, currentNode.Id);
            }
            #endregion GetCurrentNode

            #region SetCurrentNode
            [TestMethod]
            public void SetCurrentNode()
            {
                this.RegisterDependencies();
                INodeManager<INode> nodeManager = DiManager.GetInstance().Resolve<INodeManager<INode>>();
                INode node = new SimpleNode(null);
                node.AddChild(new SimpleNode(node));
                node.AddChild(new SimpleNode(node));
                node.AddChild(new SimpleNode(node));

                nodeManager.AddRoot(node);
                INode currentNode = nodeManager.GetCurrentNode();
                nodeManager.SetCurrentNode(node.Children.ToList()[1]);
                INode currentNodeNew = nodeManager.GetCurrentNode();

                Assert.IsNotNull(currentNodeNew);
                Assert.AreEqual(node.Children.ToList()[1].Id, currentNodeNew.Id);
            }
            #endregion SetCurrentNode
        }
        #endregion SimpleNodeManagerMethods
    }

}
