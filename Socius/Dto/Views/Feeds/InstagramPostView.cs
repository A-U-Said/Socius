using Socius.Helpers;
using Socius.Models.ApiResponses.Instagram;

namespace Socius.Dto.Views.Feeds
{
	public class InstagramPostView
	{
		public InstagramPostView(InstagramPost post)
		{
			Id = post.Id;
			PostLink = post.Permalink;
			Message = post.Caption;
			Attachment = new InstagramAttachmentsView(post.MediaUrl);
			CreatedAt = post.Timestamp;
		}

		public string Id { get; set; }
		public string PostLink { get; set; }
		public string Message { get; set; }
		public InstagramAttachmentsView Attachment { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	public class InstagramAttachmentsView
	{
		public InstagramAttachmentsView(string mediaUrl)
		{
			MediaType = AttachmentHelper.GetAttachmentType(mediaUrl);
			MediaUrl = mediaUrl;
		}

		public string MediaType { get; set; }
		public string MediaUrl { get; set; }
	}
}
