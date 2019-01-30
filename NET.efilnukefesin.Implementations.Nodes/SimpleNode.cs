using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    public class SimpleNode : BaseObject, INode<object>
    {
        #region Properties

        public object Data { get; set; }
        public INode<object> Parent { get; set; }
        public IEnumerable<INode<object>> Children { get; set; }

        #endregion Properties

        #region Construction

        public SimpleNode(object Data, INode<object> Parent)
        {
            this.Data = Data;
            this.Parent = Parent;
            this.Children = new List<INode<object>>();
        }

        #endregion Construction

        #region Methods

        #region AddChild
        public void AddChild(INode<object> Child)
        {
            this.Children = this.Children.Concat(new[] { Child });
        }
        #endregion AddChild

        public void Traverse()
        {
            throw new NotImplementedException();
        }

        #region dispose
        protected override void dispose()
        {
            this.Parent = null;
            this.Children = null;
        }
        #endregion dispose

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