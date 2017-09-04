using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Windows.Controls;
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

		DelayManager<PadButtons> ButtonDelay = new DelayManager<PadButtons>();
		#endregion

		#region Event Handler
		private void Controller_ButtonPressed(object sender, ButtonEventArgs e)
		{
			try
			{
				App.Current.Dispatcher.Invoke(
						() =>
						{
							if (e.Button == PadButtons.None)
								return;

							if (!ButtonDelay.CheckDelay(e.Button))
								return;

							if (App.Current == null || App.Current.MainWindow == null)
								return;

							var gb = App.Current.MainWindow.FindName(e.Button.ToString()) as GlowingButton;
							gb.Glow();
						});
			}
			catch (Exception)
			{
			}
		}

		private void Controller_TriggerPressed(object sender, TriggerEventArgs e)
		{
			try
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
			catch (Exception)
			{
			}
		}

		private void Controller_StickMoved(object sender, StickEventArgs e)
		{
			try
			{
				App.Current.Dispatcher.Invoke(
						() =>
						{
							if (App.Current == null || App.Current.MainWindow == null)
								return;

							var ellipse = App.Current.MainWindow.FindName($"{e.Stick}S") as Ellipse;
							var b = App.Current.MainWindow.FindName($"{e.Stick}SB") as TextBlock;
							var Point = StickHelper.AsCircular(Tuple.Create(e.X, e.Y), 50);

							int X = (int)Math.Round(Point.Item1) + 50;
							int Y = (int)Math.Round(Point.Item2) + 50;
							b.Text = $"{X},{Y}";
							Canvas.SetLeft(ellipse, X);
							Canvas.SetBottom(ellipse, Y);
						});
			}
			catch (Exception)
			{
			}
		}
		#endregion

		#region Helper
		int ConvertValue(short Value) => (int)Math.Round((double)100 / 65535 * Value);
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