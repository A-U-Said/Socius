using Socius.Models.ApiResponses.Facebook;
using Socius.Models.ApiResponses.Instagram;
using Socius.Models.ApiResponses.Twitter;

namespace Socius.Dto.Views.Feeds
{
	public class SociusFeedView
	{
		public SociusFeedView()
		{
			FacebookPosts = new List<FacebookPost>();
			InstagramPosts = new List<InstagramPost>();
			TwitterPosts = new List<TwitterPostView>();
		}

		public List<FacebookPost> FacebookPosts { get; set; }
		public List<InstagramPost> InstagramPosts { get; set; }
		public List<TwitterPostView> TwitterPosts { get; set; }
	}
}
