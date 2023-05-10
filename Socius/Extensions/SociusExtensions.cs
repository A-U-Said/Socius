using System.Data;
using Examine;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace Socius.Extensions;

public static class SociusExtensions
{
	public static object? GetSocialMedia(this IPublishedContent content, int profileId)
	{
		return null;
	}
}
