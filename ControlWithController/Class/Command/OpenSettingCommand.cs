using System;
using System.Windows.Input;
using CodeGenerater.Diablo3.Macro;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class OpenSettingCommand : ICommand
	{
		#region Interface Implement
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => !SingleInstance<MacroManager>.Instance.NowRun;

		public void Execute(object parameter)
		{
			string Param = parameter as string;

			if (Param.Contains("Stick"))
			{
				StickSettingDialog Dialog = new StickSettingDialog();
				Dialog.Setting = SingleInstance<BindingSettingManager>.Instance[(Direction)Enum.Parse(typeof(Direction), Param.Replace("Stick", string.Empty))];
				Dialog.ShowDialog();
			}
			else if (Param == "Macro")
			{
				MacroDialog Dialog = new MacroDialog();
				Dialog.ShowDialog();
			}
			else
			{
				KeySettingDialog Dialog = new KeySettingDialog();
				Dialog.Setting = SingleInstance<BindingSettingManager>.Instance[(PadButtons)Enum.Parse(typeof(PadButtons), Param)];
				Dialog.ShowDialog();
			}
		}
		#endregion
	}
}