using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Socius.Repositories
{
	public class UserInteractionRepository : SociusRepository<UserInteractionsSchema>, IUserInteractionRepository
	{
		private readonly IScopeProvider _scopeProvider;
		private const string _dbName = "UserInteractions";
		public UserInteractionRepository(IScopeProvider scopeProvider)
			: base(scopeProvider, _dbName)
		{
			_scopeProvider = scopeProvider;
		}

		public async Task<UserInteractionsSchema?> GetMostRecentRecord(int profileId)
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults = await scope.Database.SingleAsync<UserInteractionsSchema>
				($@"SELECT 
					TOP 1 * FROM {_dbName}
					WHERE ProfileId = {profileId}
					ORDER BY Date DESC");
			scope.Complete();

			return queryResults;
		}
	}
}
