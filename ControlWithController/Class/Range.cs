using System;
using System.ComponentModel;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public class Range<T> : INotifyPropertyChanged where T : IComparable<T>
	{
		#region Field
		T _From;

		T _To;
		#endregion

		#region Property
		public T From
		{
			set
			{
				if (value.CompareTo(To) > 0)
					throw new ArgumentOutOfRangeException(nameof(value));

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

		public T To
		{
			set
			{
				if (value.CompareTo(From) < 0)
					throw new ArgumentOutOfRangeException(nameof(value));

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
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
	}
}