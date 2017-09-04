using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class PointConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Point P = (Point)value;

			return $"{P.X}, {P.Y}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var Strings = (value as string).Split(',');

			if (Strings.Length != 2) throw new IndexOutOfRangeException();

			double X, Y;

			if (!double.TryParse(Strings[0].Trim(), out X)) throw new InvalidCastException();
			if (!double.TryParse(Strings[1].Trim(), out Y)) throw new InvalidCastException();

			return new Point(X, Y);
		}
	}
}