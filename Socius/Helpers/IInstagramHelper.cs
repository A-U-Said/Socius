using Socius.Models.Repositories;
using Socius.Models.ApiResponses.Instagram;

namespace Socius.Helpers
{
    public interface IInstagramHelper
	{
		bool IsCompleteProfile(InstagramCredentialsSchema storedCredentials);
		Task<InstagramLongTokenResponse?> GetToken(int sociusProfileId, string igAuthCode);
		Task<TaskStatus> RefreshInstagramToken(int sociusProfileId);
	}
}
