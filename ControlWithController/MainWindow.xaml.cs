using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using CodeGenerater.Diablo3.Controller;

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
		#endregion

		#region Event Handler
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//if(Process.GetProcessesByName("CWCLog").Count() == 0)
			//	Process.Start(Path.Combine(Environment.CurrentDirectory, "CWCLog"));

			//Logger.Init();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			if (SingleInstance<ButtonReactionManager>.IsExist)
				SingleInstance<ButtonReactionManager>.Instance.Dispose();

			if (SingleInstance<BindingSettingManager>.IsExist)
				SingleInstance<BindingSettingManager>.Instance.Dispose();

			System.Threading.Thread.Sleep(3);

			if (SingleInstance<XInputController>.IsExist)
				SingleInstance<XInputController>.Instance.Dispose();
		}
		#endregion
	}
}