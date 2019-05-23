using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NET.efilnukefesin.Extensions.Wpf.Commands
{
    public class RelayCommand : ICommand
    {
        #region Properties

        readonly Action executeAction;
        readonly Func<bool> canExecuteAction;

        #endregion Properties

        #region Construction

        public RelayCommand(Action execute)
        : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.executeAction = execute;
            this.canExecuteAction = canExecute;
        }
        #endregion Construction

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecuteAction == null ? true : this.canExecuteAction();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.executeAction();
        }

        #endregion // ICommand Members
    }
}
