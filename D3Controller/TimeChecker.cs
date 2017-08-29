using System;
using System.Collections.Generic;

namespace CodeGenerater.Diablo3.Controller
{
	public class TimeChecker
	{
		#region Field
		Dictionary<string, DateTime> Dict = new Dictionary<string, DateTime>();
		#endregion

		#region Method
		public bool CheckTime(string Key, int Millisecond)
		{
			if (Dict.ContainsKey(Key))
			{
				DateTime T = DateTime.Now;
				bool Result = (T - Dict[Key]).Milliseconds > Millisecond;
				if(Result) Dict[Key] = T;
				return Result;
			}
			else
			{
				Dict.Add(Key, DateTime.Now);
				return true;
			}
		}
		#endregion
	}
}