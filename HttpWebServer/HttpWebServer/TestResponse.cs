using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class TestResponse
	{
		public void SendToBrowser(string Data)
		{

		}


		//public void SendToBrowser(NetworkStream network)
		//{
		//	//reader = new BinaryReader(_client.GetStream());
		//	int bufferChunk = 1024;
		//	byte[] buffer = CreateHeader(responseCode, bufferChunk, contentType);
		//	string test = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
		//	Console.WriteLine(test);
		//	NetworkStream network = _client.GetStream();
		//	//using (NetworkStream network = _client.GetStream())
		//	{

		//		//using (reader)
		//		{
		//			int byteCount;
		//			while ((byteCount = reader.Read(buffer, 0, buffer.Length)) != 0)
		//			{
		//				network.Write(buffer, 0, byteCount);
		//			}
		//		}

		//	}
		//}
	}
}
