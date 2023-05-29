using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Socius.Helpers;
using Socius.Models.ApiResponses.Instagram;
using Socius.Repositories;
using StackExchange.Profiling.Internal;
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
	public class InstagramCallbackController : SurfaceController
	{
		private readonly IInstagramCredentialsRepository _instagramRepository;
		private readonly IInstagramHelper _instagramHelper;
		private readonly ILogger<InstagramCallbackController> _logger;

		public InstagramCallbackController(
			IUmbracoContextAccessor umbracoContextAccessor,
			IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services,
			AppCaches appCaches,
			IProfilingLogger profilingLogger,
			IPublishedUrlProvider publishedUrlProvider,
			IInstagramCredentialsRepository repository,
			IInstagramHelper instagramHelper,
			ILogger<InstagramCallbackController> logger)
			: base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_instagramRepository = repository;
			_instagramHelper = instagramHelper;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			Request.Query.TryGetValue("code", out var code);
			Request.Query.TryGetValue("state", out var scpId);

			var authCode = code.First();

			if (authCode == null || !int.TryParse(scpId, out var profileId))
			{
				return Content("Invalid callback details");
			}

			var tokenResponse = await _instagramHelper.GetToken(profileId, authCode);
			if (tokenResponse == null)
			{
				return Content("Could not get token");
			}

			var igProfile = await _instagramRepository.GetSingle(profileId);
			if (igProfile == null)
			{
				return Content("Could not find Socius profile with this Id");
			}

			igProfile.IgToken = tokenResponse.AccessToken;
			igProfile.IgTokenExpiry = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);

			await _instagramRepository.Update(igProfile);

			return Content("Instagram token successfully added");
		}

	}

}