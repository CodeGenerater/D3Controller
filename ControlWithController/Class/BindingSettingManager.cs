using System;
using System.Collections.Generic;
using CodeGenerater.Diablo3.Macro;
using System.Runtime.Serialization;
using CodeGenerater.Diablo3.Controller;
using m = CodeGenerater.Macro.HardwareControl.Mouse;
using k = CodeGenerater.Macro.HardwareControl.Keyboard;

namespace CodeGenerater.Diablo3.ControlWithController
{
	[Serializable]
	class BindingSettingManager : IDisposable, ISerializable
	{
		#region Constructor
		public BindingSettingManager()
		{
			KeySettingDict = new Dictionary<PadButtons, KeySetting>(Enum.GetValues(typeof(PadButtons)).Length);
			StickSettingDict = new Dictionary<Direction, StickSetting>(2);

			SingleInstance<XInputController>.Instance.ButtonPressed += Controller_ButtonPressed;
			SingleInstance<XInputController>.Instance.StickMoved += Controller_StickMoved;
		}
		#endregion

		#region Field
		[SerializationTarget]
		Dictionary<PadButtons, KeySetting> KeySettingDict;

		[SerializationTarget]
		Dictionary<Direction, StickSetting> StickSettingDict;

		bool IsDisposed;
		#endregion

		#region Property
		public KeySetting this[PadButtons B]
		{
			get
			{
				if (!KeySettingDict.ContainsKey(B))
					KeySettingDict.Add(B, new KeySetting() { Button = B });

				return KeySettingDict[B];
			}
		}

		public StickSetting this[Direction D]
		{
			get
			{
				if (!StickSettingDict.ContainsKey(D))
					StickSettingDict.Add(D, new StickSetting() { Stick = D });

				return StickSettingDict[D];
			}
		}
		#endregion

		#region Event Handler
		private void Controller_ButtonPressed(object sender, ButtonEventArgs e)
		{
			try
			{
				if (IsDisposed) return;

				App.Current.Dispatcher.Invoke(() =>
				{
					if (e.Button == PadButtons.None) return;
					if (!SingleInstance<DelayManager<PadButtons>>.Instance.CheckDelay(e.Button)) return;

					KeySetting Setting = this[e.Button];

					if (Setting.BindToKeyboard)
					{
						k.KeyDown(Setting.Key);
						k.KeyUp(Setting.Key);
					}

					if (Setting.BindToMacro)
					{
						MacroManager mm = SingleInstance<MacroManager>.Instance;

						switch (Setting.MacroBindingRule)
						{
							case MacroBindingRule.Run:
								mm.Run();
								break;
							case MacroBindingRule.Stop:
								mm.Stop();
								break;
							case MacroBindingRule.Toggle:
								if (mm.NowRun)
									mm.Stop();
								else
									mm.Run();
								break;
						}
					}

					if (Setting.BindToMouse)
					{
						if (Setting.MouseButton.HasValue)
							m.Click(Setting.MouseButton.Value);

						if (Setting.MoveX != 0 || Setting.MoveY != 0)
							m.MoveRelative(Setting.MoveX, -Setting.MoveY);
					}
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
				if (IsDisposed) return;

				App.Current.Dispatcher.Invoke(() =>
				{
					if (!SingleInstance<DelayManager<Direction>>.Instance.CheckDelay(e.Stick)) return;
					if (App.Current == null || App.Current.MainWindow == null) return;
					if (e.X == 0 && e.Y == 0) return;

					var Setting = this[e.Stick];
					
					switch (Setting.StickBindingRule)
					{
						case StickBindingRule.Move:
							var MoveValue = StickHelper.AsCircular(Tuple.Create(e.X, e.Y), Setting.MoveRange.To - Setting.MoveRange.From);
							m.MoveRelative(
								(int)Math.Round(MoveValue.Item1 + Setting.MoveRange.From),
								(int)Math.Round(MoveValue.Item2 + Setting.MoveRange.From));
							break;
						case StickBindingRule.Around:
							var AroundValue = StickHelper.AsCircular(Tuple.Create(e.X, e.Y), Setting.AroundRange.To - Setting.AroundRange.From);
							m.SetPosition(
								(int)Math.Round(Setting.Offset.X + AroundValue.Item1 + (Math.Sign(AroundValue.Item1) < 0 ? -Setting.AroundRange.From : Setting.AroundRange.From)),
								(int)Math.Round(Setting.Offset.Y - (AroundValue.Item2 + (Math.Sign(AroundValue.Item2) < 0 ? -Setting.AroundRange.From : Setting.AroundRange.From))));
							break;
					}
				});
			}
			catch (Exception)
			{

			}
		}
		#endregion

		#region Interface Implement
		#region IDisposable
		public void Dispose()
		{
			IsDisposed = true;
		}
		#endregion
		#region ISerializable
		public void GetObjectData(SerializationInfo Info, StreamingContext Context) => SerializationHelper.Serialize(this, Info);

		BindingSettingManager(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);

			SingleInstance<XInputController>.Instance.ButtonPressed += Controller_ButtonPressed;
			SingleInstance<XInputController>.Instance.StickMoved += Controller_StickMoved;
		}
		#endregion
		#endregion
	}
}