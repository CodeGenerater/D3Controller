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
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			mm.Dispose();
		}
		#endregion
	}
}