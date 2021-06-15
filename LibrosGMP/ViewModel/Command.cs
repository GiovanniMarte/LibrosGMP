using System;
using System.Windows.Input;

namespace LibrosGMP.ViewModel
{
    class Command : ICommand
    {
        readonly Action<object> accionAEjecutar;
        public Command(Action<object> execute) : this(execute, null)
        {
        }
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            accionAEjecutar = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            accionAEjecutar(parameter);
        }
    }
}