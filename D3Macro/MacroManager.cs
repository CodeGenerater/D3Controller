using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeGenerater.Diablo3.Macro
{
	public class MacroManager : IDisposable, INotifyPropertyChanged
	{
		#region Field
		ObservableCollection<MacroKey> MacroList = new ObservableCollection<MacroKey>();

		bool _NowRun;
		#endregion
		
		#region Property
		public IEnumerable<MacroKey> List => MacroList;

		public bool NowRun
		{
			private set
			{
				if (_NowRun != value)
				{
					_NowRun = value;
					Notify(nameof(NowRun));
				}
			}
			get
			{
				return _NowRun;
			}
		}
		#endregion

		#region Method
		public void AddMacro(MacroKey Macro)
		{
			CheckRun();

			MacroList.Add(Macro);
		}

		public void RemoveMacro(MacroKey Macro)
		{
			CheckRun();

			Macro.Dispose();
			MacroList.Remove(Macro);
		}

		public void RemoveMacro(int Index)
		{
			CheckRun();

			MacroList[Index].Dispose();
			MacroList.RemoveAt(Index);
		}

		public void Run()
		{
			foreach (var each in MacroList)
				each.Run();

			NowRun = true;
		}

		public void Stop()
		{
			foreach (var each in MacroList)
				each.Stop();

			NowRun = false;
		}
		#endregion

		#region Helper
		public void CheckRun()
		{
			if (NowRun) throw new Exception();
		}
		#endregion

		#region Interface Implement
		public void Dispose()
		{
			Stop();

			foreach (var each in MacroList)
				each.Dispose();

			MacroList.Clear();
			MacroList = null;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
	}
}