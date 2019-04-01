using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    public class SimpleNodeManager<TNode, TPayload> : BaseObject, INodeManager<TNode, TPayload> where TNode : INode<TPayload>
    {
        #region Properties

        public TNode Root { get; private set; }
        public TNode ActiveNode { get; private set; }

        #endregion Properties

        #region Construction

        public SimpleNodeManager(TNode Root)
        {
            this.AddRoot(Root);
        }

        #endregion Construction

        #region Methods

        #region AddRoot
        public void AddRoot(TNode Root)
        {
            this.Root = Root;
            this.ActiveNode = this.Root;
        }
        #endregion AddRoot

        #region GetCurrentChildCount
        public int GetCurrentChildCount()
        {
            return this.ActiveNode.Children.Count();
        }
        #endregion GetCurrentChildCount

        #region Traverse
        public void Traverse(int childIndex)
        {
            this.ActiveNode.Children.ToList()[childIndex].Traverse();
            this.ActiveNode = (TNode)this.ActiveNode.Children.ToList()[childIndex];
        }
        #endregion Traverse

        #region dispose
        protected override void dispose()
        {
            this.Root = default(TNode);
            this.ActiveNode = default(TNode);
        }
        #endregion dispose

        #endregion Methods
    }
}
