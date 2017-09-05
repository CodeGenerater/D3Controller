using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CodeGenerater.Diablo3.ControlWithController
{
	[Serializable]
	public class Range<T> : INotifyPropertyChanged, ISerializable where T : IComparable<T>
	{
		#region Constructor
		public Range()
		{

		}
		#endregion

		#region Field
		T _From;

		T _To;
		#endregion

		#region Property
		[SerializationTarget]
		public T From
		{
			set
			{
				if (!_From.Equals(value))
				{
					_From = value;
					Notify(nameof(From));
				}
			}
			get
			{
				return _From;
			}
		}

		[SerializationTarget]
		public T To
		{
			set
			{
				if (!_To.Equals(value))
				{
					_To = value;
					Notify(nameof(To));
				}
			}
			get
			{
				return _To;
			}
		}
		#endregion

		#region Interface Implement
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
		#region ISerializable
		public void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		Range(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		#endregion
		#endregion
	}
}