using Socius.Models.Repositories;

namespace Socius.Dto.Views.Profiles
{
	public class FacebookCredentialsView
	{
		public FacebookCredentialsView(FacebookCredentialsSchema dbResult) 
		{
			AppId = dbResult?.AppId;
			ClientSecret = dbResult?.ClientSecret;
			PageID = dbResult?.PageID;
			Token = dbResult?.Token;
		}

		public long? AppId { get; protected set; }
		public string? ClientSecret { get; protected set; }
		public long? PageID { get; protected set; }
		public string? Token { get; protected set; }
	}
}
