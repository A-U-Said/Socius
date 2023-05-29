using Socius.Models.Repositories;
using Socius.Dto.Commands;

namespace Socius.Helpers
{
	public interface IFacebookHelper
	{
		Task<FacebookCredentialsSchema?> UserTokenToPageToken(int sociusProfileId, FacebookUserTokenExchangeCommand command);
	}
}
