﻿using Socius.Models.Shared;

namespace Socius.Dto.Commands
{
	public class RecordInteractionCommand
	{
		public int ProfileId { get; set; }
		public SocialMediaFeedType FeedType { get; set; }
	}
}
