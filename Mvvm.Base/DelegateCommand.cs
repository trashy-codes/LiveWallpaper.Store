using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Mvvm.Base
{
    public class DelegateCommand : System.Windows.Input.ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute, Func<bool> canexecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canexecute ?? (() => true);
        }

        public bool CanExecute(object p = null)
        {
            try { return _canExecute(); }
            catch { return false; }
        }

        public void Execute(object p = null)
        {
            if (!CanExecute(p))
                return;
            try { _execute(); }
            catch { Debugger.Break(); }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : System.Windows.Input.ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canexecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canexecute ?? (e => true);
        }

        public bool CanExecute(object p)
        {
            try
            {
                var _Value = (T)Convert.ChangeType(p, typeof(T));
                return _canExecute == null ? true : _canExecute(_Value);
            }
            catch { return false; }
        }

        public void Execute(object p)
        {
            if (!CanExecute(p))
                return;
            var _Value = (T)Convert.ChangeType(p, typeof(T));
            _execute(_Value);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
