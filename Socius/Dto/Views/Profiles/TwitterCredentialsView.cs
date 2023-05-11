using Socius.Models.Repositories;

namespace Socius.Dto.Views.Profiles
{
	public class TwitterCredentialsView
	{
		public TwitterCredentialsView(TwitterCredentialsSchema dbResult) 
		{
			UserID = dbResult?.TwUserID;
			Token = dbResult?.TwToken;
		}

		public long? UserID { get; protected set; }
		public string? Token { get; protected set; }
	}
}
