using System;
using System.Collections.Generic;
using CodeGenerater.Diablo3.Macro;
using CodeGenerater.Diablo3.Controller;
using m = CodeGenerater.Macro.HardwareControl.Mouse;
using k = CodeGenerater.Macro.HardwareControl.Keyboard;

namespace CodeGenerater.Diablo3.ControlWithController
{
	class BindingSettingManager : IDisposable
	{
		#region Constructor
		public BindingSettingManager()
		{
			KeySettingDict = new Dictionary<PadButtons, KeySetting>(Enum.GetValues(typeof(PadButtons)).Length);

			SingleInstance<XInputController>.Instance.ButtonPressed += Controller_ButtonPressed;
		}
		#endregion

		#region Field
		Dictionary<PadButtons, KeySetting> KeySettingDict;

		bool IsDisposed;
		#endregion

		#region Property
		public KeySetting this[PadButtons B]
		{
			get
			{
				if (!KeySettingDict.ContainsKey(B))
					KeySettingDict.Add(B, new KeySetting());

				return KeySettingDict[B];
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
							m.MoveRelative(Setting.MoveX, Setting.MoveY);
					}
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