using System;
using System.Threading;
using System.Windows.Input;
using k = CodeGenerater.Macro.HardwareControl.Keyboard;

namespace ControllerForImpale
{
	public class D3Helper : IDisposable
	{
		#region Constructor
		public D3Helper()
		{
			Run = false;
			T = new Thread(ThreadProc);
			T.Start();
		}
		#endregion

		#region Field
		Thread T;
		#endregion

		#region Property
		public bool Run
		{
			private set;
			get;
		}
		#endregion

		#region Method
		public void Start()
		{
			Run = true;
		}

		public void Stop()
		{
			Run = false;
		}
		#endregion

		#region Helper
		void ThreadProc()
		{
			bool Prev = false;

			while (true)
			{
				if (Run)
				{
					if (Prev == false)
					{
						k.KeyDown(Key.D2);
						k.KeyUp(Key.D2);
					}

					k.KeyDown(Key.D1);
					k.KeyUp(Key.D1);
					k.KeyDown(Key.D3);
					k.KeyUp(Key.D3);
					k.KeyDown(Key.D4);
					k.KeyUp(Key.D4);

					Thread.Sleep(200);
				}

				Prev = Run;
			}
		}
		#endregion

		#region Interface Implement
		#region IDisposable
		public void Dispose()
		{
			if (T != null)
			{
				T.Abort();
				T = null;
			}
		}
		#endregion
		#endregion
	}
}