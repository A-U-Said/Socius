using Socius.Models.ApiResponses;
using Socius.Models.Repositories;

namespace Socius.Helpers
{
	public interface IInstagramHelper
	{
		bool IsCompleteProfile(InstagramCredentialsSchema storedCredentials);
		Task<InstagramLongTokenResponse?> GetToken(int sociusProfileId, string igAuthCode);
		Task<TaskStatus> RefreshInstagramToken(int sociusProfileId);
	}
}
