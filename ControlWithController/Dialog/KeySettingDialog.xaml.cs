using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public partial class KeySettingDialog : Window
	{
		#region Constructor
		KeySettingDialog()
		{
			InitializeComponent();
		}

		public KeySettingDialog(KeySetting Setting) : this()
		{
			this.Setting = Setting;
		}
		#endregion

		#region Field
		KeySetting Setting;
		#endregion

		#region Event Handler
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox_MouseButton.ItemsSource = GetComboBoxItems();

			DataContext = Setting;
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			Setting.Key = e.Key;
			(sender as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
			e.Handled = true;
		}
		#endregion

		#region Helper
		IEnumerable<string> GetComboBoxItems()
		{
			yield return "None";

			foreach (var each in Enum.GetNames(typeof(MouseButton)))
				yield return each;
		}
		#endregion

		
	}
}