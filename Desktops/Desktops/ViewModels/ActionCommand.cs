using System;
using System.Windows.Input;


namespace Desktops.ViewModels
{

    public class ActionCommand : ICommand
    {
        private Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public ActionCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }


}