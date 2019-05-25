using System;
using System.Diagnostics;
using System.Windows.Input;

namespace NET.efilnukefesin.Extensions.Wpf.Commands
{
    public class ParameterRelayCommand<T> : BaseCommand, ICommand
    {
        #region Properties

        private readonly Action<T> executeAction;
        private readonly Predicate<T> canExecuteAction;

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

        #region Methods

        #region CanExecute
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecuteAction == null ? true : this.canExecuteAction((T)parameter);
        }
        #endregion CanExecute

        #region Execute
        public void Execute(object parameter)
        {
            this.executeAction((T)parameter);
        }
        #endregion Execute

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods
    }
}
