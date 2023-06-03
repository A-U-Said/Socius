using Socius.Models.Shared;


namespace Socius.Helpers
{
	public interface IUserInteractionHelper
	{
		Task IncrementClickCount(int sociusProfileId, SocialMediaFeedType feedType);
	}
}
