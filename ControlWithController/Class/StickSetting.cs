using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.Serialization;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	[Serializable]
	public class StickSetting : INotifyPropertyChanged, ISerializable
	{
		#region Constructor
		public StickSetting()
		{
			MoveRange = new Range<int>() { From = 0, To = 50 };
			AroundRange = new Range<int>() { From = 0, To = 200 };
		}
		#endregion

		#region Field
		Direction _Stick;

		StickBindingRule _StickBindingRule;

		Range<int> _MoveRange;

		Range<int> _AroundRange;

		Point _Offset;
		#endregion

		#region Property
		[SerializationTarget]
		public Direction Stick
		{
			set
			{
				if (_Stick != value)
				{
					_Stick = value;
					Notify(nameof(Stick));
				}
			}
			get
			{
				return _Stick;
			}
		}

		[SerializationTarget]
		public StickBindingRule StickBindingRule
		{
			set
			{
				if (_StickBindingRule != value)
				{
					_StickBindingRule = value;
					Notify(nameof(StickBindingRule));
				}
			}
			get
			{
				return _StickBindingRule;
			}
		}

		[SerializationTarget]
		public Range<int> MoveRange
		{
			set
			{
				if (_MoveRange != value)
				{
					_MoveRange = value;
					Notify(nameof(MoveRange));
				}
			}
			get
			{
				return _MoveRange;
			}
		}

		[SerializationTarget]
		public Range<int> AroundRange
		{
			set
			{
				if (_AroundRange != value)
				{
					_AroundRange = value;
					Notify(nameof(AroundRange));
				}
			}
			get
			{
				return _AroundRange;
			}
		}

		[SerializationTarget]
		public Point Offset
		{
			set
			{
				if (_Offset != value)
				{
					_Offset = value;
					Notify(nameof(Offset));
				}
			}
			get
			{
				return _Offset;
			}
		}
		#endregion

		#region Interface Implement
		#region INotifyPropertyChaged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
		#region ISerializable
		public void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		StickSetting(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		#endregion
		#endregion
	}
}