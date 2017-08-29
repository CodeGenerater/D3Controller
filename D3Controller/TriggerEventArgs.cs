using System;

namespace CodeGenerater.Diablo3.Controller
{
	public class TriggerEventArgs : EventArgs
	{
		#region Constructor
		public TriggerEventArgs(Direction Trigger, byte Value)
		{
			this.Trigger = Trigger;
			this.Value = Value;
		}
		#endregion

		#region Property
		public Direction Trigger { private set; get; }

		public byte Value { private set; get; }
		#endregion
	}
}