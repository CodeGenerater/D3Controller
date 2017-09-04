using System.Windows;

namespace ControllerTest
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
		MainViewModel mm;
		#endregion

		#region Event Handler
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			mm = new MainViewModel();
			DataContext = mm;

			Title = $"{SystemParameters.KeyboardDelay}, {SystemParameters.KeyboardSpeed}";
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			mm.Dispose();
		}
		#endregion

		private void Window_Closed(object sender, System.EventArgs e)
		{
		}
	}
}