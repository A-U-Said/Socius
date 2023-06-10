using Socius.Dto.Views.UserInteraction;
using Socius.Models.Shared;

namespace Socius.Helpers
{
	public interface IUserInteractionHelper
	{
		Task IncrementClickCount(int sociusProfileId, SocialMediaFeedType feedType);
		Task<UserInteractionArrayView?> GetByTimescale(int profileId, TimescaleType timescale);
	}
}
