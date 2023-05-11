using Socius.Models.Repositories;

namespace Socius.Dto.Views.Profiles
{
	public class InstagramCredentialsView
	{
		public InstagramCredentialsView(InstagramCredentialsSchema dbResult)
		{
			ClientId = dbResult?.IgClientId;
			ClientSecret = dbResult?.IgClientSecret;
			RedirectUri = dbResult?.IgRedirectUri;
			Token = dbResult?.IgToken;
			TokenExpiry = dbResult?.IgTokenExpiry;
		}

		public long? ClientId { get; protected set; }
		public string? ClientSecret { get; protected set; }
		public string? RedirectUri { get; protected set; }
		public string? Token { get; protected set; }
		public DateTime? TokenExpiry { get; protected set; }
	}
}
