using Socius.Helpers;
using Socius.Models.ApiResponses.Facebook;
using Socius.Models.ApiResponses.Instagram;
using Socius.Models.ApiResponses.Twitter;
using System.Net.Mail;

namespace Socius.Dto.Views.Feeds
{
	public class SocialMediaPostView
	{
		public SocialMediaPostView(FacebookPost post)
		{
			Id = post.Id;
			PostLink = post.PermalinkUrl;
			Message = post.Message;
			Attachment = new AttachmentsView(post.FullPicture);
			CreatedAt = post.UpdatedTime;
		}

		public SocialMediaPostView(InstagramPost post)
		{
			Id = post.Id;
			PostLink = post.Permalink;
			Message = post.Caption;
			Attachment = new AttachmentsView(post.MediaUrl);
			CreatedAt = post.Timestamp;
		}

		public SocialMediaPostView(TwitterPost post, Includes includes)
		{
			Id = post.Id;
			Message = post.Text;
			PostLink = $"https://twitter.com/{includes.Users.First().Username}/status/{post.Id}";
			Attachment = new AttachmentsView(post.Attachments?.MediaKeys?.FirstOrDefault(), includes.Media);
			CreatedAt = post.CreatedAt;
		}


		public string Id { get; set; }
		public string PostLink { get; set; }
		public string Message { get; set; }
		public AttachmentsView Attachment { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	public class AttachmentsView
	{
		public AttachmentsView(string mediaUrl, List<Medium>? media = null)
		{
			if (mediaUrl == null)
			{
				return;
			}
			if (media == null)
			{
				MediaType = AttachmentHelper.GetAttachmentType(mediaUrl);
				MediaUrl = mediaUrl;
			}
			else
			{
				var attachment = media.Where(x => x.MediaKey == mediaUrl).FirstOrDefault();
				if (attachment == null)
				{
					return;
				}
				MediaType = attachment.Type;
				MediaUrl = attachment.Url;
			}
		}

		public string MediaType { get; set; }
		public string MediaUrl { get; set; }
	}
}
