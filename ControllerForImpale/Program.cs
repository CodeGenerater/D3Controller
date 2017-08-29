using System;
using SharpDX.XInput;
using System.Windows.Input;
using System.Threading.Tasks;
using k = CodeGenerater.Macro.HardwareControl.Keyboard;
using m = CodeGenerater.Macro.HardwareControl.Mouse;

namespace ControllerForImpale
{
	class Program
	{
		#region Main
		static void Main(string[] args)
		{
			while (true)
			{
				Controller.GetState(out State S);

				var Buttons = S.Gamepad.Buttons;

				if (Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
					Task.Factory.StartNew(On_LS);

				if (Mode == Modes.Ignore) continue;

				On_Stick(S.Gamepad.LeftThumbX, S.Gamepad.LeftThumbY);

				if (Buttons.HasFlag(GamepadButtonFlags.A))
					Task.Factory.StartNew(On_A);

				if (Buttons.HasFlag(GamepadButtonFlags.B))
					Task.Factory.StartNew(On_B);

				if (Buttons.HasFlag(GamepadButtonFlags.X))
					Task.Factory.StartNew(On_X);

				if (Buttons.HasFlag(GamepadButtonFlags.Y))
				{

				}

				if (Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
				{

				}

				if (Buttons.HasFlag(GamepadButtonFlags.DPadRight))
					Task.Factory.StartNew(On_Right);

				if (Buttons.HasFlag(GamepadButtonFlags.DPadUp))
				{

				}

				if (Buttons.HasFlag(GamepadButtonFlags.DPadDown))
				{

				}

				if (Buttons.HasFlag(GamepadButtonFlags.Start))
					Task.Factory.StartNew(On_Start);

				if (Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
					Task.Factory.StartNew(On_RS);

				System.Threading.Thread.Sleep(50);
			}
		}
		#endregion

		#region Inner Type
		enum Modes
		{
			Ignore,
			Pad,
			Mouse,
		}
		#endregion

		#region Field
		static D3Helper DH = new D3Helper();

		static Controller Controller = new Controller(UserIndex.One);

		static Modes Mode = Modes.Pad;

		static TimeChecker TC = new TimeChecker();
		#endregion

		#region Method
		static void Dispose()
		{
			DH.Dispose();
			DH = null;
		}
		#endregion

		#region Helper
		static double Calc(short Value)
		{
			return Value == 0 ? 0 : (Value + 0.5) / 32767.5;
		}
		#endregion

		#region Event Handler
		static void On_Stick(short X, short Y)
		{
			const int OFFSET_X = 960;
			const int OFFSET_Y = 500;

			if (X == 0 && Y == 0) return;

			switch (Mode)
			{
				case Modes.Pad:
					double Theta = Math.Atan2(Y, X);
					m.SetPosition((int)(OFFSET_X + Math.Cos(Theta) * 100), (int)(OFFSET_Y - Math.Sin(Theta) * 100));
					break;
				case Modes.Mouse:
					var p = m.Position;
					m.SetPosition((int)(p.X + Calc(X) * 50), (int)(p.Y - Calc(Y) * 50));
					break;
			}
		}

		static void On_A()
		{
			switch (Mode)
			{
				case Modes.Pad:
					k.KeyDown(Key.LeftShift);
					m.Click(MouseButton.Left);
					k.KeyUp(Key.LeftShift);
					break;
				case Modes.Mouse:
					m.Click(MouseButton.Left);
					break;
			}
		}

		static void On_B()
		{
			if (!TC.CheckTime("B", 200)) return;

			switch(Mode)
			{
				case Modes.Pad:
					m.Click(MouseButton.Right);
					break;
				case Modes.Mouse:
					k.KeyDown(Key.Space);
					k.KeyUp(Key.Space);
					break;
			}
		}

		static void On_X()
		{
			m.Click(MouseButton.Left);
		}

		static void On_Right()
		{
			k.KeyDown(Key.C);
			k.KeyUp(Key.C);
		}

		static void On_Up()
		{
			k.KeyDown(Key.P);
			k.KeyUp(Key.P);
		}

		static void On_LS()
		{
			if (!TC.CheckTime("LS", 200)) return;

			switch (Mode)
			{
				case Modes.Ignore:
					Mode = Modes.Pad;
					break;
				case Modes.Pad:
					Mode = Modes.Mouse;
					break;
				case Modes.Mouse:
					Mode = Modes.Ignore;
					break;
			}
		}

		static void On_RS()
		{
			k.KeyDown(Key.Q);
			k.KeyUp(Key.Q);
		}

		static void On_Start()
		{
			if (DH.Run)
				DH.Stop();
			else
				DH.Start();
		}
		#endregion
	}
}