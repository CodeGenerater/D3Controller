using System.Windows.Input;
using System.ComponentModel;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	public class KeySetting : INotifyPropertyChanged
	{
		#region Field
		PadButtons _Button;

		bool _BindToKeyboard;

		Key _Key;

		bool _BindToMacro;

		MacroBindingRule _MacroBindingRule;

		bool _BindToMouse;

		MouseButton? _MouseButton;

		int _MoveX;

		int _MoveY;
		#endregion

		#region Property
		public PadButtons Button
		{
			set
			{
				if (_Button != value)
				{
					_Button = value;
					Notify(nameof(Button));
				}
			}
			get
			{
				return _Button;
			}
		}

		public bool BindToKeyboard
		{
			set
			{
				if (_BindToKeyboard != value)
				{
					_BindToKeyboard = value;
					Notify(nameof(BindToKeyboard));
				}
			}
			get
			{
				return _BindToKeyboard;
			}
		}

		public Key Key
		{
			set
			{
				if (_Key != value)
				{
					_Key = value;
					Notify(nameof(Key));
				}
			}
			get
			{
				return _Key;
			}
		}

		public bool BindToMacro
		{
			set
			{
				if (_BindToMacro != value)
				{
					_BindToMacro = value;
					Notify(nameof(BindToMacro));
				}
			}
			get
			{
				return _BindToMacro;
			}
		}

		public MacroBindingRule MacroBindingRule
		{
			set
			{
				if (_MacroBindingRule != value)
				{
					_MacroBindingRule = value;
					Notify(nameof(MacroBindingRule));
				}
			}
			get
			{
				return _MacroBindingRule;
			}
		}

		public bool BindToMouse
		{
			set
			{
				if (_BindToMouse != value)
				{
					_BindToMouse = value;
					Notify(nameof(BindToMouse));
				}
			}
			get
			{
				return _BindToMouse;
			}
		}

		public MouseButton? MouseButton
		{
			set
			{
				if (_MouseButton != value)
				{
					_MouseButton = value;
					Notify(nameof(MouseButton));
				}
			}
			get
			{
				return _MouseButton;
			}
		}

		public int MoveX
		{
			set
			{
				if (_MoveX != value)
				{
					_MoveX = value;
					Notify(nameof(MoveX));
				}
			}
			get
			{
				return _MoveX;
			}
		}

		public int MoveY
		{
			set
			{
				if (_MoveY != value)
				{
					_MoveY = value;
					Notify(nameof(MoveY));
				}
			}
			get
			{
				return _MoveY;
			}
		}
		#endregion

		#region Interface Implement
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		#endregion
		#endregion
	}
}