using Socius.Helpers;
using Socius.Models.ApiResponses.Facebook;

namespace Socius.Dto.Views.Feeds
{
	public class FacebookPostView
	{
		public FacebookPostView(FacebookPost post)
		{
			Id = post.Id;
			PostLink = post.PermalinkUrl;
			Message = post.Message;
			Attachment = new FacebookAttachmentsView(post.FullPicture);
			CreatedAt = post.UpdatedTime;
		}

		public string Id { get; set; }
		public string PostLink { get; set; }
		public string Message { get; set; }
		public FacebookAttachmentsView Attachment { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	public class FacebookAttachmentsView
	{
		public FacebookAttachmentsView(string mediaUrl)
		{
			MediaType = AttachmentHelper.GetAttachmentType(mediaUrl);
			MediaUrl = mediaUrl;
		}

		public string MediaType { get; set; }
		public string MediaUrl { get; set; }
	}
}
