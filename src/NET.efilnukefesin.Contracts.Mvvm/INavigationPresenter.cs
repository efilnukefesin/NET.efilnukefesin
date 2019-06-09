using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface INavigationPresenter : IBaseObject
    {
        #region Methods
        void RegisterPresenter(object Presenter);
        bool Present(string ViewUri, object DataContext);
        #endregion Methods
    }
}
