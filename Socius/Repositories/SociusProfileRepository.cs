using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Socius.Repositories
{
	public class SociusProfileRepository : SociusRepository<SociusProfilesSchema>, ISociusProfileRepository
	{
		private readonly IScopeProvider _scopeProvider;
		private const string _dbName = "SociusProfiles";

		public SociusProfileRepository(IScopeProvider scopeProvider) 
			: base(scopeProvider, _dbName)
		{
			_scopeProvider = scopeProvider;
		}

		public async Task<ICollection<SociusProfilesSchema>> GetProfileList()
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults = await scope.Database.FetchAsync<SociusProfilesSchema>
				($@"SELECT 
					sociusProfile.Id,
					sociusProfile.Name,
					sociusProfile.ProfileImage,
					sociusProfile.CreatedBy,
					sociusProfile.CreateDate,
					sociusProfile.UpdatedBy,
					sociusProfile.UpdateDate,
					creator.userName as CreatedByName,
					updater.userName as UpdatedByName,
					fb.ProfileId,
					fb.FbAppId,
					fb.FbClientSecret,
					fb.FbPageId,
					fb.FbToken,
					ig.ProfileId,
					ig.IgClientId,
					ig.IgClientSecret,
					ig.IgRedirectUri,
					ig.IgToken,
					ig.IgTokenExpiry,
					tw.ProfileId,
					tw.TwUserId,
					tw.TwToken
				FROM {_dbName} sociusProfile
				INNER JOIN umbracoUser creator ON sociusProfile.CreatedBy = creator.id
				INNER JOIN umbracoUser updater ON sociusProfile.UpdatedBy = updater.id
				LEFT JOIN FacebookCredentials fb ON sociusProfile.id = fb.ProfileId
				LEFT JOIN InstagramCredentials ig ON sociusProfile.id = ig.ProfileId
				LEFT JOIN TwitterCredentials tw ON sociusProfile.id = tw.ProfileId");
			scope.Complete();

			return queryResults;
		}

		public async Task<SociusProfilesSchema?> GetProfileDetails(int profileId)
		{
			using var scope = _scopeProvider.CreateScope();
			var queryResults = await scope.Database.FetchAsync<SociusProfilesSchema>
				($@"SELECT 
					sociusProfile.Id,
					sociusProfile.Name,
					sociusProfile.ProfileImage,
					sociusProfile.CreatedBy,
					sociusProfile.CreateDate,
					sociusProfile.UpdatedBy,
					sociusProfile.UpdateDate,
					creator.userName as CreatedByName,
					updater.userName as UpdatedByName,
					fb.ProfileId,
					fb.FbAppId,
					fb.FbClientSecret,
					fb.FbPageId,
					fb.FbToken,
					ig.ProfileId,
					ig.IgClientId,
					ig.IgClientSecret,
					ig.IgRedirectUri,
					ig.IgToken,
					ig.IgTokenExpiry,
					tw.ProfileId,
					tw.TwUserId,
					tw.TwToken
				FROM {_dbName} sociusProfile
				INNER JOIN umbracoUser creator ON sociusProfile.CreatedBy = creator.id
				INNER JOIN umbracoUser updater ON sociusProfile.UpdatedBy = updater.id
				LEFT JOIN FacebookCredentials fb ON sociusProfile.id = fb.ProfileId
				LEFT JOIN InstagramCredentials ig ON sociusProfile.id = ig.ProfileId
				LEFT JOIN TwitterCredentials tw ON sociusProfile.id = tw.ProfileId
				WHERE sociusProfile.Id={profileId}");
			scope.Complete();

			return queryResults.FirstOrDefault();
		}
	}
}
