namespace HttpWebServer
{
	public interface IStatusCodeResponse
	{
		string GetResponse(StatusCode statusCode);
	}
}