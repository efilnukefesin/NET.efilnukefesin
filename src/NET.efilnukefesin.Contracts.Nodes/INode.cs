using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Nodes
{
    public interface INode<T> : IBaseObject, IOnEnter, IOnExit
    {
        #region Properties

        T Data { get; set; }
        INode<T> Parent { get; set; }
        IEnumerable<INode<T>> Children { get; set; }
        Action TraverseAction { get; set; }

        #endregion Properties

        #region Methods

        void AddChild(INode<T> Child);

        #region Traverse: Pass the node and do all necessary actions
        /// <summary>
        /// Pass the node and do all necessary actions
        /// </summary>
        void Traverse();
        #endregion Traverse

        #endregion Methods
    }
}
