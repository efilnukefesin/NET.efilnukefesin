using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    public class SimpleNodeManager<I, T> : BaseObject, INodeManager<I, T> where T: INode<I>
    {
        #region Properties

        public T Root { get; private set; }

        private T activeNode;

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region AddRoot
        public void AddRoot(T RootNode)
        {
            this.Root = RootNode;
            this.activeNode = this.Root;
        }
        #endregion AddRoot

        #region GetCurrentNode
        public T GetCurrentNode()
        {
            return this.activeNode;
        }
        #endregion GetCurrentNode

        #region SetCurrentNode
        public void SetCurrentNode(T Node)
        {
            this.activeNode = Node;
        }
        #endregion SetCurrentNode

        #region dispose
        protected override void dispose()
        {
            this.Root = default(T);
            this.activeNode = default(T);
        }
        #endregion dispose

        #endregion Methods
    }
}
