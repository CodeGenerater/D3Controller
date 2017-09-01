using System;

namespace CodeGenerater.Diablo3.Controller
{
	public class ButtonEventArgs : EventArgs
	{
		#region Constructor
		public ButtonEventArgs(PadButtons Button)
		{
			this.Button = Button;
		}
		#endregion

		#region Property
		public PadButtons Button { private set; get; }
		#endregion
	}
}