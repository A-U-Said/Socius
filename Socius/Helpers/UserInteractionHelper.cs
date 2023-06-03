using Socius.Dto.Commands;
using Socius.Extensions;
using Socius.Helpers;
using Socius.Models.Repositories;
using Socius.Models.Shared;
using Socius.Repositories;
using System.Linq;

namespace Socius.Socius.Helpers
{
	public class UserInteractionHelper : IUserInteractionHelper
	{
		private readonly IUserInteractionRepository _repository;
		private readonly ILogger<UserInteractionHelper> _logger;

		public UserInteractionHelper(
			IUserInteractionRepository repository,
			ILogger<UserInteractionHelper> logger)
		{
			_repository = repository;
			_logger = logger;
		}


		public async Task IncrementClickCount(int sociusProfileId, SocialMediaFeedType feedType)
		{
			UserInteractionsSchema? userInteractionRecord;

			userInteractionRecord = (await _repository.GetBy(x => x.ProfileId == sociusProfileId))?
				.OrderByDescending(x => x.Date)
				.FirstOrDefault()
				.ThrowIfNull("Socius profile cannot be found");

			if (userInteractionRecord == null || userInteractionRecord.Date.ToString("yyyy-MM-dd hh") != DateTime.Now.ToString("yyyy-MM-dd hh"))
			{

				var newHourlyInteractionRecord = new UserInteractionsSchema(sociusProfileId, new SaveUserInteractionCommand(DateTime.Now, 0, 0, 0));
				userInteractionRecord = await _repository.Create(newHourlyInteractionRecord);
			}

			switch (feedType)
			{
				case SocialMediaFeedType.Facebook:
					userInteractionRecord.FacebookClicks += 1;
					break;
				case SocialMediaFeedType.Instagram:
					userInteractionRecord.InstagramClicks += 1;
					break;
				case SocialMediaFeedType.Twitter:
					userInteractionRecord.TwitterClicks += 1;
					break;
				default:
					break;
			}

			await _repository.Update(userInteractionRecord);
		}
	}
}
