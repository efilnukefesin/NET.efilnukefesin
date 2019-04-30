using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base.Events
{
    public interface IOnEnter
    {
        #region Methods

        void Enter();

        #endregion Methods

        #region Events

        event EventHandler OnEnter;

        #endregion Events
    }
}
