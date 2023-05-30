using Microsoft.AspNetCore.Mvc;
using Socius.Dto.Views.Feeds;
using Socius.Helpers;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Website.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class SociusFeedAsyncController : SurfaceController
	{
		private readonly ISociusFeedHelper _sociusFeedHelper;
		private readonly ILogger<SociusFeedAsyncController> _logger;

		public SociusFeedAsyncController(
			IUmbracoContextAccessor umbracoContextAccessor,
			IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services,
			AppCaches appCaches,
			IProfilingLogger profilingLogger,
			IPublishedUrlProvider publishedUrlProvider,
			ISociusFeedHelper sociusFeedHelper,
			ILogger<SociusFeedAsyncController> logger)
			: base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_sociusFeedHelper = sociusFeedHelper;
			_logger = logger;
		}


		[HttpGet]
		public SociusFeedView GetSocialMediaPostRender(int profile)
		{
			return _sociusFeedHelper.GetSociusFeeds(profile);
		}

	}

}