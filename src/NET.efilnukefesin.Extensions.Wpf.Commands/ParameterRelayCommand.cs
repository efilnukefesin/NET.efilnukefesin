using System;
using System.Diagnostics;
using System.Windows.Input;

namespace NET.efilnukefesin.Extensions.Wpf.Commands
{
    public class ParameterRelayCommand<T> : ICommand
    {
        #region Properties

        readonly Action<T> executeAction;
        readonly Predicate<T> canExecuteAction;

        #endregion Properties

        #region Construction

        public ParameterRelayCommand(Action<T> execute)
        : this(execute, null)
        {
        }

        public ParameterRelayCommand(Action<T> execute, Predicate<T> canExecute)
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
            return this.canExecuteAction == null ? true : this.canExecuteAction((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.executeAction((T)parameter);
        }

        #endregion // ICommand Members
    }
}
