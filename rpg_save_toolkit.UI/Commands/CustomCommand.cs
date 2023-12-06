using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace rpg_save_toolkit.UI.Commands
{
    public class CustomCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Func<object?, bool> _canExecute { get; }
        private Action<object?> _execute { get; }

        public CustomCommand(Func<object?, bool> canExecute, Action<object?> execute) 
        {
            _canExecute = canExecute;
            _execute = execute;
        }
        
        public bool CanExecute(object? parameter)
        {
            if(_canExecute == null)
            {
                return true;
            }
            return _canExecute!.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
