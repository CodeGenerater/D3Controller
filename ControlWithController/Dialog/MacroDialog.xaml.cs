using System.Windows;
using System.Windows.Input;
using CodeGenerater.Diablo3.Macro;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public partial class MacroDialog : Window
    {
		#region Constructor
		public MacroDialog()
        {
            InitializeComponent();
        }
		#endregion

		#region Event Handler
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = SingleInstance<MacroManager>.Instance;
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			MacroKey Key = ListView_Macro.SelectedItem as MacroKey;
			if(Key != null)
				Key.Key = e.Key;
			(sender as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
			e.Handled = true;
		}
		#endregion
	}
}