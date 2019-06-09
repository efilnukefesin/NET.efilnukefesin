using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface INavigationService : IBaseObject
    {
        #region Methods

        bool CanNavigate(string ViewModelName);
        bool Navigate(string ViewModelName);

        #endregion Methods

        #region Events

        event EventHandler NavigationStarted;
        event EventHandler NavigationSuccessful;
        event EventHandler NavigationFailed;

        #endregion Events
    }
}
