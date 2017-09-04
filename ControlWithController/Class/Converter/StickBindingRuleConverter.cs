using System;
using System.Windows.Data;
using System.Globalization;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class StickBindingRuleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString() == parameter.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Enum.Parse(typeof(StickBindingRule), parameter.ToString());
		}
	}
}