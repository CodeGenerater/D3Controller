using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CodeGenerater.Diablo3.ControlWithController
{
	static class GlowManager
	{
		#region Field
		static List<Control> Targets = new List<Control>();

		static ConcurrentQueue<Control> TempQueue = new ConcurrentQueue<Control>();

		static ColorAnimation C;
		#endregion

		#region Method
		public static void Glow(this Control Target)
		{
			if (TempQueue.Contains(Target))
				return;

			Regist(Target);
			Target.Background.BeginAnimation(SolidColorBrush.ColorProperty, C);

			TempQueue.Enqueue(Target);
			Task.Factory.StartNew(() =>
			{
				Thread.Sleep(200);
				TempQueue?.TryDequeue(out Control notuse);
			});
		}
		#endregion

		#region Helper
		static void Regist(Control Target)
		{
			if (Targets.Contains(Target))
				return;

			if (C == null)
				C = new ColorAnimation()
				{
					From = Colors.Black,
					To = Colors.White,
					AutoReverse = true,
					Duration = new Duration(TimeSpan.FromMilliseconds(100)),
				};

			Target.Background = new SolidColorBrush(Colors.Black);
			Targets.Add(Target);
		}
		#endregion
	}
}