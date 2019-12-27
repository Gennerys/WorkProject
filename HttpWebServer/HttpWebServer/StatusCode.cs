using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public enum StatusCode
	{
		OK = 200,
		BAD_REQUEST = 400,
		UNAUTHORIZED = 401,
		NOT_FOUND = 404,
		NOT_IMPLEMENTED = 501
	}
}
