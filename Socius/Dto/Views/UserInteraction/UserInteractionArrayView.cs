namespace Socius.Dto.Views.UserInteraction
{
	public class UserInteractionArrayView
	{
		public UserInteractionArrayView()
		{
			Labels = new List<string>();
			FacebookClicks = new List<int>();
			InstagramClicks = new List<int>();
			TwitterClicks = new List<int>();
		}

		public List<string> Labels { get; set; }
		public List<int> FacebookClicks { get; set; }
		public List<int> InstagramClicks { get; set; }
		public List<int> TwitterClicks { get; set; }

		public void SetDateDefaults()
		{
			for (int i = 0; i < Labels.Count; i++)
			{
				FacebookClicks.Add(0);
				InstagramClicks.Add(0);
				TwitterClicks.Add(0);
			}
		}
	}
}
