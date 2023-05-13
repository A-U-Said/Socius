using NPoco;
using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;
using Umbraco.Cms.Infrastructure.Scoping;
using System.Data;
using static Umbraco.Cms.Core.Persistence.SqlExtensionsStatics;

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

		public async Task<ICollection<SociusProfilesSchema>> GetProfileListSql() //Legacy
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

		public async Task<SociusProfilesSchema?> GetProfileDetailsSql(int profileId) //Legacy
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

		public async Task<ICollection<SociusProfilesSchema>> GetProfileList()
		{
			using var scope = _scopeProvider.CreateScope();

			Sql<ISqlContext>? sql1 = scope.SqlContext?.Sql()?
				.Select<SociusProfilesSchema>()
				.AndSelect<FacebookCredentialsSchema>()
				.AndSelect<InstagramCredentialsSchema>()
				.AndSelect<TwitterCredentialsSchema>()
				.From<SociusProfilesSchema>()
				.InnerJoin<FacebookCredentialsSchema>()
					.On<SociusProfilesSchema, FacebookCredentialsSchema>((sp, fb) => sp.Id == fb.ProfileId)
				.InnerJoin<InstagramCredentialsSchema>()
					.On<SociusProfilesSchema, InstagramCredentialsSchema>((sp, ig) => sp.Id == ig.ProfileId)
				.InnerJoin<TwitterCredentialsSchema>()
					.On<SociusProfilesSchema, TwitterCredentialsSchema>((sp, tw) => sp.Id == tw.ProfileId)
				.OrderBy<SociusProfilesSchema>(x => x.Id);

			var queryResults = await scope.Database.FetchAsync<SociusProfilesSchema>(sql1);
			scope.Complete();

			return queryResults;
		}

		//Moved to NPOCO syntax in the hopes that a single aggregate root can be used. Still doesn't work.
		public async Task<SociusProfilesSchema?> GetProfileDetails(int profileId)
		{
			using var scope = _scopeProvider.CreateScope();

			Sql<ISqlContext>? sql1 = scope.SqlContext?.Sql()?
				.Select<SociusProfilesSchema>()
				.AndSelect<UserDto>("creator", x => x.Id, x => Alias(x.UserName, "CreatedByName"))
				.AndSelect<UserDto>("updater", x => x.Id, x => Alias(x.UserName, "UpdatedByName"))
				.AndSelect<FacebookCredentialsSchema>()
				.AndSelect<InstagramCredentialsSchema>()
				.AndSelect<TwitterCredentialsSchema>()
				.From<SociusProfilesSchema>()
				.InnerJoin<UserDto>("creator")
					.On<SociusProfilesSchema, UserDto>((sp, usr) => sp.CreatedBy == usr.Id, aliasRight: "creator")
				.InnerJoin<UserDto>("updater")
					.On<SociusProfilesSchema, UserDto>((sp, usr) => sp.UpdatedBy == usr.Id, aliasRight: "updater")
				.InnerJoin<FacebookCredentialsSchema>()
					.On<SociusProfilesSchema, FacebookCredentialsSchema>((sp, fb) => sp.Id == fb.ProfileId)
				.InnerJoin<InstagramCredentialsSchema>()
					.On<SociusProfilesSchema, InstagramCredentialsSchema>((sp, ig) => sp.Id == ig.ProfileId)
				.InnerJoin<TwitterCredentialsSchema>()
					.On<SociusProfilesSchema, TwitterCredentialsSchema>((sp, tw) => sp.Id == tw.ProfileId)
				.Where<SociusProfilesSchema>(x => x.Id == profileId)
				.OrderBy<SociusProfilesSchema>(x => x.Id);

			var queryResults = await scope.Database.FetchAsync<SociusProfilesSchema>(sql1);
			scope.Complete();

			return queryResults.FirstOrDefault();
		}

	}
}
