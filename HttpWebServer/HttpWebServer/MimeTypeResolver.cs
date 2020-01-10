using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	class MimeTypeResolver : IMimeTypeResolver
	{
		private readonly Dictionary<string, string> _mimeTypesDictionary = new Dictionary<string, string>
		{
			{"css", "text/css"},
			{"gif", "image/gif"},
			{"htm", "text/html"},
			{"html", "text/html"},
			{"jpe", "image/jpeg"},
			{"jpeg", "image/jpeg"},
			{"jpg", "image/jpeg"},
			{"js", "application/x-javascript"},
			{"png", "image/png"},
		};
		public string GetMIMEType(string fileName)
		{
			//get file extension
			string extension = Path.GetExtension(fileName).ToLowerInvariant();

			if (extension.Length > 0 &&
			    _mimeTypesDictionary.ContainsKey(extension.Remove(0, 1)))
			{
				return _mimeTypesDictionary[extension.Remove(0, 1)];
			}
			else
			{
				return "unknown/unknown";
			}
		}
	}
}
