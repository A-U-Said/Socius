using Microsoft.AspNetCore.Html;
using Socius.Dto.Views.Feeds;
using Socius.Helpers;
using Socius.Models.Shared;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Socius.Extensions
{
	public static class SociusPageExtensions
	{
		public static SociusFeedView GetFeeds(this IPublishedContent content, object? profile)
		{
			var sociusFeedHelper = StaticServiceProvider.Instance.GetRequiredService<ISociusFeedHelper>();

			if (profile == null || profile is not int)
			{
				return new SociusFeedView();
			}

			return sociusFeedHelper.GetSociusFeeds((int)profile);
		}

		public static IHtmlContent RenderFeeds(this IPublishedContent content, object? profile, bool showTitle = true)
		{
			string htmlString = "";
			var sociusFeedHelper = StaticServiceProvider.Instance.GetRequiredService<ISociusFeedHelper>();

			if (profile == null || profile is not int)
			{
				return new HtmlString(htmlString);
			}

			var profileId = (int)profile;
			var feeds = sociusFeedHelper.GetSociusFeeds(profileId);
			if (feeds == null)
			{
				return new HtmlString(htmlString);
			}

			if (feeds.FacebookPosts.Any())
			{
				htmlString += new HtmlString($@"
					<section class='facebook'>
					{(showTitle ? "<h3>Facebook</h3>" : "")}
					<div class='feed'>
				");
				foreach (var fbPost in feeds.FacebookPosts)
				{
					htmlString += CreatePost(fbPost, profileId, SocialMediaFeedType.Facebook);
				}
				htmlString += new HtmlString($@"
					</div>
					</section>
				");
			}

			if (feeds.InstagramPosts.Any())
			{
				htmlString += new HtmlString($@"
					<section class='instagram'>
					{(showTitle ? "<h3>Instagram</h3>" : "")}
					<div class='feed'>
				");
				foreach (var igPost in feeds.InstagramPosts)
				{
					htmlString += CreatePost(igPost, profileId, SocialMediaFeedType.Instagram);
				}
				htmlString += new HtmlString($@"
					</div>
					</section>
				");
			}

			if (feeds.TwitterPosts.Any())
			{
				htmlString += new HtmlString($@"
					<section class='twitter'>
					{(showTitle ? "<h3>Twitter</h3>" : "")}
					<div class='feed'>
				");
				foreach (var twPost in feeds.TwitterPosts)
				{
					htmlString += CreatePost(twPost, profileId, SocialMediaFeedType.Twitter);
				}
				htmlString += new HtmlString($@"
					</div>
					</section>
				");
			}

			htmlString += new HtmlString($@"<script type='text/javascript' src='/scripts/SociusJs.js'></script>");

			return new HtmlString(htmlString);
		}

		private static IHtmlContent CreatePost(SocialMediaPostView postView, int profileId, SocialMediaFeedType feedType)
		{
			IHtmlContent media = new HtmlString($@"");

			if (!postView.Attachment.MediaUrl.IsNullOrWhiteSpace())
			{
				if (postView.Attachment.MediaType == "photo")
				{
					media = new HtmlString($@"<img src='{postView.Attachment.MediaUrl}' />");
				}
				else if (postView.Attachment.MediaType == "video")
				{
					media = new HtmlString($@"<video src='{postView.Attachment.MediaUrl}' />");
				}
			}

			return new HtmlString($@"
				<article class='post'>
					<a href='{postView.PostLink}' rel='nofollow' target='_blank' onclick='Socius.addInteraction({profileId}, {(int)feedType})'>
						{media}
						<div>
							<p>{postView.Message}</p>
							<small>{@postView.CreatedAt}</small>
						</div>
					</a>
				</article>
			");
		}
	}
}
