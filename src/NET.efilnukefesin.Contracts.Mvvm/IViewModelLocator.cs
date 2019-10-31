using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Mvvm
{
    public interface IViewModelLocator : IBaseObject
    {
        #region Properties

        bool IsBusy { get; }

        #endregion Properties

        #region Methods
        void Register(string name, object o);
        object GetInstance(string name);
        object this[string name] { get; }
        #endregion Methods
    }
}
