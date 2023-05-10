using NPoco;
using Socius.Dto.Commands;
using System.Data;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	[TableName("FacebookCredentials")]
	[PrimaryKey("ProfileId", AutoIncrement = true)]
	[ExplicitColumns]
	public class FacebookCredentialsSchema : ISociusSchema<UpdateFacebookCredentialsCommand>
	{
		[PrimaryKeyColumn(AutoIncrement = false), ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		[Column("ProfileId")]
		public required int ProfileId { get; set; }

		[Column("FbAppId")]
		public required long AppId { get; set; }

		[Column("FbClientSecret")]
		public required string ClientSecret { get; set; }

		[Column("FbPageID")]
		public required long PageID { get; set; }

		[Column("FbToken")]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public required string Token { get; set; }

		public void Update(UpdateFacebookCredentialsCommand newDetails)
		{
			AppId = newDetails.AppId;
			ClientSecret = newDetails.ClientSecret;
			PageID = newDetails.PageID;
			Token = newDetails.Token;
		}

	}
}
