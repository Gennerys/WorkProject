using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	class Response
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

		public async Task TransferToClient(RequestDTO requestDto)
		{
			if (requestDto.Url == "\\auth")
			{
				await SendLoginForm(requestDto);
			}
			else if (requestDto.Url == "\\auth\\challenge")
			{
				await SendAuthenticationResponse(requestDto);
			}
			else
			{
				await SendResponse(requestDto);
			}
		}

		public async Task SendError(string statusCode)
		{
			string html = "<html><body><h1>" + statusCode + "</h1></body></html>";
			string errorResponse = "HTTP/1.1 " + statusCode + "\nContent-type: text/html\nContent-Length:" + html.Length + "\n\n" + html;
			byte[] buffer = Encoding.ASCII.GetBytes(errorResponse);
			await SendToBrowser(buffer, _clientStream);
		}
		public async Task SendResponse(RequestDTO requestDto)
		{
			string physicalPath = _fileHandler.GetFilePath(requestDto.Url);
			string contentType = _fileHandler.GetFileMimeType(requestDto.Url);
			if (_fileHandler.CheckIfExists(physicalPath))
			{
				FileStream fileStream = _fileHandler.ReadFile(physicalPath);
				var reader = new BinaryReader(fileStream, Encoding.ASCII);
				int chunkSize = 1024;
				var buffer = new byte[chunkSize];
				int readedBytes;
				await SendHeader(requestDto.Version, contentType, Convert.ToInt32(fileStream.Length), _statusCodeResponse.GetResponse(StatusCode.OK));
				
				while ((readedBytes = reader.Read(buffer, 0, buffer.Length)) != 0)
				{
					 await _clientStream.WriteAsync(buffer, 0, readedBytes);
				}
				reader.Close();
				fileStream.Close();

			}
			else
			{
				await SendError(_statusCodeResponse.GetResponse(StatusCode.NOT_FOUND));
			}
		}

		public async Task SendAuthenticationResponse(RequestDTO requestDto)
		{
			var authChallengePage = new StringBuilder();
			authChallengePage.AppendLine("<!DOCTYPE html>");
			authChallengePage.AppendLine("<html>");
			authChallengePage.AppendLine("<head><title> Logged In! </title></head>");
			authChallengePage.AppendLine("<body>");
			authChallengePage.AppendLine("<h2> Logged In! </h2>");
			authChallengePage.AppendLine("</body>");
			authChallengePage.AppendLine("</html>");

			string authChallengeResponse = $"{requestDto.Version} " + _statusCodeResponse.GetResponse(StatusCode.OK) +
			                               "\nContent-type: text/html\nContent-Length:" + 
			                               authChallengePage.Length + $"\nSet-Cookie: {Guid.NewGuid()}" + "\n\n" + authChallengePage;
			Console.WriteLine(authChallengeResponse);
			byte[] authChallengeBuff = Encoding.ASCII.GetBytes(authChallengeResponse);
			await SendToBrowser(authChallengeBuff, _clientStream);
		}

		public async Task SendLoginForm(RequestDTO requestDto)
		{
			var loginForm = new StringBuilder();
			loginForm.AppendLine("<!DOCTYPE html>");
			loginForm.AppendLine("<html>");
			loginForm.AppendLine("<head><title> Login </title></head>");
			loginForm.AppendLine("<body>");
			loginForm.AppendLine("<h2> LOGIN </h2>");
			loginForm.AppendLine("<form method = \"post\" action = \"/auth/challenge\" >");
			loginForm.AppendLine("Username: <input type = \"text\" name = \"user\" size = \"25\"/><br/>");
			loginForm.AppendLine("Password: <input type =\"password\" name = \"pw\" size = \"10\"/><br/><br/>");
			loginForm.AppendLine("<input type = \"hidden\" name = \"action\" value = \"login\">");
			loginForm.AppendLine("<input type = \"submit\" value = \"SEND\"/>");
			loginForm.AppendLine("</form>");
			loginForm.AppendLine("</body>");
			loginForm.AppendLine("</html>");

			await SendHeader(requestDto.Version, "text/html", loginForm.Length, _statusCodeResponse.GetResponse(StatusCode.OK));
			var loginFormBuff = Encoding.ASCII.GetBytes(loginForm.ToString());
			await SendToBrowser(loginFormBuff,_clientStream);
		}

		public async Task SendHeader(string httpVersion, string mimeHeader, int totalBytes, string statusCode)
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
			await SendToBrowser(headerBuffer, _clientStream);
		}
		public async Task SendToBrowser(byte[] sendData, NetworkStream network)
		{
			try
			{
				await network.WriteAsync(sendData, 0, sendData.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error Occurred : {0} ", e);
			}
		}
	}
}
