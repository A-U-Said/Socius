using Socius.Dto.Views.Feeds;

namespace Socius.Helpers
{
	public interface ISociusFeedHelper
	{
		SociusFeedView GetSociusFeeds(int profileId);
	}
}
