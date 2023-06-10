using NUglify.Helpers;
using Socius.Dto.Commands;
using Socius.Dto.Views.UserInteraction;
using Socius.Extensions;
using Socius.Models.Repositories;
using Socius.Models.Shared;
using Socius.Models.UserInteraction;
using Socius.Repositories;

namespace Socius.Helpers
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
			UserInteractionsSchema? userInteractionRecord = null;

			var records = await _repository.GetBy(x => x.ProfileId == sociusProfileId);

			if (records != null && records.Any())
			{
				userInteractionRecord = records.OrderByDescending(x => x.Date)
					.FirstOrDefault()
					.ThrowIfNull("Socius profile cannot be found");
			}

			if (userInteractionRecord == null || userInteractionRecord.Date.ToString("yyyy-MM-dd HH") != DateTime.Now.ToString("yyyy-MM-dd HH"))
			{
				var dt = DateTime.Now;
				var rounded = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
				var newHourlyInteractionRecord = new UserInteractionsSchema(sociusProfileId, new SaveUserInteractionCommand(rounded, 0, 0, 0));
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


		public async Task<UserInteractionArrayView?> GetByTimescale(int profileId, TimescaleType timescale)
		{
			const int startYear = 2022;
			var uiArrs = new UserInteractionArrayView();

			static DateTime GetWeekStartDate(DateTime? date = null)
			{
				var contextDate = date ?? DateTime.Today;
				var weekStartDate = contextDate.AddDays(-(int)contextDate.DayOfWeek + (int)DayOfWeek.Monday);
				return weekStartDate.Date;
			}

			static DateTime GetMonthStartDate(DateTime? date = null)
			{
				var contextDate = date ?? DateTime.Today;
				var monthStartDate = new DateTime(contextDate.Year, contextDate.Month, 1);
				return monthStartDate.Date;
			}

			static DateTime GetYearStartDate(DateTime? date = null)
			{
				var contextDate = date ?? DateTime.Today;
				var yearStartDate = new DateTime(contextDate.Year, 1, 1);
				return yearStartDate.Date;
			}

			static int GetTimescaleIndexer(TimescaleType timescale, DateTime groupDate)
			{
				return timescale switch
				{
					TimescaleType.Day => groupDate.Hour,
					TimescaleType.Week => (int)groupDate.DayOfWeek - 1,
					TimescaleType.Month => groupDate.Day - 1,
					TimescaleType.Year => groupDate.Month - 1,
					TimescaleType.AllTime => groupDate.Year - startYear,
				};
			}

			async static Task<List<string>> PopulateYears(IUserInteractionRepository repo, int profileId)
			{
				var mostRecentRecord = await repo.GetMostRecentRecord(profileId);
				if (mostRecentRecord == null)
				{
					return new List<string>();
				}
				var numberOfYears = (mostRecentRecord.Date.Year - startYear) + 2;
				return Enumerable.Range(startYear, numberOfYears).Select(n => n.ToString()).ToList();
			}

			Func<UserInteractionsSchema, bool> whereCondition = timescale switch
			{
				TimescaleType.Day => x => (x.ProfileId == profileId) && (x.Date.Date == DateTime.Now.Date),
				TimescaleType.Week => x => (x.ProfileId == profileId) && (x.Date.Date >= GetWeekStartDate()),
				TimescaleType.Month => x => (x.ProfileId == profileId) && (x.Date.Date >= GetMonthStartDate()),
				TimescaleType.Year => x => (x.ProfileId == profileId) && (x.Date >= GetYearStartDate()),
				TimescaleType.AllTime => x => (x.ProfileId == profileId),
				_ => x => false
			};

			Func<UserInteractionsSchema, DateTime> groupCondition = timescale switch
			{
				TimescaleType.Day => x => x.Date,
				TimescaleType.Week => x => x.Date.Date,
				TimescaleType.Month => x => x.Date.Date, //Because it is still a day view. Use GetWeekStartDate(x.Date) to split into weeks.
				TimescaleType.Year => x => GetMonthStartDate(x.Date),
				TimescaleType.AllTime => x => GetYearStartDate(x.Date),
				_ => x => x.Date
			};

			uiArrs.Labels = timescale switch
			{
				TimescaleType.Day => Enumerable.Range(1, 24).Select(n => n.ToString()).ToList(),
				TimescaleType.Week => new List<string>() { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" },
				TimescaleType.Month => Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).Select(n => n.ToString()).ToList(),
				TimescaleType.Year => new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
				TimescaleType.AllTime => await PopulateYears(_repository, profileId),
				_ => new List<string>()
			};
			uiArrs.SetDateDefaults();

			var records = await _repository.GetBy(whereCondition);
			if (records == null)
			{
				return null;
			}

			var ee = records
				.GroupBy(groupCondition)
				.Select(x => new UserInteraction()
				{
					GroupDate = x.Key,
					FacebookClicks = x.Sum(y => y.FacebookClicks),
					InstagramClicks = x.Sum(y => y.InstagramClicks),
					TwitterClicks = x.Sum(y => y.TwitterClicks)
				});

				ee.ForEach(userInteraction => {
					uiArrs.FacebookClicks[GetTimescaleIndexer(timescale, userInteraction.GroupDate)] = userInteraction.FacebookClicks;
					uiArrs.InstagramClicks[GetTimescaleIndexer(timescale, userInteraction.GroupDate)] = userInteraction.InstagramClicks;
					uiArrs.TwitterClicks[GetTimescaleIndexer(timescale, userInteraction.GroupDate)] = userInteraction.TwitterClicks;
				});


			return uiArrs;
		}


	}

}
