using Socius.Models.ApiResponses.Instagram;

namespace Socius.Dto.Views.Feeds
{
	public class SociusFeedView
	{
		public SociusFeedView()
		{
			FacebookPosts = new List<SocialMediaPostView>();
			InstagramPosts = new List<SocialMediaPostView>();
			TwitterPosts = new List<SocialMediaPostView>();
		}

		public List<SocialMediaPostView> FacebookPosts { get; set; }
		public List<SocialMediaPostView> InstagramPosts { get; set; }
		public List<SocialMediaPostView> TwitterPosts { get; set; }
	}
}
