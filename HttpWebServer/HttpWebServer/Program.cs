using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var server = new Server(1234, new ConsoleLogger());
			server.ClientConnection();
			

		}
	}
}
