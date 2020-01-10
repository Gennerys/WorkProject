using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public class Request
	{
		public async Task<string> ReadRequest(NetworkStream network)
		{
			string requestMessage = string.Empty;

			StreamReader reader = new StreamReader(network);
			while (reader.Peek() != -1)
			{
				 requestMessage = requestMessage + await reader.ReadLineAsync() + "\r\n"; 
			}

			Console.WriteLine(requestMessage);
			return requestMessage;
		}
		public RequestDTO Parse(string requestMessage)
		{
			Dictionary<string, string> headers = new Dictionary<string, string>();
			RequestDTO requestDto = new RequestDTO();

			string[] headersParts = requestMessage.Split(new string[] { "\r\n" }, StringSplitOptions.None);

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
			requestDto.Method = requestLine[0];
			requestDto.Url = requestLine[1].Replace('/', '\\');
			requestDto.Version = requestLine[2];
			requestDto.Host = headers.FirstOrDefault(x => x.Key == "Host").Value;
			requestDto.UserAgent = headers.FirstOrDefault(x => x.Key == "User-Agent").Value;
			requestDto.Cookies = headers.FirstOrDefault(x => x.Key == "Cookie").Value;

			return requestDto;
		}
		public Dictionary<string, string> ParseCookie(string cookieHeader)
		{
			Dictionary<string, string> cookies = new Dictionary<string, string>();
			string[] cookieParts = cookieHeader.Trim().Split(';');
			foreach (var line in cookieParts)
			{
				string[] cookieNameValue = line.Split('=');
				cookies.Add(cookieNameValue[0], cookieNameValue[1]);
			}

			return cookies;
		}
	}
}
