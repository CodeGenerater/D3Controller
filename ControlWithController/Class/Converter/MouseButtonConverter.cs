using System;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class MouseButtonConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var Button = value as MouseButton?;

			if (!Button.HasValue)
				return "None";
			else
				return Button.Value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var String = value as string;

			if (String == "None")
				return null;
			else
				return Enum.Parse(typeof(MouseButton), String);
		}
	}
}