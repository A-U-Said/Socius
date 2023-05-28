using Microsoft.AspNetCore.Mvc;
using Socius.Dto.Commands;
using Socius.Dto.Views.Profiles;
using Socius.Helpers;
using Socius.Repositories;
using Socius.Socius.Dto.Commands;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class FacebookController : UmbracoApiController
	{
		private readonly IFacebookCredentialsRepository _repository;
		private readonly IFacebookHelper _facebookHelper;
		private readonly ILogger<FacebookController> _logger;

		public FacebookController(
			IFacebookCredentialsRepository repository,
			IFacebookHelper facebookHelper,
			ILogger<FacebookController> logger)
		{
			_repository = repository;
			_facebookHelper = facebookHelper;
			_logger = logger;
		}


		[HttpDelete]
		public async Task<IActionResult> ClearFbToken(int profileId)
		{
			var fbCredentials = await _repository.GetSingle(profileId);
			if (fbCredentials == null || string.IsNullOrEmpty(fbCredentials.Token))
			{
				return NotFound("No FB token token found");
			}

			fbCredentials.Token = null;
			await _repository.Update(fbCredentials);

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> GetPageToken(int profileId, [FromBody] FacebookUserTokenExchangeCommand command)
		{
			var updatedProfile = await _facebookHelper.UserTokenToPageToken(profileId, command);

			return Ok(updatedProfile);
		}
	}

}