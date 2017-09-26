using System;
using System.Windows.Input;
using CodeGenerater.Diablo3.Macro;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class MacroRemoveCommand : ICommand
	{
		#region Interface Implement
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => !SingleInstance<MacroManager>.Instance.NowRun;

		public void Execute(object parameter)
		{
			int Index = (int)parameter;

			try
			{
				SingleInstance<MacroManager>.Instance.List.RemoveAt(Index);
			}
			catch (IndexOutOfRangeException)
			{

			}
		}
		#endregion
	}
}