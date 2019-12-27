using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CookieApp
{
	public class Response
	{
		public  void WriteGetResponse(NetworkStream stream)
		{
			using (var writer = new StreamWriter(stream, Encoding.ASCII))
			{

				var sb = new StringBuilder();
				sb.AppendLine("<!DOCTYPE html>");
				sb.AppendLine("<html>");
				sb.AppendLine("<head><title> Login </title></head>");
				sb.AppendLine("<body>");
				sb.AppendLine("<h2> LOGIN </h2>");
				sb.AppendLine("<form method = \"post\" action = \"/auth/challenge\">");
				sb.AppendLine("Username: <input type = \"text\" name = \"user\" size = \"25\"/><br/>");
				sb.AppendLine("Password: <input type =\"password\" name = \"pw\" size = \"10\"/><br/><br/>");
				sb.AppendLine("<input type = \"hidden\" name = \"action\" value = \"login\">");
				sb.AppendLine("<input type = \"submit\" value = \"SEND\"/>");
				sb.AppendLine("</form>");
				sb.AppendLine("</body>");
				sb.AppendLine("</html>");

				writer.WriteLine("HTTP/1.1 200 OK");
				writer.WriteLine("Server: nginx");
				writer.WriteLine("Content-Type: text/html");
				writer.WriteLine($"Content-Length: {sb.Length}");
				writer.WriteLine("Connection: close");
				writer.WriteLine();
				writer.WriteLine(sb.ToString());

				writer.Flush();
			}
		}

		public void WritePostResponse(NetworkStream stream)
		{
			using (var writer = new StreamWriter(stream, Encoding.ASCII))
			{

				var sb = new StringBuilder();
				sb.AppendLine("<!DOCTYPE html>");
				sb.AppendLine("<html>");
				sb.AppendLine("<head><title> Logged In! </title></head>");
				sb.AppendLine("<body>");
				sb.AppendLine("<h2> Logged In! </h2>");
				sb.AppendLine("</body>");
				sb.AppendLine("</html>");

				writer.WriteLine("HTTP/1.1 200 OK");
				writer.WriteLine("Server: nginx");
				writer.WriteLine("Content-Type: text/html");
				writer.WriteLine($"Content-Length: {sb.Length}");
				writer.WriteLine("Connection: close");
				writer.WriteLine("Set-Cookie: sessionId=1111");
				writer.WriteLine();
				writer.WriteLine("user=test");
				writer.WriteLine("pw=1234");
				writer.WriteLine(sb.ToString());

				writer.Flush();
			}
		}
	}
}
