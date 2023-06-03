using NPoco;
using Socius.Dto.Commands;
using Socius.Models.Shared;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Socius.Models.Repositories
{
	[TableName("UserInteractions")]
	[ExplicitColumns]
	public class UserInteractionsSchema : SociusUpdateCommand<SaveUserInteractionCommand>, ISociusSchema
	{
		public UserInteractionsSchema()
		{
		}

		[SetsRequiredMembers]
		public UserInteractionsSchema(int profileId, SaveUserInteractionCommand details)
		{
			ProfileId = profileId;
			Date = details.Date;
			FacebookClicks = details.FacebookClicks;
			InstagramClicks = details.InstagramClicks;
			TwitterClicks = details.TwitterClicks;
		}

		[Column("Id")]
		[PrimaryKeyColumn(AutoIncrement = true)]
		public required int Id { get; set; }

		[Column("ProfileId")]
		[ForeignKey(typeof(SociusProfilesSchema), Column = "Id", OnDelete = Rule.Cascade)]
		public required int ProfileId { get; set; }

		[Column("Date")]
		public DateTime Date { get; set; }

		[Column("FacebookClicks")]
		public int FacebookClicks { get; set; }

		[Column("InstagramClicks")]
		public int InstagramClicks { get; set; }

		[Column("TwitterClicks")]
		public int TwitterClicks { get; set; }

		public bool IsComplete()
		{
			return true;
		}

		public override void Update(SaveUserInteractionCommand newDetails)
		{
			FacebookClicks = newDetails.FacebookClicks;
			InstagramClicks = newDetails.InstagramClicks;
			TwitterClicks = newDetails.TwitterClicks;
		}
	}
}
