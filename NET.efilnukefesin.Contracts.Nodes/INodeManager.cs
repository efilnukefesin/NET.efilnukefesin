using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Nodes
{
    public interface INodeManager<I, T> : IBaseObject where T: INode<I>
    {
        #region Properties

        T Root { get; }

        #endregion Properties

        #region Methods

        T GetCurrentNode();
        void AddRoot(T RootNode);
        void SetCurrentNode(T Node);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
