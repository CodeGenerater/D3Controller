using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class ButtonReactionManager : IDisposable
	{
		#region Constructor
		public ButtonReactionManager()
		{
			SingleInstance<XInputController>.Instance.ButtonPressed += XInput_ButtonPressed;
			SingleInstance<XInputController>.Instance.TriggerPressed += XInput_TriggerPressed;
			SingleInstance<XInputController>.Instance.StickMoved += XInput_StickMoved;
		}
		#endregion

		#region Field
		DelayManager<Direction> TriggerDelay = new DelayManager<Direction>(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1));
		DelayManager<Direction> StickDelay = new DelayManager<Direction>(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1));

		volatile bool IsDisposed = false;
		#endregion

		#region Event Handler
		private void XInput_ButtonPressed(object sender, ButtonEventArgs e)
		{
			try
			{
				if (IsDisposed) return;

				App.Current.Dispatcher.Invoke(() =>
				{
					if (e.Button == PadButtons.None) return;
					if (!SingleInstance<DelayManager<PadButtons>>.Instance.CheckDelay(e.Button)) return;
					if (App.Current == null || App.Current.MainWindow == null) return;
					var C = App.Current.MainWindow.FindName(e.Button.ToString()) as Control;
					if (C == null) return;
					GlowManager.Glow(C);
				});
			}
			catch (Exception)
			{

			}
		}

		private void XInput_TriggerPressed(object sender, TriggerEventArgs e)
		{
			try
			{
				if (IsDisposed) return;

				App.Current.Dispatcher.Invoke(() =>
				{
					if (!TriggerDelay.CheckDelay(e.Trigger)) return;
					if (App.Current == null || App.Current.MainWindow == null) return;
					var C = App.Current.MainWindow.FindName($"{e.Trigger}Trigger") as Control;
					if (C == null) return;
					if(C.Background.IsFrozen)
						C.Background = new SolidColorBrush(Colors.Black);
					if (C.Foreground.IsFrozen)
						C.Foreground = new SolidColorBrush(Colors.White);
					(C.Background as SolidColorBrush).Color = Color.FromRgb(e.Value, e.Value, e.Value);
					(C.Foreground as SolidColorBrush).Color = Color.FromRgb((byte)(255 - e.Value), (byte)(255 - e.Value), (byte)(255 - e.Value));
				});
			}
			catch (Exception)
			{

			}
		}

		private void XInput_StickMoved(object sender, StickEventArgs e)
		{
			try
			{
				if (IsDisposed) return;

				App.Current.Dispatcher.Invoke(() =>
				{
					if (!StickDelay.CheckDelay(e.Stick)) return;
					if (App.Current == null || App.Current.MainWindow == null) return;
					var E = App.Current.MainWindow.FindName($"Ellipse_{e.Stick}") as UIElement;
					if (E == null) return;
					var T = StickHelper.AsCircular(Tuple.Create(e.X, e.Y), 150);
					double X = Math.Round(T.Item1) + 150;
					double Y = Math.Round(T.Item2) + 150;
					Canvas.SetLeft(E, X);
					Canvas.SetBottom(E, Y);
				});
			}
			catch (Exception)
			{

			}
		}
		#endregion

		#region Interface Implement
		public void Dispose()
		{
			IsDisposed = true;
		}
		#endregion
	}
}