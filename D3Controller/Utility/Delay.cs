using System;

namespace CodeGenerater.Diablo3.Controller
{
	internal class Delay
	{
		#region Constructor
		public Delay()
		{
			DelayLevel = DelayLevel.Long;
			Time = DateTime.Now;
		}
		#endregion

		#region Property
		public DelayLevel DelayLevel
		{
			set;
			get;
		}

		public DateTime Time
		{
			set;
			get;
		}
		#endregion
	}
}