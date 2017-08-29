using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ControllerTest
{
	class GlowingButton : Button
	{
		#region Field
		Storyboard S;
		#endregion

		#region Method
		public void Glow()
		{
			BeginStoryboard(S);
		}
		#endregion

		#region Event Handler
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			Background = new SolidColorBrush(Colors.Black);

			ColorAnimation c = new ColorAnimation()
			{
				Duration = new Duration(TimeSpan.FromMilliseconds(100)),
				To = Colors.White,
				From = Colors.Black,
				AutoReverse = true
			};

			Storyboard.SetTarget(c, this);
			Storyboard.SetTargetProperty(c, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));

			S = new Storyboard();
			S.Children.Add(c);
		}
		#endregion
	}
}