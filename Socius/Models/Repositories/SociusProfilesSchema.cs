using NPoco;
using Socius.Dto.Commands;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;

namespace Socius.Models.Repositories
{
	[TableName("SociusProfiles")]
	[PrimaryKey("Id", AutoIncrement = true)]
	[ExplicitColumns]
	public class SociusProfilesSchema : ISociusSchema
	{
		public SociusProfilesSchema() { }

		[SetsRequiredMembers]
		public SociusProfilesSchema(SaveProfileCommand newDetails, int createdById)
		{
			Name = newDetails.Name;
			ProfileImage = newDetails.ProfileImage;
			CreatedBy = createdById;
			CreateDate = DateTime.Now;
			UpdatedBy = createdById;
			UpdateDate = DateTime.Now;
		}

		[Column("Id")]
		[PrimaryKeyColumn(AutoIncrement = true)]
		public int Id { get; set; }

		[Column("Name")]
		public required string Name { get; set; }

		[Column("ProfileImage")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public string? ProfileImage { get; set; }

		[Column("CreatedBy")]
		[ForeignKey(typeof(UserDto), Column = "Id", Name = "FK_SociusProfiles_umbracoUser_Id_CreatedBy", OnDelete = Rule.None)]
		public required int CreatedBy { get; set; }

		[Column("CreateDate")]
		public required DateTime CreateDate { get; set; }

		[Column("UpdatedBy")]
		[ForeignKey(typeof(UserDto), Column = "Id", Name = "FK_SociusProfiles_umbracoUser_Id_UpdatedBy", OnDelete = Rule.None)]
		public required int UpdatedBy { get; set; }

		[Column("UpdateDate")]
		public required DateTime UpdateDate { get; set; }

		[ResultColumn]
		[Reference(ReferenceType.OneToOne, ColumnName = "Id", ReferenceMemberName = "ProfileId")]
		public FacebookCredentialsSchema Facebook { get; set; } = null!;

		[ResultColumn]
		[Reference(ReferenceType.OneToOne, ColumnName = "Id", ReferenceMemberName = "ProfileId")]
		public InstagramCredentialsSchema Instagram { get; set; } = null!;

		[ResultColumn]
		[Reference(ReferenceType.OneToOne, ColumnName = "Id", ReferenceMemberName = "ProfileId")]
		public TwitterCredentialsSchema Twitter { get; set; } = null!;

		[ResultColumn]
		[Reference(ReferenceType.OneToOne, ColumnName = "CreatedBy")]
		public UserDto Creator { get; set; } = null!;

		[ResultColumn]
		[Reference(ReferenceType.OneToOne, ColumnName = "UpdatedBy")]
		public UserDto Updater { get; set; } = null!;


		public void SetProfileImage(string imageUri)
		{
			ProfileImage = imageUri;
		}

		public void Update(SaveProfileCommand newDetails)
		{
			Name = newDetails.Name;
			ProfileImage = newDetails.ProfileImage;
			UpdateDate = DateTime.Now;
		}
	}
}
