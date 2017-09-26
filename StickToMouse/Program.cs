using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using CodeGenerater.Diablo3.Controller;
using m = CodeGenerater.Macro.HardwareControl.Mouse;

namespace StickToMouse
{
	class Program
	{
		static XInputController Controller = new XInputController();
		static DelayManager<Direction> StickDelay = new DelayManager<Direction>();
		static DelayManager<PadButtons> ButtonDelay = new DelayManager<PadButtons>();
		static Dictionary<PadButtons, DateTime> dictionary = new Dictionary<PadButtons, DateTime>();
		static Dictionary<PadButtons, bool> dictionary2 = new Dictionary<PadButtons, bool>();
		const int DELAY = 5;

		static void Main(string[] args)
		{
			dictionary.Add(PadButtons.A, DateTime.Now);
			dictionary.Add(PadButtons.B, DateTime.Now);
			dictionary.Add(PadButtons.LeftShoulder, DateTime.Now);
			dictionary.Add(PadButtons.RightShoulder, DateTime.Now);
			dictionary2.Add(PadButtons.A, false);
			dictionary2.Add(PadButtons.B, false);
			dictionary2.Add(PadButtons.LeftShoulder, false);
			dictionary2.Add(PadButtons.RightShoulder, false);

			Controller.StickMoved += Controller_StickMoved;
			Controller.ButtonPressed += Controller_ButtonPressed;

			Console.ReadLine();

			Controller.Dispose();
		}

		static void Controller_ButtonPressed(object sender, ButtonEventArgs e)
		{
			DateTime Now = DateTime.Now;

			foreach (PadButtons each in Enum.GetValues(typeof(PadButtons)))
				if (dictionary.ContainsKey(each))
					if (((Now - dictionary[each]).TotalMilliseconds > DELAY) && dictionary2[each])
					{
						m.Up(FromPadButton(each));
						dictionary2[each] = false;
					}

			if (dictionary.ContainsKey(e.Button) && !dictionary2[e.Button] && (DateTime.Now - dictionary[e.Button]).TotalMilliseconds > DELAY)
				try { m.Down(FromPadButton(e.Button)); dictionary2[e.Button] = true; }
				catch (NotImplementedException) { }

			if(dictionary.ContainsKey(e.Button))
				dictionary[e.Button] = DateTime.Now;
		}

		static void Controller_StickMoved(object sender, StickEventArgs e)
		{
			if (e.Stick == Direction.Left)
			{
				if (!StickDelay.CheckDelay(Direction.Left)) return;
				var Point = StickHelper.AsCircular(Tuple.Create(e.X, e.Y), 50);
				m.MoveRelative((int)Math.Round(Point.Item1), -(int)Math.Round(Point.Item2));
			}
			else if (e.Stick == Direction.Right)
			{
				if (!StickDelay.CheckDelay(Direction.Right)) return;
				if (e.Y == 0) return;
				if (e.Y > 0) m.WheelUp(1);
				if (e.Y < 0) m.WheelDown(1);
			}
		}

		static MouseButton FromPadButton(PadButtons PB)
		{
			switch (PB)
			{
				case PadButtons.A: return MouseButton.Left;
				case PadButtons.B: return MouseButton.Right;
				case PadButtons.LeftShoulder: return MouseButton.XButton1;
				case PadButtons.RightShoulder: return MouseButton.XButton2;
				default: throw new NotImplementedException();
			}
		}
	}
}
