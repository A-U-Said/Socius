using Socius.Dto.Commands;

namespace Socius.Helpers
{
	public interface ISociusProfilesHelper
	{
		Task<string> SetProfileImage(int profileId, IList<IFormFile> file);
		Task UpdateProfile(int profileId, SaveProfileCommand profile);
	}
}
