using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CodeGenerater.WCFLogger.Server;

namespace CWCLog
{
	class Logger : LoggerBase
	{
		#region Contructor
		public Logger()
		{
			T.Background = new SolidColorBrush(Colors.Black);

			ColorAnimation ToWhite = new ColorAnimation()
			{
				To = Colors.White,
				Duration = new Duration(TimeSpan.FromMilliseconds(100))
			};
			ColorAnimation ToBlack = new ColorAnimation()
			{
				To = Colors.Black,
				Duration = new Duration(TimeSpan.FromMilliseconds(100))
			};

			Storyboard.SetTarget(ToWhite, T);
			Storyboard.SetTarget(ToBlack, T);
			Storyboard.SetTargetProperty(ToWhite, new PropertyPath("(TextBox.Background).(SolidColorBrush.Color)"));
			Storyboard.SetTargetProperty(ToBlack, new PropertyPath("(TextBox.Background).(SolidColorBrush.Color)"));

			S = new Storyboard();
			S.Children.Add(ToWhite);
			S.Children.Add(ToBlack);
		}
		#endregion

		#region Field
		TextBox _T;

		Storyboard S;
		#endregion

		#region Property
		TextBox T
		{
			get
			{
				if (_T == null)
					_T = App.Current.MainWindow.FindName("TextBox_Log") as TextBox;
				return _T;
			}
		}
		#endregion

		#region Method
		public override void Write(string Message)
		{
			T.AppendText(Message + Environment.NewLine);
			T.ScrollToEnd();
			T.BeginStoryboard(S);
		}
		#endregion
	}
}