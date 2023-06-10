using Microsoft.AspNetCore.Mvc;
using Socius.Dto.Views;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Controllers;

namespace Socius.Controllers
{
	[PluginController("Socius")]
	public class HelpController : UmbracoApiController
	{
		private readonly ILogger<HelpController> _logger;

		public HelpController(ILogger<HelpController> logger)
		{
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> GetFaq()
		{
			var faq = new List<FaqQuestionView>()
			{
				new FaqQuestionView("Question 1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque mattis, ex eu lacinia hendrerit, odio nulla ultricies ipsum, fringilla porttitor libero sem et sem. Maecenas quis cursus magna. Duis at luctus felis. Nullam euismod, risus et laoreet eleifend, dolor nisl fringilla tellus, a venenatis tellus ligula vel elit."),
				new FaqQuestionView("Question 2", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque mattis, ex eu lacinia hendrerit, odio nulla ultricies ipsum, fringilla porttitor libero sem et sem. Maecenas quis cursus magna. Duis at luctus felis. Nullam euismod, risus et laoreet eleifend, dolor nisl fringilla tellus, a venenatis tellus ligula vel elit."),
			};

			return Ok(faq);
		}


	}
}