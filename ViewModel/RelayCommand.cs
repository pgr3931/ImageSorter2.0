using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageSorter2._0.ViewModel
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Action<object> _undo;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Action<object> undo, Predicate<object> canExecute)
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

        public event EventHandler CanExecuteChanged = delegate { };
    }
}