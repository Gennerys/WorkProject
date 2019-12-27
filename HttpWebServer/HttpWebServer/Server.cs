using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class Server
	{
		private TcpListener _server;
		private IConsoleLogger _consoleLogger;
		private bool _isRunning;
		public Server(int port,IConsoleLogger consoleLogger)
		{
			_consoleLogger = consoleLogger;
			try
			{
				_server = new TcpListener(IPAddress.Any, port);
				_server.Start();
				_consoleLogger.WriteLogMessage("Server is running...");
				_isRunning = true;
			}
			catch (Exception e)
			{
				_consoleLogger.WriteLogMessage($"An Exception Occurred while Listening : {e}");
			}
		}

		public void ClientConnection()
		{
			try
			{
				while (_isRunning)
				{
					TcpClient client = _server.AcceptTcpClient();
					RequestHandler requestHandler = new RequestHandler(new RequestParser(), client.GetStream());
					if (client.Connected)
					{
						var clientThread = Task.Factory.StartNew(() =>
						{
							_consoleLogger.WriteLogMessage($"Client connected...\nClient IP {client.Client.RemoteEndPoint}");
							requestHandler.HandleRequest();
							_consoleLogger.WriteLogMessage($"Current thread ID :{Thread.CurrentThread.ManagedThreadId}");

						});
						if (clientThread.IsCompleted)
						{
							client.Close();
						}
					}

				}
			}
			catch (SocketException e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				_server.Stop();
			}
		}
	}
}
