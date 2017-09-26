using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace CodeGenerater.Diablo3.Macro
{
	[Serializable]
	public class MacroManager : IDisposable, INotifyPropertyChanged, ISerializable
	{
		#region Constructor
		public MacroManager()
		{

		}
		#endregion

		#region Field
		[SerializationTarget]
		ObservableCollection<MacroKey> MacroList = new ObservableCollection<MacroKey>();

		bool _NowRun;
		#endregion
		
		#region Property
		public ObservableCollection<MacroKey> List => MacroList;

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
		#region IDisposable
		public void Dispose()
		{
			Stop();

			foreach (var each in MacroList)
				each.Dispose();

			MacroList.Clear();
			MacroList = null;
		}
		#endregion
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
		#region ISerializable
		public void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		MacroManager(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		#endregion
		#endregion
	}
}