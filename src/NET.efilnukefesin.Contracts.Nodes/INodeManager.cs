using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Nodes
{
    public interface INodeManager<TNode, TPayload> : IBaseObject where TNode : INode<TPayload>
    {
        #region Properties

        TNode Root { get; }
        TNode ActiveNode { get; }

        #endregion Properties

        #region Methods

        int GetCurrentChildCount();
        void Traverse(int childIndex);
        void AddRoot(TNode Root);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
