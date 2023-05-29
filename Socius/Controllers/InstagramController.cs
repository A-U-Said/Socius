﻿using Microsoft.AspNetCore.Mvc;
using Socius.Dto.Views.Profiles;
using Socius.Helpers;
using Socius.Repositories;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class InstagramController : UmbracoApiController
	{
		private readonly IInstagramCredentialsRepository _repository;
		private readonly IInstagramHelper _instagramHelper;
		private readonly ILogger<InstagramController> _logger;

		public InstagramController(
			IInstagramCredentialsRepository repository,
			IInstagramHelper instagramHelper,
			ILogger<InstagramController> logger)
		{
			_repository = repository;
			_instagramHelper = instagramHelper;
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> GetInstagramAuthCodeParams(int profileId)
		{
			var igCredentials = await _repository.GetSingle(profileId);

			if (igCredentials == null)
			{
				return NotFound();
			}

			return Ok(new InstagramCredentialsView(igCredentials));
		}


		[HttpGet]
		public async Task<IActionResult> RefreshInstagramToken(int profileId)
		{
			var igCredentials = await _repository.GetSingle(profileId);
			if (igCredentials == null || string.IsNullOrEmpty(igCredentials.IgToken))
			{
				return NotFound("No instagram token found");
			}

			var refreshResult = await _instagramHelper.RefreshInstagramToken(profileId);
			if (refreshResult == TaskStatus.Faulted)
			{
				return BadRequest("Failed to refresh token with Instagram API");
			}

			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> ClearIgToken(int profileId)
		{
			var igCredentials = await _repository.GetSingle(profileId);
			if (igCredentials == null || string.IsNullOrEmpty(igCredentials.IgToken))
			{
				return NotFound("No instagram token found");
			}

			igCredentials.IgToken = null;
			await _repository.Update(igCredentials);
			
			return Ok();
		}
	}

}