using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface INavigationService
    {
        #region Methods

        bool CanNavigate(string ViewModelName);
        bool Navigate(string ViewModelName);

        #endregion Methods
    }
}
