namespace HttpWebServer
{
	public interface IMimeTypeResolver
	{
		string GetMIMEType(string fileName);
	}
}