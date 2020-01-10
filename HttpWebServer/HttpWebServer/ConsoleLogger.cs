using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public class ConsoleLogger : IConsoleLogger
	{
		public void WriteLogMessage(string message)
		{
			Console.WriteLine(message);
		}
	}
}
