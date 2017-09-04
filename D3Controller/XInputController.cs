using System;
using System.Threading;
using CodeGenerater.WCFLogger.Client;
using x = SharpDX.XInput;

namespace CodeGenerater.Diablo3.Controller
{
	public class XInputController : IDisposable
	{
		#region Constructor
		public XInputController()
		{
			Controller = new x.Controller(x.UserIndex.One);

			T = new Thread(EventLoop);
			Continue = true;
			T.Start();
		}
		#endregion

		#region Field
		x.Controller Controller;

		Thread T;

		volatile bool Continue;
		#endregion

		#region Event
		public event EventHandler<StickEventArgs> StickMoved;

		public event EventHandler<TriggerEventArgs> TriggerPressed;

		public event EventHandler<ButtonEventArgs> ButtonPressed;
		#endregion

		#region Helper
		void EventLoop()
		{
			while (Continue)
			{
				Controller.GetState(out x.State State);
				var Pad = State.Gamepad;

				StickMoved?.Invoke(this, new StickEventArgs(Direction.Left, Pad.LeftThumbX, Pad.LeftThumbY));
				StickMoved?.Invoke(this, new StickEventArgs(Direction.Right, Pad.RightThumbX, Pad.RightThumbY));

				TriggerPressed?.Invoke(this, new TriggerEventArgs(Direction.Left, Pad.LeftTrigger));
				TriggerPressed?.Invoke(this, new TriggerEventArgs(Direction.Right, Pad.RightTrigger));

				foreach (x.GamepadButtonFlags each in Enum.GetValues(typeof(x.GamepadButtonFlags)))
					if (Pad.Buttons.HasFlag(each))
						ButtonPressed?.Invoke(this, new ButtonEventArgs((PadButtons)each));
			}
		}
		#endregion

		#region Interface Implement
		public void Dispose()
		{
			Continue = false;
		}
		#endregion
	}
}