using System;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace ControllerTest
{
	class TriggerButton : Button
	{
		#region Constructor
		static TriggerButton()
		{
			ValueProperty = DependencyProperty.Register("Value", typeof(byte), typeof(TriggerButton));
		}
		#endregion

		#region Field
		SolidColorBrush BackgroundBrush;
		SolidColorBrush ForegroundBrush;

		public static DependencyProperty ValueProperty;
		#endregion

		#region Property
		public byte Value
		{
			set
			{
				if (Value != value)
				{
					SetValue(ValueProperty, value);
					Color C = Color.FromRgb(value, value, value);
					BackgroundBrush.Color = C;
					C.A = 0;
					ForegroundBrush.Color = Colors.White - C;
					Content = value;
				}
			}
			get
			{
				return (byte)GetValue(ValueProperty);
			}
		}
		#endregion

		#region Event Handler
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			Content = 0;
			Background = BackgroundBrush = new SolidColorBrush(Colors.Black);
			Foreground = ForegroundBrush = new SolidColorBrush(Colors.White);
		}
		#endregion
	}
}