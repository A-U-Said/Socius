using NPoco;
using Socius.Dto.Commands;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	//https://dba.stackexchange.com/questions/199226/keep-row-null-or-delete-it
	//nullify rather than delete empty records

	[TableName("FacebookCredentials")]
	[PrimaryKey("ProfileId", AutoIncrement = false)]
	[ExplicitColumns]
	public class FacebookCredentialsSchema : SociusUpdateCommand<UpdateFacebookCredentialsCommand>, ISociusSchema
	{
		public FacebookCredentialsSchema() { }

		[SetsRequiredMembers]
		public FacebookCredentialsSchema(int profileId, UpdateFacebookCredentialsCommand details) 
		{
			ProfileId = profileId;
			AppId = details.AppId;
			ClientSecret = details.ClientSecret;
			Token = details.Token;
			PageID = details.PageID;
		}


		[Column("ProfileId")]
		[PrimaryKeyColumn(AutoIncrement = false)]
		[ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		public required int ProfileId { get; set; }

		[Column("FbAppId")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public long? AppId { get; set; }

		[Column("FbClientSecret")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public string? ClientSecret { get; set; }

		[Column("FbPageID")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public long? PageID { get; set; }

		[Column("FbToken")]
		[NullSetting(NullSetting = NullSettings.Null)]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public string? Token { get; set; }


		public override void Update(UpdateFacebookCredentialsCommand newDetails)
		{
			//if the user wipes all input fields then nullify the record fields but do not delete the record
			if (newDetails.AppId == null && newDetails.ClientSecret.IsNullOrWhiteSpace() && newDetails.PageID == null)
			{
				AppId = null;
				ClientSecret = null;
				PageID = null;
				Token = null;
				return;
			}

			AppId = newDetails.AppId;
			ClientSecret = newDetails.ClientSecret;
			PageID = newDetails.PageID;
			Token = newDetails.Token;
		}

		public bool IsComplete()
		{
			return (
				AppId != null
				&& ClientSecret != null
				&& PageID != null
				&& Token != null
			);
		}
	}
}
