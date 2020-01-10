using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public class ClientHandler : IDisposable
	{
		private NetworkStream _clientStream;
		public ClientHandler(TcpClient client)
		{
			_clientStream = client.GetStream();
		}
		public void Dispose()
		{
			_clientStream.Dispose();
		}

		public async Task ProcessAsync()
		{
			Console.WriteLine($"Current working thread: {Thread.CurrentThread.ManagedThreadId}");
			Request request = new Request();
			RequestDTO requestDto = request.Parse(await request.ReadRequest(_clientStream));
			Response response = new Response(_clientStream,new FileHandler(new MimeTypeResolver()),new StatusCodeResponse());
			await response.TransferToClient(requestDto);

		}


	}
}
