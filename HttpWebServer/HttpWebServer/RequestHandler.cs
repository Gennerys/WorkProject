using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class RequestHandler
	{
		private NetworkStream _clientStream;
		private IParser _requestParser;
		public RequestHandler(IParser requestParser, NetworkStream clientStream)
		{
			_requestParser = requestParser;
			_clientStream = clientStream;
		}
		public void HandleRequest()
		{
			NetworkStream requestMessage = _clientStream;
			RequestDTO requestDTO = _requestParser.Parse(requestMessage);

			if (requestDTO.Method.Equals("GET"))
			{
				var createResponse = new Response(_clientStream,new FileHandler(new MimeTypeResolver()),new StatusCodeResponse());
				createResponse.TransferToClient(requestDTO);
			}
			else if (requestDTO.Method.Equals("POST"))
			{
				Console.WriteLine("TODO");
			}
			else
			{
				Console.WriteLine("TODO");
			}

		}

		public bool CheckSession(Dictionary<string,string> cookies)
		{
			return true;
		}


	}
}
