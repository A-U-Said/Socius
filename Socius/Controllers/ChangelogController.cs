using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class ChangelogController : UmbracoAuthorizedJsonController
	{
		private readonly ILogger<ChangelogController> _logger;
		private readonly IConfiguration _configuration;

		public ChangelogController(
			ILogger<ChangelogController> logger,
			IConfiguration configuration)
		{
			_configuration = configuration;
			_logger = logger;
		}


		[HttpGet]
		public void GetRootNodes()
		{

		}


	}
}