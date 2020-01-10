using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerTestAttempt
{
	public class ClientAuthentication
	{
		private const string ServerCookie = "AlexCookie";
		private Dictionary<string,Guid> AuthenticatedUsers = new Dictionary<string, Guid>();
		public Request _request;
		public ClientAuthentication(Request request)
		{
			_request = request;
		}
		public bool CheckSession(string cookieHeader)
		{
			if (cookieHeader.Contains(ServerCookie))
			{
				return true;
			}

			return false;

		}

		public bool CheckUserCookies(Dictionary<string,string> cookies)
		{
			bool isThereOurCookie = false;
			foreach (var cookiePair in cookies)
			{
				if (AuthenticatedUsers.FirstOrDefault(x => x.Value.ToString() == cookiePair.Value).Key != null)
				{
					isThereOurCookie = true;
				}
			}

			return isThereOurCookie;
		}

		public void Authenticate()
		{

		}
	}
}
