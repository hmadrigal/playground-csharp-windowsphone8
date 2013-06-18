using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HomeWork2.Interactivity
{
    public class RelayCommand : ICommand
    {
        private Delegate _canExecute;
        private Delegate _cancelAction;
        private Delegate _executeAction;
        private int _parameterCount;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var result = (_canExecute == null && _executeAction != null) || (bool)_canExecute.DynamicInvoke(parameter);
            return result;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                ExecuteAction(parameter);
            }
            else
            {
                _cancelAction.DynamicInvoke(parameter);
            }
        }

        private void ExecuteAction(object parameter)
        {
            if (_parameterCount == 0)
            {
                _executeAction.DynamicInvoke();
            }
            else
            {
                _executeAction.DynamicInvoke(parameter);
            }
        }

        public RelayCommand(Action execution)
        {
            _executeAction = execution;
            _parameterCount = _executeAction.Method.GetParameters().Length;
        }

        public RelayCommand(Action<Object> execution, Func<object, bool> canExecute = null, Action<object> cancelAction = null)
        {
            _executeAction = execution;
            _canExecute = canExecute;
            _cancelAction = cancelAction;
        }



    }

    public class RelayCommand<T> : ICommand
    {
        private Delegate _canExecute;
        private Delegate _executeAction;
        private Delegate _cancelAction;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var result = _canExecute == null || (bool)_canExecute.DynamicInvoke(parameter);
            return result;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeAction.DynamicInvoke(parameter);
            }
            else
            {
                _cancelAction.DynamicInvoke(parameter);
            }
        }

        public RelayCommand(Action<T> execution, Func<T, bool> canExecute = null, Action<T> cancelAction = null)
        {
            _executeAction = execution;
            _canExecute = canExecute;
            _cancelAction = cancelAction;
        }
    }
}
