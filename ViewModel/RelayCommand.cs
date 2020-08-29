using System;
using System.Windows.Input;

namespace ImageSorter2._0.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Action<object> _undo;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute, Action<object> undo = null)
        {
            _execute = execute;
            _undo = undo;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void Undo(object parameter)
        {
            _undo(parameter);
        }

        public event EventHandler CanExecuteChanged    
        {    
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }   
    }
}