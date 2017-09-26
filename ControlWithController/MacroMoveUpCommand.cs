using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CodeGenerater.Diablo3.Macro;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class MacroMoveUpCommand : ICommand
	{
		#region Interface Implement
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			int Index = (int)parameter;

			return Index > 0;
		}

		public void Execute(object parameter)
		{
			int Index = (int)parameter;

			SingleInstance<MacroManager>.Instance.List.Move(Index, Index - 1);
		}
		#endregion
	}
}