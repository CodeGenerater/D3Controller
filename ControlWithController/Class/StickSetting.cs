using System.Windows;
using System.ComponentModel;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public class StickSetting : INotifyPropertyChanged
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
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
	}
}