using Newtonsoft.Json;


namespace Socius.Models.ApiResponses.Instagram
{
    public class InstagramFeedResponse
    {
        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Media
    {
        [JsonProperty("data")]
        public List<InstagramPost> Posts { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public class InstagramPost
	{
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }

	public class Paging
	{
		[JsonProperty("cursors")]
		public Cursors Cursors { get; set; }

		[JsonProperty("next")]
		public string Next { get; set; }
	}

	public class Cursors
	{
		[JsonProperty("before")]
		public string Before { get; set; }

		[JsonProperty("after")]
		public string After { get; set; }
	}
}
