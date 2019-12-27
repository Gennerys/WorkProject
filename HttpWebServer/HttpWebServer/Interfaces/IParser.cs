using System.IO;

namespace HttpWebServer
{
	public interface IParser
	{
		RequestDTO Parse(Stream stream);
	}
}