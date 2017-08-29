using System;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Threading.Tasks;
using CodeGenerater.Diablo3.Controller;

namespace ControllerTest
{
	class MainViewModel : INotifyPropertyChanged, IDisposable
	{
		#region Constructor
		public MainViewModel()
		{
			Controller = new XInputController();

			Controller.ButtonPressed += Controller_ButtonPressed;
			Controller.TriggerPressed += Controller_TriggerPressed;
			Controller.StickMoved += Controller_StickMoved;
		}
		#endregion

		#region Field
		XInputController Controller;

		TimeChecker tc = new TimeChecker();
		#endregion

		#region Event Handler
		private void Controller_ButtonPressed(object sender, ButtonEventArgs e)
		{
			App.Current.Dispatcher.Invoke(
				() =>
				{
					if (e.Button == PadButtons.None)
						return;

					if (!tc.CheckTime(e.Button.ToString(), 200))
						return;

					if (App.Current == null || App.Current.MainWindow == null)
						return;

					var gb = App.Current.MainWindow.FindName(e.Button.ToString()) as GlowingButton;
					gb.Glow();
				});
		}

		private void Controller_TriggerPressed(object sender, TriggerEventArgs e)
		{
			App.Current.Dispatcher.Invoke(
				() =>
				{
					if (App.Current == null || App.Current.MainWindow == null)
						return;

					var tb = App.Current.MainWindow.FindName($"{e.Trigger}T") as TriggerButton;
					tb.Value = e.Value;
				});
		}

		private void Controller_StickMoved(object sender, StickEventArgs e)
		{
			App.Current.Dispatcher.Invoke(
				() =>
				{
					if (App.Current == null || App.Current.MainWindow == null)
						return;
					
					var ellipse = App.Current.MainWindow.FindName($"{e.Stick}S") as Ellipse;
					var b = App.Current.MainWindow.FindName($"{e.Stick}SB") as TextBlock;
					int X = ConvertValue(e.X);
					int Y = ConvertValue(e.Y);
					double Theta = Math.Atan2(e.Y, e.X);
					b.Text = $"{X},{Y}";
					Canvas.SetLeft(ellipse, (int)(50 + Math.Cos(Theta) * Math.Abs((int)e.X) / 1000));
					Canvas.SetBottom(ellipse, (int)(50 + Math.Sin(Theta) * Math.Abs((int)e.Y) / 1000));
				});
		}
		#endregion

		#region Helper
		int ConvertValue(short Value) => (int)Math.Round((double)100 / 65535 * Value + 50);
		#endregion

		#region Interface Implement
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

		public void Dispose()
		{
			Controller.Dispose();
		}
		#endregion
	}
}