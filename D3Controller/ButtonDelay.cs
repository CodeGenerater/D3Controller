using System;
using System.Collections.Generic;

namespace CodeGenerater.Diablo3.Controller
{
	public class ButtonDelay
	{
		#region Inner Type
		class ButtonTime
		{
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

		enum DelayLevel
		{
			Long,
			Short,
		}
		#endregion

		#region Constructor
		public ButtonDelay() : this(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(5), TimeSpan.FromMilliseconds(33), TimeSpan.FromMilliseconds(5))
		{
			
		}

		public ButtonDelay(TimeSpan LongDelay, TimeSpan LongDelayMax, TimeSpan ShortDelay, TimeSpan ShortDelayMax)
		{
			this.LongDelay = LongDelay;
			this.LongDelayMax = LongDelayMax;
			this.ShortDelay = ShortDelay;
			this.ShortDelayMax = ShortDelayMax;

			dictionary = new Dictionary<PadButtons, ButtonTime>(Enum.GetValues(typeof(PadButtons)).Length);

			foreach (PadButtons each in Enum.GetValues(typeof(PadButtons)))
				dictionary.Add(each, new ButtonTime() { DelayLevel = DelayLevel.Long, Time = DateTime.Now });
		}
		#endregion

		#region Field
		Dictionary<PadButtons, ButtonTime> dictionary;
		#endregion

		#region Property
		public TimeSpan LongDelay { set; get; }

		public TimeSpan LongDelayMax { set; get; }

		public TimeSpan ShortDelay { set; get; }

		public TimeSpan ShortDelayMax { set; get; }
		#endregion

		#region Method
		/// <returns>False라면 딜레이가 적용되야함을 나타냅니다.</returns>
		public bool CheckDelay(PadButtons Button)
		{
			var Time = dictionary[Button];
			TimeSpan ElipsedTime = DateTime.Now - Time.Time;

			switch (Time.DelayLevel)
			{
				case DelayLevel.Long:
					if (ElipsedTime < LongDelay)
						return false;
					else if (ElipsedTime > LongDelay || ElipsedTime < LongDelay + LongDelayMax)
					{
						Time.Time = DateTime.Now;
						Time.DelayLevel = DelayLevel.Short;
						return true;
					}
					else
					{
						Time.Time = DateTime.Now;
						return true;
					}
				case DelayLevel.Short:
					if (ElipsedTime < ShortDelay)
						return false;
					else if (ElipsedTime > ShortDelay || ElipsedTime < ShortDelay + ShortDelayMax)
					{
						Time.Time = DateTime.Now;
						return true;
					}
					else
					{
						Time.Time = DateTime.Now;
						Time.DelayLevel = DelayLevel.Long;
						return true;
					}
			}

			return true;
		}
		#endregion
	}
}