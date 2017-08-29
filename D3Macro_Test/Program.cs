using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using CodeGenerater.Diablo3.Macro;

namespace D3Macro_Test
{
	class Program
	{
		static void Main(string[] args)
		{
			MacroManager mm = new MacroManager();

			mm.AddMacro(new MacroKey { Interval = 200, Key = Key.A, Rule = MacroRule.Repeat });

			var key = mm.List.First();

			Console.WriteLine($"Key = {key.Key}, Interval = {key.Interval}");

			Console.WriteLine("Run after 3s");

			Thread.Sleep(3000);

			Console.WriteLine("Run");

			mm.Run();

			Thread.Sleep(5000);

			Console.WriteLine("Stop");

			mm.Stop();

			mm.Dispose();
		}
	}
}