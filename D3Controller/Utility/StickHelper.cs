using System;

namespace CodeGenerater.Diablo3.Controller
{
	public static class StickHelper
	{
		public static Tuple<double, double> AsCircular(Tuple<short, short> Point, double Range)
		{
			double Theta = Math.Atan2(Point.Item2, Point.Item1);
			var @new = ChangeRange(Point, Range);

			return Tuple.Create(Math.Cos(Theta) * Math.Abs(@new.Item1), Math.Sin(Theta) * Math.Abs(@new.Item2));
		}

		public static Tuple<double, double> ChangeRange(Tuple<short, short> Point, double Range)
			=> Tuple.Create(Range / 65535 * Point.Item1, Range / 65535 * Point.Item2);
	}
}