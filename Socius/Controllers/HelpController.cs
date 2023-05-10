using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class HelpController : UmbracoAuthorizedJsonController
	{
		private readonly ILogger<HelpController> _logger;
		private readonly IConfiguration _configuration;

		public HelpController(
			ILogger<HelpController> logger,
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