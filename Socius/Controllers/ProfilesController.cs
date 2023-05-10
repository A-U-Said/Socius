using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Socius.Repositories;
using Socius.Dto.Views.Profiles;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;
using Socius.Dto.Commands;
using Socius.Helpers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class ProfilesController : UmbracoApiController
	{
        private readonly ISociusProfileRepository _repository;
		private readonly ISociusProfilesHelper _profilesHelper;
		private readonly ILogger<ProfilesController> _logger;

        public ProfilesController(
			ISociusProfileRepository repository,
			ISociusProfilesHelper profilesHelper,
            ILogger<ProfilesController> logger)
        {
            _repository = repository;
			_profilesHelper = profilesHelper;
			_logger = logger;
        }


		[HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            var profiles = await _repository.GetProfileList();
			var mapped = profiles.Select(dbo => new SociusProfileListView(dbo));

			return Ok(mapped);
        }


		[HttpGet]
		public async Task<IActionResult> GetProfile(int profileId)
		{
			var profileDetails = await _repository.GetProfileDetails(profileId);

			if (profileDetails == null)
			{
				return NotFound();
			}

			var mapped = new SociusProfileDetailView(profileDetails);

			return Ok(mapped);
		}


		[HttpPost]
		public async Task<IActionResult> SetProfileImage(int profileId, IList<IFormFile> file)
		{
			var newImageUri = await _profilesHelper.SetProfileImage(profileId, file);

			if (newImageUri == null)
			{
				return NotFound();
			}

			return Ok(newImageUri);
		}


		[HttpPost]
		public async Task<IActionResult> UpdateProfile(int profileId, [FromBody] SaveProfileCommand profile)
		{
			await _profilesHelper.UpdateProfile(profileId, profile);
			return Ok();
		}
	}
}