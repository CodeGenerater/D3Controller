using System;
using System.Threading;
using System.Windows.Input;
using System.ComponentModel;
using k = CodeGenerater.Macro.HardwareControl.Keyboard;
using m = CodeGenerater.Macro.HardwareControl.Mouse;

namespace CodeGenerater.Diablo3.Macro
{
	public class MacroKey : INotifyPropertyChanged, IDisposable
	{
		#region Constructor
		public MacroKey()
		{
			_Key = System.Windows.Input.Key.None;
			T = new Thread(ThreadProc);
			T.Start();
		}
		#endregion

		#region Field
		dynamic _Key;

		MacroRule _Rule;

		int _Interval;

		Thread T;

		AutoResetEvent ResetEvent = new AutoResetEvent(false);

		bool _IsRun = false;

		bool DoOnce = true;
		#endregion

		#region Property
		public object Key
		{
			set
			{
				if (!(value is Key) && !(value is MouseButton))
					throw new ArgumentException(nameof(value));

				if ((_Key.GetType() == value.GetType()) && (_Key.ToString() == value.ToString()))
					return;

				_Key = value;

				Notify(nameof(Key));
			}
			get
			{
				return _Key;
			}
		}

		public int KeyCode => ToKeyCode(_Key);

		public MacroRule Rule
		{
			set
			{
				if (_Rule != value)
				{
					_Rule = value;
					Notify(nameof(Rule));
				}
			}
			get
			{
				return _Rule;
			}
		}

		public int Interval
		{
			set
			{
				if (_Interval != value)
				{
					_Interval = value;
					Notify(nameof(Interval));
				}
			}
			get
			{
				return _Interval;
			}
		}

		public bool IsRun
		{
			private set
			{
				if (_IsRun != value)
				{
					_IsRun = value;

					if (value)
					{
						DoOnce = true;
						ResetEvent.Set();
					}
					else
						ResetEvent.Reset();

					Notify(nameof(IsRun));
				}
			}
			get
			{
				return _IsRun;
			}
		}
		#endregion

		#region Method
		public void Run()
		{
			IsRun = true;
		}

		public void Stop()
		{
			IsRun = false;
		}
		#endregion

		#region Helper
		int ToKeyCode(MouseButton Button)
		{
			switch (Button)
			{
				case MouseButton.Left:
					return 0x01;
				case MouseButton.Middle:
					return 0x04;
				case MouseButton.Right:
					return 0x02;
				case MouseButton.XButton1:
					return 0x05;
				case MouseButton.XButton2:
					return 0x06;
				default:
					throw new ArgumentOutOfRangeException(nameof(Button));
			}
		}

		int ToKeyCode(Key Key)
		{
			return KeyInterop.VirtualKeyFromKey(Key);
		}

		void MacroOnce()
		{
			if (Key is MouseButton)
			{
				m.Click(_Key);
			}
			else
			{
				k.KeyDown(_Key);
				k.KeyUp(_Key);
			}
		}

		void ThreadProc()
		{
			while (true)
			{
				if (DoOnce && IsRun)
					ResetEvent.Set();

				ResetEvent.WaitOne();

				MacroOnce();

				if (Rule == MacroRule.Repeat)
					Thread.Sleep(Interval);
				else if (DoOnce)
					DoOnce = false;
			}
		}
		#endregion

		#region Interface Implement
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

		public void Dispose()
		{
			if (T != null)
			{
				IsRun = false;
				ResetEvent = null;
				T.Abort();
				T = null;
			}
		}
		#endregion
	}
}