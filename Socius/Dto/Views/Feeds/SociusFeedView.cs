using Socius.Models.ApiResponses.Instagram;

namespace Socius.Dto.Views.Feeds
{
	public class SociusFeedView
	{
		public SociusFeedView()
		{
			FacebookPosts = new List<FacebookPostView>();
			InstagramPosts = new List<InstagramPostView>();
			TwitterPosts = new List<TwitterPostView>();
		}

		public List<FacebookPostView> FacebookPosts { get; set; }
		public List<InstagramPostView> InstagramPosts { get; set; }
		public List<TwitterPostView> TwitterPosts { get; set; }
	}
}
