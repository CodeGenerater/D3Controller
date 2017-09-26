using System;
using System.Windows.Input;
using CodeGenerater.Diablo3.Macro;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class MacroAddCommand : ICommand
	{
		#region Interface Implment
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => !SingleInstance<MacroManager>.Instance.NowRun;

		public void Execute(object parameter)
		{
			SingleInstance<MacroManager>.Instance.List.Add(new MacroKey());
		}
		#endregion
	}
}