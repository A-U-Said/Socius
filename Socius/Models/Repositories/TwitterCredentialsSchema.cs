using NPoco;
using Socius.Dto.Commands;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	[TableName("TwitterCredentials")]
	[PrimaryKey("ProfileId", AutoIncrement = false)]
	[ExplicitColumns]
	public class TwitterCredentialsSchema : SociusUpdateCommand<UpdateTwitterCredentialsCommand>, ISociusSchema
	{
		public TwitterCredentialsSchema() { }

		[SetsRequiredMembers]
		public TwitterCredentialsSchema(int profileId, UpdateTwitterCredentialsCommand details)
		{
			ProfileId = profileId;
			TwUserID = details.UserID;
			TwToken = details.Token;
		}

		[Column("ProfileId")]
		[PrimaryKeyColumn(AutoIncrement = false)]
		[ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		public required int ProfileId { get; set; }

		[Column("TwUserID")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public long? TwUserID { get; set; }

		[Column("TwToken")]
		[NullSetting(NullSetting = NullSettings.Null)]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public string? TwToken { get; set; }


		public override void Update(UpdateTwitterCredentialsCommand newDetails)
		{
			//if the user wipes all input fields then nullify the record fields but do not delete the record
			if (newDetails.UserID == null && newDetails.Token.IsNullOrWhiteSpace())
			{
				TwUserID = null;
				TwToken = null;
				return;
			}

			TwUserID = newDetails.UserID;
			TwToken = newDetails.Token;
		}

		public bool IsComplete()
		{
			return (
				TwUserID != null 
				&& TwToken != null
			);
		}

	}
}
