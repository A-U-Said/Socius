using Umbraco.Cms.Core.Models.PublishedContent;

namespace Socius.Extensions;

public static class SociusExtensions
{
	public static object? GetSocialMedia(this IPublishedContent content, object? profileId)
	{
		if (profileId == null || !(profileId is int))
		{
			return null;
		}

		return profileId;
	}

	public static T ThrowIfNull<T>(this T source, string errorMessage)
	{
		if (source != null)
		{
			return source;
		}

		throw new ArgumentNullException(nameof(source), errorMessage);
	}
}
