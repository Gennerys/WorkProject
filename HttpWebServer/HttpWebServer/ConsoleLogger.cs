using System;

namespace HttpWebServer
{
	public class ConsoleLogger : IConsoleLogger
	{
		public void WriteLogMessage(string message)
		{
			Console.WriteLine(message);
		}
	}
}
