using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Nodes
{
    public interface INode : IBaseObject, IOnEnter, IOnExit
    {
        #region Properties

        INode Parent { get; set; }
        IEnumerable<INode> Children { get; set; }

        #endregion Properties

        #region Methods

        #region Traverse: Pass the node and do all necessary actions
        /// <summary>
        /// Pass the node and do all necessary actions
        /// </summary>
        void Traverse();
        #endregion Traverse

        #endregion Methods
    }
}
