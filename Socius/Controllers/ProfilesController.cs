using Microsoft.AspNetCore.Mvc;
using Socius.Repositories;
using Socius.Dto.Views.Profiles;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;
using Socius.Dto.Commands;
using Socius.Helpers;
using Socius.Models.Repositories;

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
			if (profile == null)
			{
				return BadRequest();
			}

			await _profilesHelper.UpdateProfile(profileId, profile);
			return Ok();
		}


		[HttpPost]
		public async Task<IActionResult> CreateProfile([FromBody] SaveProfileCommand newProfile)
		{
			if (newProfile == null)
			{
				return BadRequest();
			}

			var createdProfile = await _profilesHelper.CreateProfile(newProfile);

			return Ok(createdProfile.Id);
		}


		[HttpDelete]
		public async Task<IActionResult> DeleteProfile(int profileId)
		{
			var deleteResult = await _profilesHelper.DeleteProfile(profileId);

			if (deleteResult == TaskStatus.Faulted)
			{
				return NotFound();
			}

			return Ok();
		}

	}
}