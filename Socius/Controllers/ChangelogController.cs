using Microsoft.AspNetCore.Mvc;
using Socius.Dto.Views.Changelog;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class ChangelogController : UmbracoApiController
	{
		private readonly ILogger<ChangelogController> _logger;

		public ChangelogController(ILogger<ChangelogController> logger)
		{
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> GetChanges()
		{
			var changes = new List<ChangelogView>()
			{
				new ChangelogView("0.1", new DateTime(2023, 05, 01), new List<string> {
					"Morbi gravida pharetra nulla nec rhoncus.",
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit."
				}),
				new ChangelogView("0.2", new DateTime(2023, 05, 02), new List<string> {
					"Donec et interdum sem. Fusce eleifend gravida nisi, sit amet tempor ligula feugiat et.",
					"Phasellus tincidunt vestibulum elit, eu gravida tellus congue id."
				}),
			}.OrderByDescending(x => x.Date);

			return Ok(changes);
		}


	}
}