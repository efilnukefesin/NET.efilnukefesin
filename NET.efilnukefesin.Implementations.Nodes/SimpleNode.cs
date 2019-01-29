using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    public class SimpleNode : BaseObject, INode
    {
        #region Properties

        public INode Parent { get; set; }
        public IEnumerable<INode> Children { get; set; }

        #endregion Properties

        #region Construction

        public SimpleNode(INode Parent)
        {
            this.Parent = Parent;
            this.Children = new List<INode>();
        }

        #endregion Construction

        #region Methods

        #region AddChild
        public void AddChild(INode Child)
        {
            this.Children = this.Children.Concat(new[] { Child });
        }
        #endregion AddChild

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