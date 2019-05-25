using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NET.efilnukefesin.Extensions.Wpf.Commands
{
    public abstract class BaseCommand : BaseObject
    {
        #region RaiseCanExecuteChanged
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion RaiseCanExecuteChanged

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
