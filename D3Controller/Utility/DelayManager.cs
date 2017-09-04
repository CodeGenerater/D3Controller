using System;
using System.Collections.Generic;

namespace CodeGenerater.Diablo3.Controller
{
	public class DelayManager<TKey>
	{
		#region Constructor
		public DelayManager() : this(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(5), TimeSpan.FromMilliseconds(33), TimeSpan.FromMilliseconds(5))
		{

		}

		public DelayManager(TimeSpan LongDelay, TimeSpan LongDelayMax, TimeSpan ShortDelay, TimeSpan ShortDelayMax)
		{
			this.LongDelay = LongDelay;
			this.LongDelayMax = LongDelayMax;
			this.ShortDelay = ShortDelay;
			this.ShortDelayMax = ShortDelayMax;

			dictionary = new Dictionary<TKey, Delay>(Enum.GetValues(typeof(TKey)).Length);

			foreach (TKey each in Enum.GetValues(typeof(PadButtons)))
				dictionary.Add(each, new Delay());
		}
		#endregion

		#region Field
		Dictionary<TKey, Delay> dictionary;
		#endregion

		#region Property
		public TimeSpan LongDelay { set; get; }

		public TimeSpan LongDelayMax { set; get; }

		public TimeSpan ShortDelay { set; get; }

		public TimeSpan ShortDelayMax { set; get; }
		#endregion

		#region Method
		public bool CheckDelay(TKey Key)
		{
			var Time = dictionary[Key];
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