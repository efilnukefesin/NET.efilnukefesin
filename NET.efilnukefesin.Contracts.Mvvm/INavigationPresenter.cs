using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface INavigationPresenter
    {
        #region Methods
        void RegisterPresenter(object Presenter);
        bool Present(string ViewUri);
        #endregion Methods
    }
}
