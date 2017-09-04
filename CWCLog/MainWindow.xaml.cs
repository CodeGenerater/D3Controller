using System.Windows;
using CodeGenerater.WCFLogger.Server;

namespace CWCLog
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
		LogServer Server;
		#endregion

		#region Event Handler
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Server = new LogServer("ControlWithController", new Logger());
		}
		#endregion
	}
}