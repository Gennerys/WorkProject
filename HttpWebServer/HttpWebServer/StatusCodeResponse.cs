using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public class StatusCodeResponse : IStatusCodeResponse
	{
		public string GetResponse(StatusCode statusCode)
		{
			string responseCode = String.Empty;
			switch (statusCode)
			{
				case StatusCode.OK:
					responseCode = $"{(int)statusCode} OK";
					break;
				case StatusCode.BAD_REQUEST:
					responseCode = $"{(int)statusCode} Bad Request";
					break;
				case StatusCode.UNAUTHORIZED:
					responseCode = $"{(int)statusCode} Unauthorized";
					break;
				case StatusCode.NOT_FOUND:
					responseCode = $"{(int)statusCode} Not Found";
					break;
				case StatusCode.NOT_IMPLEMENTED:
					responseCode = $"{(int)statusCode} Not Implemented";
					break;
				default:
					responseCode = $"Unknown case";
					break;
			}
			return responseCode;
		}
	}
}
