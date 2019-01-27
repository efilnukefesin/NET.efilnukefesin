using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Nodes
{
    public interface INodeManager<T> : IBaseObject where T: INode
    {
        #region Properties

        T Root { get; };

        #endregion Properties

        #region Methods

        T GetCurrentNode();
        void AddRoot(T RootNode);

        #endregion Methods

        #region Events

        #endregion Events
    }
}
