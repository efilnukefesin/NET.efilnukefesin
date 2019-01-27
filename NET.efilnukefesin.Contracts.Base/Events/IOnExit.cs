using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Base.Events
{
    public interface IOnExit
    {
        #region Methods

        void Exit();

        #endregion Methods

        #region Events

        event EventHandler OnExit;

        #endregion Events
    }
}
