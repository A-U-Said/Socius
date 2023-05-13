using NPoco;
using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Persistence;

namespace Socius.Repositories
{
	public interface ISociusProfileRepository : ISociusRepository<SociusProfilesSchema>
	{
		Task<ICollection<SociusProfilesSchema>> GetProfileList();
		Task<SociusProfilesSchema?> GetProfileDetails(int profileId);
	}
}
