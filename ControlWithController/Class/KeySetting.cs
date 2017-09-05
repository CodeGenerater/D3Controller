using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.Serialization;
using CodeGenerater.Diablo3.Controller;

namespace CodeGenerater.Diablo3.ControlWithController
{
	[Serializable]
	public class KeySetting : INotifyPropertyChanged, ISerializable
	{
		#region Constructor
		public KeySetting()
		{

		}
		#endregion

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
		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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

		[SerializationTarget]
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
		#region ISerializable
		public void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		KeySetting(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		#endregion
		#endregion
	}
}