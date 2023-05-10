using Socius.Models.Repositories;

namespace Socius.Repositories
{
	public interface ISociusProfileRepository : ISociusRepository<SociusProfilesSchema>
	{
		Task<ICollection<SociusProfilesSchema>> GetProfileList();
		Task<SociusProfilesSchema?> GetProfileDetails(int profileId);
	}
}
