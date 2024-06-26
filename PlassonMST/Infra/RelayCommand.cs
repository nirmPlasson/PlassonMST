﻿using System.Windows.Input;

namespace PlassonMST.Infra
{
	public class RelayCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public event EventHandler? CanExecuteChanged;

		public RelayCommand(Action execute) : this(execute, null) { }

		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object? parameter) => _canExecute == null ? true : _canExecute();

		public void Execute(object? parameter) => _execute();

		public void RaiseCanExecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}

	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;


		public event EventHandler? CanExecuteChanged;

		public bool CanExecute(object? parameter)
		{
			if (parameter != null)
			{
				return _canExecute == null ? true : _canExecute((T)parameter);
			}
			else
			{
				return false;
			}
		}

		public void Execute(object parameter) => _execute((T)parameter);

		public RelayCommand(Action<T> execute) : this(execute, null) { }
		public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		public void RaiseCanExecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}
