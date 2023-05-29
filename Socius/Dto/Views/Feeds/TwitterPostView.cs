using Socius.Models.ApiResponses.Twitter;

namespace Socius.Dto.Views.Feeds
{
	public class TwitterPostView
	{
		public TwitterPostView(TwitterPost post, Includes includes)
		{
			var attachments = new List<TwitterAttachmentsView>();
			foreach (var attachmentId in post.Attachments.MediaKeys)
			{
				attachments.Add(new TwitterAttachmentsView(attachmentId, includes.Media));
			}

			Id = post.Id;
			Message = post.Text;
			PostLink = $"https://twitter.com/{includes.Users.First().Username}/status/{post.Id}";
			Attachments = attachments;
			CreatedAt = post.CreatedAt;
		}

		public string Id { get; set; }
		public string PostLink { get; set; }
		public string Message { get; set; }
		public List<TwitterAttachmentsView> Attachments { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	public class TwitterAttachmentsView
	{
		public TwitterAttachmentsView(string attachmentId, List<Medium> media)
		{
			var attachment = media.Where(x => x.MediaKey == attachmentId).FirstOrDefault();
			if (attachment == null)
			{
				return;
			}
			MediaType = attachment.Type;
			MediaUrl = attachment.Url;
		}

		public string MediaType { get; set; }
		public string MediaUrl { get; set; }
	}
}
