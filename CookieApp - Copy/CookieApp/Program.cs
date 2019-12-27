using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CookieApp
{
	class Program
	{
		static void Main(string[] args)
		{

			var server = new TcpListener(IPAddress.Loopback, 6666);
			server.Start();
			RequestParser requestParser = new RequestParser();
			while (true)
			{
				using (var client = server.AcceptTcpClient())
				{
					using (NetworkStream stream = client.GetStream())
					{
						requestParser.HandleRequest(stream);
						
					}
					client.Close();
				}
			}
		}
	}
}
