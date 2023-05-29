using Socius.Models.ApiResponses.Twitter;

namespace Socius.Dto.Views.Feeds
{
	public class TwitterPostView
	{
		public TwitterPostView(TwitterPost post, Includes includes)
		{
			var attachments = new List<AttachmentsView>();
			foreach (var attachmentId in post.Attachments.MediaKeys)
			{
				attachments.Add(new AttachmentsView(attachmentId, includes.Media));
			}

			Id = post.Id;
			Text = post.Text;
			PostLink = $"https://twitter.com/{includes.Users.First().Username}/status/{post.Id}";
			Attachments = attachments;
			CreatedAt = post.CreatedAt;
		}

		public string Id { get; set; }
		public string PostLink { get; set; }
		public string Text { get; set; }
		public List<AttachmentsView> Attachments { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	public class AttachmentsView
	{
		public AttachmentsView(string attachmentId, List<Medium> media)
		{
			var attachment = media.Where(x => x.MediaKey == attachmentId).FirstOrDefault();
			if (attachment == null)
			{
				return;
			}
			MediaKey = attachment.MediaKey;
			Type = attachment.Type;
			Url = attachment.Url;
		}

		public string MediaKey { get; set; }
		public string Type { get; set; }
		public string Url { get; set; }
	}
}
