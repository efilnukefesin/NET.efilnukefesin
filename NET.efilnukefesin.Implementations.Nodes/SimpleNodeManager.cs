using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    //TODO: how to traverse through nodes?
    public class SimpleNodeManager<T> : BaseObject, INodeManager<T> where T: INode
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
        }
        #endregion AddRoot

        #region GetCurrentNode
        public T GetCurrentNode()
        {
            return this.activeNode;
        }
        #endregion GetCurrentNode

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
