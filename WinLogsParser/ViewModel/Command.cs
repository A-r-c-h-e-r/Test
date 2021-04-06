using System;
using System.Windows.Input;

namespace WinLogsParser.ViewModel
{
    public class Command : ICommand
    {
        #region Properties
        private Action<object> execute;
        private Func<object, bool> canExecute;
        #endregion

        #region Constructor
        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter) =>
            this.execute(parameter);
        #endregion
    }
}
