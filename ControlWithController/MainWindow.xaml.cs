using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Diagnostics;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public partial class MainWindow : Window
	{
		#region Constructor
		public MainWindow()
		{
			InitializeComponent();
		}
		#endregion

		#region Field
		ButtonReactionManager brm;
		#endregion

		#region Event Handler
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//if(Process.GetProcessesByName("CWCLog").Count() == 0)
			//	Process.Start(Path.Combine(Environment.CurrentDirectory, "CWCLog"));

			//Logger.Init();

			KeySetting Setting = new KeySetting();
			Setting.Button = Controller.PadButtons.Start;
			Setting.BindToKeyboard = true;
			Setting.Key = System.Windows.Input.Key.Enter;
			Setting.MacroBindingRule = MacroBindingRule.Toggle;

			KeySettingDialog Dialog = new KeySettingDialog(Setting);

			Dialog.ShowDialog();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			brm?.Dispose();
		}
		#endregion
	}
}