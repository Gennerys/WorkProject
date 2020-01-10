using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public class Server
	{
		private TcpListener _server;
		private IConsoleLogger _consoleLogger;
		private bool _isRunning;
		List<Task> activeClientTasks = new List<Task>();

		public Server(IConsoleLogger consoleLogger)
		{
			_consoleLogger = consoleLogger;
		}

		public async Task RunServer(int port)
		{
			_server = new TcpListener(IPAddress.Any, port);
			_server.Start();
			_consoleLogger.WriteLogMessage("Server is running...");
			_isRunning = true;
			while (_isRunning)
			{
				var client = await _server.AcceptTcpClientAsync();
				await ProcessClient(client);
			}
			Task.WaitAll(activeClientTasks.ToArray());
		}

		public async Task ProcessClient(TcpClient client)
		{
			using (ClientHandler clientHandler = new ClientHandler(client))
			{
				Task clientTask = null;
				try
				{
					clientTask = clientHandler.ProcessAsync();
					activeClientTasks.Add(clientTask);
					await clientTask;
				}
				finally
				{
					if (clientTask != null)
					{
						activeClientTasks.Remove(clientTask);
					}
				}
			}
		}

	}
}
