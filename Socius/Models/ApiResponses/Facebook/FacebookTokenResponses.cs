using Newtonsoft.Json;

namespace Socius.Models.ApiResponses.Facebook
{
    public class FacebookLongTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class FacebookPageTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
