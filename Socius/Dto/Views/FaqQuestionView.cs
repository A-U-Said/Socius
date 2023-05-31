namespace Socius.Socius.Dto.Views
{
	public class FaqQuestionView
	{
		public FaqQuestionView(string question, string answer)
		{
			Question = question;
			Answer = answer;
		}

		public string Question { get; set; }
		public string Answer { get; set; }
	}
}
