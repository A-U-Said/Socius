using Socius.Dto.Commands;
using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Socius.Repositories
{
	public class FacebookCredentialsRepository : SociusRepository<FacebookCredentialsSchema, UpdateFacebookCredentialsCommand>, IFacebookCredentialsRepository
	{
		public FacebookCredentialsRepository(IScopeProvider scopeProvider) 
			: base(scopeProvider, "FacebookCredentials", "ProfileId")
		{
		}
	}

	public class InstagramCredentialsRepository : SociusRepository<InstagramCredentialsSchema>, IInstagramCredentialsRepository
	{
		public InstagramCredentialsRepository(IScopeProvider scopeProvider)
			: base(scopeProvider, "InstagramCredentials", "ProfileId")
		{
		}
	}

	public class TwitterCredentialsRepository : SociusRepository<TwitterCredentialsSchema>, ITwitterCredentialsRepository
	{
		public TwitterCredentialsRepository(IScopeProvider scopeProvider)
			: base(scopeProvider, "TwitterCredentials", "ProfileId")
		{
		}
	}
}
