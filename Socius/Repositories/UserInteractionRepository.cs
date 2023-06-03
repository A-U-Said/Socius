using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Socius.Repositories
{
	public class UserInteractionRepository : SociusRepository<UserInteractionsSchema>, IUserInteractionRepository
	{
		public UserInteractionRepository(IScopeProvider scopeProvider)
			: base(scopeProvider, "UserInteractions", "ProfileId")
		{
		}
	}
}
