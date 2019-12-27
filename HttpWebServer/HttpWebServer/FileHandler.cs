using System.IO;

namespace HttpWebServer
{
	public class FileHandler : IFileHandler
	{
		IMimeTypeResolver _mimeType;

		public FileHandler(IMimeTypeResolver mimeType)
		{
			_mimeType = mimeType;
		}

		public FileStream ReadFile(string filePath)
		{
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			return fileStream;
		}

		public string GetFilePath(string fileName)
		{
			var baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (GetFileMimeType(fileName) == "text/html")
			{
				baseDirectory = $"{baseDirectory}{fileName}";
			}
			else if (GetFileMimeType(fileName) == "image/jpeg")
			{
				baseDirectory = $"{baseDirectory}{fileName}";
			}
			else
			{
				baseDirectory = $"{baseDirectory}index.html"; // Add here status msg or something
			}

			return baseDirectory;
		}

		public bool CheckIfExists(string filePath)
		{
			if (File.Exists(filePath))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public string GetFileMimeType(string fileName)
		{
			return _mimeType.GetMIMEType(fileName);
		}
	}
}
