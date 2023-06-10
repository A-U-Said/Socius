using Socius.Models.Repositories;

namespace Socius.Repositories
{
	public interface IUserInteractionRepository : ISociusRepository<UserInteractionsSchema>
	{
		Task<UserInteractionsSchema?> GetMostRecentRecord(int profileId);
	}
}
