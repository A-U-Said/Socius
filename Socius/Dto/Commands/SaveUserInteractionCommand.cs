namespace Socius.Dto.Commands
{
	public class SaveUserInteractionCommand : ISociusUpdateCommand
	{
		public SaveUserInteractionCommand(DateTime date, int facebookClicks, int instagramClicks, int twitterClicks)
		{ 
			Date = date;
			FacebookClicks = facebookClicks;
			InstagramClicks = instagramClicks;
			TwitterClicks = twitterClicks;
		}

		public DateTime Date { get; set; }
		public int FacebookClicks { get; set; }
		public int InstagramClicks { get; set; }
		public int TwitterClicks { get; set; }
	}
}
