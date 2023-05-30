using Socius.Helpers;
using Socius.Repositories;
using Umbraco.Cms.Core.Composing;

namespace Socius.DI
{
	public class RepositoryComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.Services.AddSingleton<ISociusProfileRepository, SociusProfileRepository>();
			builder.Services.AddSingleton<IFacebookCredentialsRepository, FacebookCredentialsRepository>();
			builder.Services.AddSingleton<IInstagramCredentialsRepository, InstagramCredentialsRepository>();
			builder.Services.AddSingleton<ITwitterCredentialsRepository, TwitterCredentialsRepository>();

			builder.Services.AddSingleton<ISociusProfilesHelper, SociusProfilesHelper>();
			builder.Services.AddSingleton<IInstagramHelper, InstagramHelper>();
			builder.Services.AddSingleton<IFacebookHelper, FacebookHelper>();
			builder.Services.AddSingleton<ISociusFeedHelper, SociusFeedHelper>();
		}
	}
}
