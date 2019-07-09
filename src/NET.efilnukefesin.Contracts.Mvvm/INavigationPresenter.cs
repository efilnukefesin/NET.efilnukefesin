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

        #region RegisterPresenter: Registers a presenter, this has to be called first. In WPF a Presenter is a Frame.
        /// <summary>
        /// Registers a presenter, this has to be called first. In WPF a Presenter is a Frame.
        /// </summary>
        /// <param name="Presenter">the presenting object, in WPF a Frame</param>
        void RegisterPresenter(object Presenter);
        #endregion RegisterPresenter

        #region Present: Presents the View with the given Data Context.
        /// <summary>
        /// Presents the View with the given Data Context.
        /// </summary>
        /// <param name="ViewUri">the Uri of the View, if this ends with "Window.xaml", a modal Window will be opened, otherwise the Frame will receive the Uri to navigate there.</param>
        /// <param name="DataContext">The Data Context to give</param>
        /// <returns>indicates, if presenting was successfull</returns>
        bool Present(string ViewUri, object DataContext);
        #endregion Present

        #region Back: Goes back or closes a model Window if shown
        /// <summary>
        /// Goes back or closes a model Window if shown
        /// </summary>
        void Back();
        #endregion Back

        #endregion Methods
    }
}
