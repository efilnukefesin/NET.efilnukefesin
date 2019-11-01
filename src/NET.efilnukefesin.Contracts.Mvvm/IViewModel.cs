using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface IViewModel : IBaseObject, INotifyPropertyChanged
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Initialize
        /// <summary>
        /// this is being called after the ViewModels have been instanciated and should be used for init code, e.g. initial Navigation
        /// </summary>
        void Initialize();
        #endregion Initialize

        #endregion Methods
    }
}
