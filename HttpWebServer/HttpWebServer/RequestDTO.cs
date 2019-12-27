using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
	public class RequestDTO
	{
		public string Method { get; set; }
		public string Version { get; set; }
		public string Url { get; set; }
		public string Host { get; set; }
		public string UserAgent { get; set; }
		public  int ContentLength { get; set; }
		public  string Cookies { get; set; }
		//public string Body { get; set; }
	}
}
