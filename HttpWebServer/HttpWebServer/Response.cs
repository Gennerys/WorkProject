using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class Response
	{
		private NetworkStream _clientStream;
		private IFileHandler _fileHandler;
		private IStatusCodeResponse _statusCodeResponse;
		public Response(NetworkStream clientStream, IFileHandler fileHandler, IStatusCodeResponse statusCodeResponse)
		{
			_clientStream = clientStream;
			_fileHandler = fileHandler;
			_statusCodeResponse = statusCodeResponse;
		}
		public void TransferToClient(RequestDTO requestDto)
		{
			SendToBrowser(requestDto,_clientStream);

		}

		public void SendToBrowser(RequestDTO requestDto, NetworkStream network)
		{
			string physicalPath = _fileHandler.GetFilePath(requestDto.Url);
			string contentType = _fileHandler.GetFileMimeType(requestDto.Url);
			if (_fileHandler.CheckIfExists(physicalPath))
			{
				const int MAX_BUFFER = 1024;
				FileStream fileStream = _fileHandler.ReadFile(physicalPath);
				var reader = new BinaryReader(fileStream);
				var bufferBytes = new byte[MAX_BUFFER];
				int readedBytes;
				int totalBytes = 0;
				string response = String.Empty;
				SendHeader(requestDto.Version, contentType, totalBytes, _statusCodeResponse.GetResponse(StatusCode.OK), network);
				while ((readedBytes = reader.Read(bufferBytes, 0, MAX_BUFFER)) != 0)
				{
					SendToBrowser(bufferBytes, network);
					response = response + Encoding.ASCII.GetString(bufferBytes, 0, readedBytes);
					totalBytes += readedBytes;
				}
				reader.Close();
				fileStream.Close();
			}
			else
			{
				Console.WriteLine("Todo");
			}

		}
		public void SendToBrowser(byte[] sendData, NetworkStream network)
		{
			try
			{
				network.Write(sendData, 0, sendData.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error Occurred : {0} ", e);
			}
		}

		public void SendHeader(string httpVersion, string mimeHeader, int totalBytes, string statusCode,NetworkStream network)
		{
			var stringBuffer = "";

			// if Mime type is not provided set default to text/html  
			if (mimeHeader.Length == 0)
			{
				mimeHeader = "text/html";// Default Mime Type is text/html  
			}

			stringBuffer += httpVersion + " " + statusCode + "\r\n";
			stringBuffer += "Server: Alex Papirnyk\r\n";
			stringBuffer += "Content-Type: " + mimeHeader + "\r\n";
			stringBuffer += "Accept-Ranges: bytes\r\n";
			stringBuffer += "Content-Length: " + totalBytes + "\r\n\r\n";

			var headerBuffer = Encoding.ASCII.GetBytes(stringBuffer);
			Console.WriteLine("Total Bytes : " + totalBytes.ToString());
			SendToBrowser(headerBuffer, _clientStream);
		}


	}
}
