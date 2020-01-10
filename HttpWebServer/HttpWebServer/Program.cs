using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Server server = new Server(new ConsoleLogger());
			await server.RunServer(1234);

		}
	}
}
