using Microsoft.AspNetCore.Mvc;
using Socius.Helpers;
using Socius.Dto.Commands;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class SociusFeedAsyncController : UmbracoApiController
	{
		private readonly ISociusFeedHelper _sociusFeedHelper;
		private readonly IUserInteractionHelper _userInteractionHelper;
		private readonly ILogger<SociusFeedAsyncController> _logger;

		public SociusFeedAsyncController(
			ISociusFeedHelper sociusFeedHelper,
			IUserInteractionHelper userInteractionHelper,
			ILogger<SociusFeedAsyncController> logger)
		{
			_sociusFeedHelper = sociusFeedHelper;
			_userInteractionHelper = userInteractionHelper;
			_logger = logger;
		}


		[HttpGet]
		[Route("socius/feeds/{profile}")]
		public IActionResult GetSocialMediaPostRender(int profile)
		{
			return Ok(_sociusFeedHelper.GetSociusFeeds(profile));
		}


		[HttpPost]
		[Route("socius/interaction")]
		public async Task<IActionResult> RecordClick([FromBody] RecordInteractionCommand command)
		{
			if (command.ProfileId == 0 || command.FeedType == 0)
			{
				return BadRequest("Required parameters not provided");
			}

			await _userInteractionHelper.IncrementClickCount(command.ProfileId, command.FeedType);

			return Ok();
		}

	}

}