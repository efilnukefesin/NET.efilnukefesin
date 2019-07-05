using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface INavigationPresenter : IBaseObject
    {
        #region Properties

        #region IsPresenterRegistered: indicates, if the presenter is ready to present
        /// <summary>
        /// indicates, if the presenter is ready to present
        /// </summary>
        bool IsPresenterRegistered { get; }
        #endregion IsPresenterRegistered

        #endregion Properties

        #region Methods
        void RegisterPresenter(object Presenter);
        bool Present(string ViewUri, object DataContext);
        void Back();
        #endregion Methods
    }
}
