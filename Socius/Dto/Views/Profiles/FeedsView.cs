using Socius.Models.Repositories;

namespace Socius.Dto.Views.Profiles
{
    public class FeedsView
    {
        public FeedsView(FacebookCredentialsSchema facebook, InstagramCredentialsSchema instagram, TwitterCredentialsSchema twitter)
        {
            Facebook = new FacebookCredentialsView(facebook);
            Instagram = new InstagramCredentialsView(instagram);
            Twitter = new TwitterCredentialsView(twitter);
        }

        public FacebookCredentialsView Facebook { get; protected set; }
		public InstagramCredentialsView Instagram { get; protected set; }
		public TwitterCredentialsView Twitter { get; protected set; }
	}
}
