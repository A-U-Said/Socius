using Newtonsoft.Json;


namespace Socius.Models.ApiResponses.Twitter
{
	public class TwitterFeedResponse
	{
		[JsonProperty("data")]
		public List<TwitterPost> Posts { get; set; }

		[JsonProperty("includes")]
		public Includes Includes { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }
	}

	public class TwitterPost
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("attachments")]
		public Attachments Attachments { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }
	}

	public class Attachments
	{
		[JsonProperty("media_keys")]
		public List<string> MediaKeys { get; set; }
	}

	public class Includes
	{
		[JsonProperty("media")]
		public List<Medium> Media { get; set; }

		[JsonProperty("users")]
		public List<User> Users { get; set; }
	}

	public class User
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }
	}

	public class Medium
	{
		[JsonProperty("media_key")]
		public string MediaKey { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}

	public class Meta
	{
		[JsonProperty("next_token")]
		public string NextToken { get; set; }

		[JsonProperty("result_count")]
		public int ResultCount { get; set; }

		[JsonProperty("newest_id")]
		public string NewestId { get; set; }

		[JsonProperty("oldest_id")]
		public string OldestId { get; set; }
	}
}
