using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NET.efilnukefesin.Contracts.Nodes;
using NET.efilnukefesin.Implementations.Base;

namespace NET.efilnukefesin.Implementations.Nodes
{
    public class SimpleNode<T> : BaseObject, INode<T>
    {
        #region Properties

        public T Data { get; set; }
        public INode<T> Parent { get; set; }
        public IEnumerable<INode<T>> Children { get; set; }
        public Action TraverseAction { get; set; }

        #endregion Properties

        #region Construction

        public SimpleNode(T Data, INode<T> Parent, Action TraverseAction = null)
        {
            this.Data = Data;
            this.Parent = Parent;
            this.Children = new List<INode<T>>();
            this.TraverseAction = TraverseAction;
        }

        #endregion Construction

        #region Methods

        #region AddChild
        public void AddChild(INode<T> Child)
        {
            this.Children = this.Children.Concat(new[] { Child });
        }
        #endregion AddChild

        #region Traverse
        public void Traverse()
        {
            this.TraverseAction?.Invoke();
        }
        #endregion Traverse

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