using System;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class KeyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> ((Key)value) == Key.Enter ? "Enter" : value.ToString();

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}