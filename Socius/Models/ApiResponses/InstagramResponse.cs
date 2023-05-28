using Newtonsoft.Json;

namespace Socius.Models.ApiResponses
{
	public class InstagramShortTokenResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("user_id")]
		public string UserId { get; set; }
	}

	public class InstagramLongTokenResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }
	}

	public class InstagramRefreshResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("Expires_in")]
		public int ExpiresIn { get; set; }

		public DateTime ExpiryDate => DateTimeOffset.FromUnixTimeSeconds(ExpiresIn).DateTime;
	}
}
