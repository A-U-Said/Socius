namespace Socius.Dto.Views.Changelog
{
	public class ChangelogView
	{
		public ChangelogView(string title, DateTime date, IEnumerable<string> changes)
		{
			Title = title;
			Date = date;
			Changes = changes;
		}

		public string Title { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<string> Changes { get; set; }
	}
}
