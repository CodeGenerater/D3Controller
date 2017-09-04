using System;
using CodeGenerater.WCFLogger.Client;

namespace CodeGenerater.Diablo3.ControlWithController
{
	static class Logger
	{
		#region Field
		static LogClient Client;
		#endregion

		#region Method
		public static void Init()
		{
			Client = new LogClient("ControlWithController");
		}

		public static void Write(string Message)
		{
			Client.Write($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}] {Message}");
		}
		#endregion
	}
}