using Socius.Dto.Commands;
using Socius.Models.Repositories;

namespace Socius.Helpers
{
	public interface ISociusProfilesHelper
	{
		Task<string?> SetProfileImage(int profileId, IList<IFormFile> file);
		Task UpdateProfile(int profileId, SaveProfileCommand profile);
		Task<SociusProfilesSchema> CreateProfile(SaveProfileCommand profile);
		Task<TaskStatus> DeleteProfile(int profileId);
	}
}
