using NPoco;
using Socius.Dto.Commands;
using Socius.Migrations.Repositories;
using System.Data;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	[TableName("InstagramCredentials")]
	[PrimaryKey("ProfileId", AutoIncrement = true)]
	[ExplicitColumns]
	public class InstagramCredentialsSchema : ISociusSchema
	{
		[PrimaryKeyColumn(AutoIncrement = false), ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		[Column("ProfileId")]
		public required int ProfileId { get; set; }

		[Column("IgClientId")]
		public required long IgClientId { get; set; }

		[Column("IgClientSecret")]
		public required string IgClientSecret { get; set; }

		[Column("IgRedirectUri")]
		public required string IgRedirectUri { get; set; }

		[Column("IgToken")]
		[SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
		public required string IgToken { get; set; }

		[Column("IgTokenExpiry")]
		public required DateTime IgTokenExpiry { get; set; }


		public void Update(UpdateInstagramCredentialsCommand newDetails)
		{
			IgClientId = newDetails.ClientId;
			IgClientSecret = newDetails.ClientSecret;
			IgRedirectUri = newDetails.RedirectUri;
			IgToken = newDetails.Token;
		}
	}
}
