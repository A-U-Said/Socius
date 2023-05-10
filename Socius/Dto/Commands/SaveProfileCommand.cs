
namespace Socius.Dto.Commands
{
	public class SaveProfileCommand
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string? ProfileImage { get; set; }
		public UpdateFeedsCommand? Feeds { get; set; }
		public int? UpdatedById { get; set; }

		public void AddUpdater(int updatedById)
		{
			UpdatedById = updatedById;
		}
	}
}
