using System;

namespace CodeGenerater.Diablo3.Controller
{
	public class StickEventArgs : EventArgs
	{
		#region Constructor
		public StickEventArgs(Direction Stick, short X, short Y)
		{
			this.Stick = Stick;
			this.X = X;
			this.Y = Y;
		}
		#endregion

		#region Property
		public Direction Stick { private set; get; }

		public short X { private set; get; }

		public short Y { private set; get; }
		#endregion
	}
}