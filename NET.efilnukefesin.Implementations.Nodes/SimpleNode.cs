using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    public class SimpleNode : BaseObject, INode
    {
        #region Properties

        public INode Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<INode> Children { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        public void Traverse()
        {
            throw new NotImplementedException();
        }

        protected override void dispose()
        {
            throw new NotImplementedException();
        }

        #region Enter
        public void Enter()
        {
            this.OnEnter?.Invoke(this, new EventArgs());
        }
        #endregion Enter

        #region Exit
        public void Exit()
        {
            this.OnExit?.Invoke(this, new EventArgs());
        }
        #endregion Exit

        #endregion Methods

        #region Events

        public event EventHandler OnEnter;
        public event EventHandler OnExit;

        #endregion Events
    }
}