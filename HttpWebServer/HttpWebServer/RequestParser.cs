using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class RequestParser : IParser
	{
		public RequestDTO Parse(Stream network)
		{

			Dictionary<string, string> headers = new Dictionary<string, string>();
			RequestDTO requestDto = new RequestDTO();
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
				else
				{
					string[] requestLine = line.Split(' ');
					requestDto.Method = requestLine[0];
					requestDto.Url = requestLine[1].Replace('/','\\');
					requestDto.Version = requestLine[2];
				}
			}

			requestDto.Host = headers.FirstOrDefault(x => x.Key == "Host").Value;
			requestDto.UserAgent = headers.FirstOrDefault(x => x.Key == "User-Agent").Value;
			requestDto.ContentLength = message.Length;

			return requestDto;
		}

	}
}
