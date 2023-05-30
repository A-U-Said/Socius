using Socius.Dto.Views.Feeds;
using Socius.Helpers;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Socius.Extensions
{
	public static class SociusPageExtensions
	{
		public static SociusFeedView GetSocialMedia(this IPublishedContent content, object? profile)
		{
			var sociusFeedHelper = StaticServiceProvider.Instance.GetRequiredService<ISociusFeedHelper>();

			if (profile == null || profile is not int)
			{
				return new SociusFeedView();
			}

			return sociusFeedHelper.GetSociusFeeds((int)profile);
		}
	}
}
