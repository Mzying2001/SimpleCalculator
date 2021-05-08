﻿using System;
using System.Windows.Input;

namespace SimpleCalculator.Commands
{
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute == null || CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute?.Invoke(parameter);
        }

        public Func<object, bool> CanExecute { get; set; }

        public Action<object> Execute { get; set; }

        public DelegateCommand() { }

        public DelegateCommand(Action<object> executeDelegate)
        {
            Execute = executeDelegate;
        }

        public DelegateCommand(Action executeDelegate) : this(p => executeDelegate()) { }

        public DelegateCommand(Func<object, bool> canExecuteDelegate)
        {
            CanExecute = canExecuteDelegate;
        }

        public DelegateCommand(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
        {
            Execute = executeDelegate;
            CanExecute = canExecuteDelegate;
        }
    }
}
