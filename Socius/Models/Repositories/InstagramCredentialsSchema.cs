using NPoco;
using Socius.Dto.Commands;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	[TableName("InstagramCredentials")]
	[PrimaryKey("ProfileId", AutoIncrement = false)]
	[ExplicitColumns]
	public class InstagramCredentialsSchema : SociusUpdateCommand<UpdateInstagramCredentialsCommand>, ISociusSchema
	{
		public InstagramCredentialsSchema() { }

		[SetsRequiredMembers]
		public InstagramCredentialsSchema(int profileId, UpdateInstagramCredentialsCommand details)
		{
			ProfileId = profileId;
			IgClientId = details.ClientId;
			IgClientSecret = details.ClientSecret;
			IgRedirectUri = details.RedirectUri;
			IgToken = details.Token;
			IgTokenExpiry = details.TokenExpiry;
		}

		[Column("ProfileId")]
		[PrimaryKeyColumn(AutoIncrement = false)]
		[ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		public required int ProfileId { get; set; }

		[Column("IgClientId")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public long? IgClientId { get; set; }

		[Column("IgClientSecret")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public string? IgClientSecret { get; set; }

		[Column("IgRedirectUri")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public string? IgRedirectUri { get; set; }

		[Column("IgToken")]
		[NullSetting(NullSetting = NullSettings.Null)]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public string? IgToken { get; set; }

		[Column("IgTokenExpiry")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public DateTime? IgTokenExpiry { get; set; }

		public override void Update(UpdateInstagramCredentialsCommand newDetails)
		{
			//if the user wipes all input fields then nullify the record fields but do not delete the record
			if (newDetails.ClientId == null && newDetails.ClientSecret.IsNullOrWhiteSpace())
			{
				IgClientId = null;
				IgClientSecret = null;
				IgRedirectUri = null;
				IgToken = null;
				IgTokenExpiry = null;
				return;
			}

			IgClientId = newDetails.ClientId;
			IgClientSecret = newDetails.ClientSecret;
			IgRedirectUri = newDetails.RedirectUri;
			IgToken = newDetails.Token;
			IgTokenExpiry = newDetails.TokenExpiry;
		}

		public bool IsComplete()
		{
			return (
				IgClientId != null 
				&& IgClientSecret != null
				&& IgRedirectUri != null
				&& IgTokenExpiry != null
				&& !IgToken.IsNullOrWhiteSpace()
			);
		}
	}
}
