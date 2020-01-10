using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public interface IFileHandler
	{
		bool CheckIfExists(string filePath);
		string GetFileMimeType(string fileName);
		string GetFilePath(string fileName);
		FileStream ReadFile(string filePath);
	}
}
