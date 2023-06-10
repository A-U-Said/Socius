using Microsoft.AspNetCore.Mvc;
using Socius.Helpers;
using Socius.Models.Shared;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class UserInteractionController : UmbracoApiController
	{
		private readonly IUserInteractionHelper _userInteractionHelper;
		private readonly ILogger<UserInteractionController> _logger;

		public UserInteractionController(
			IUserInteractionHelper userInteractionHelper,
			ILogger<UserInteractionController> logger)
		{
			_userInteractionHelper = userInteractionHelper;
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> GetInteractions(int profile, TimescaleType timescale)
		{
			var records = await _userInteractionHelper.GetByTimescale(profile, timescale);
			return Ok(records);
		}
	}
}
