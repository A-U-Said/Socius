using NPoco;
using Socius.Dto.Commands;
using System.Data;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	[TableName("TwitterCredentials")]
	[PrimaryKey("ProfileId", AutoIncrement = true)]
	[ExplicitColumns]
	public class TwitterCredentialsSchema : ISociusSchema
	{
		[PrimaryKeyColumn(AutoIncrement = false), ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		[Column("ProfileId")]
		public required int ProfileId { get; set; }

		[Column("TwUserID")]
		public required long TwUserID { get; set; }

		[Column("TwToken")]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public required string TwToken { get; set; }


		public void Update(UpdateTwitterCredentialsCommand newDetails)
		{
			TwUserID = newDetails.UserID;
			TwToken = newDetails.Token;
		}

	}
}
