using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CookieApp
{
	public class RequestParser
	{
		Response response = new Response();
		public Dictionary<string,string> Parse(Stream network)
		{

			Dictionary<string, string> headers = new Dictionary<string, string>();
			string message = string.Empty;
			StreamReader reader = new StreamReader(network);

			while (reader.Peek() != -1)
			{
				message = message + reader.ReadLine() + "\r\n";
			}

			Console.WriteLine(message);
			string[] headersParts = message.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(x => !string.IsNullOrEmpty(x)).ToArray();

			foreach (var line in headersParts)
			{
				if (line.Contains(": "))
				{
					string[] headerKeyValue = line.Split(new string[] { ": " }, StringSplitOptions.None);
					if (headerKeyValue.Length != 2)
					{
						Console.WriteLine("exception");
					}
					else
					{
						headers.Add(headerKeyValue[0], headerKeyValue[1]);
					}
				}
			}

			string[] requestLine = headersParts[0].Split(' ');
			headers.Add("Method", requestLine[0]);
			headers.Add("URL", requestLine[1]);
			headers.Add("Version", requestLine[2]);



			return headers;
		}
		public void HandleRequest(NetworkStream _clientStream)
		{
			NetworkStream requestMessage = _clientStream;
			Dictionary<string,string> requestDTO = Parse(requestMessage);
			if (requestDTO.FirstOrDefault(x => x.Key == "Method").Value.Equals("GET"))
			{
				response.WriteGetResponse(_clientStream);
				Console.WriteLine("GET");
			}
			else if (requestDTO.FirstOrDefault(x => x.Key == "Method").Value.Equals("POST"))
			{
				response.WritePostResponse(_clientStream);
				Console.WriteLine("POST");
			}
			else
			{
				Console.WriteLine("TODO");
			}

		}
	}
}
