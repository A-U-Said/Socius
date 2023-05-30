using Newtonsoft.Json;
using Socius.Extensions;
using Socius.Models.Repositories;
using Socius.Repositories;
using Socius.Models.ApiResponses.Instagram;
using System.Net;

namespace Socius.Helpers
{
    public class InstagramHelper : IInstagramHelper
	{
		private readonly IInstagramCredentialsRepository _repository;
		private readonly ILogger<InstagramHelper> _logger;

		public InstagramHelper(
			IInstagramCredentialsRepository repository,
			ILogger<InstagramHelper> logger)
		{
			_repository = repository;
			_logger = logger;
		}


		public bool IsCompleteProfile(InstagramCredentialsSchema storedCredentials)
		{
			if (storedCredentials == null
				|| storedCredentials.IgClientId == null
				|| storedCredentials.IgClientSecret == null
				|| storedCredentials.IgRedirectUri == null)
			{
				return false;
			}
			return true;
		}

		private async Task<InstagramShortTokenResponse?> GetShortToken(string igAuthCode, InstagramCredentialsSchema storedCredentials)
		{
			if (!IsCompleteProfile(storedCredentials))
			{
				throw new ArgumentNullException(nameof(storedCredentials));
			}

			var client = new HttpClient();
			var formValues = new Dictionary<string, string>
			{
				{ "client_id", storedCredentials.IgClientId!.Value.ToString()},
				{ "client_secret", storedCredentials.IgClientSecret! },
				{ "grant_type", "authorization_code" },
				{ "redirect_uri", storedCredentials.IgRedirectUri! },
				{ "code", igAuthCode },
			};
			var formBodyContent = new FormUrlEncodedContent(formValues);

			try
			{
				var response = await client.PostAsync(SociusConstants.ApiUrls.InstagramShortTokenUrl, formBodyContent);
				client.Dispose();
				var responseString = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<InstagramShortTokenResponse>(responseString);
			}
			catch (HttpRequestException ex)
			{
				_logger.LogError("Error getting short Instagram token | {ex}", ex.Message);
				return null;
			}
		}

		public async Task<InstagramLongTokenResponse?> GetToken(int sociusProfileId, string igAuthCode)
		{
			var storedCredentials = await _repository.GetSingle(sociusProfileId).ThrowIfNull("Socius profile cannot be found");
			var shortToken = await GetShortToken(igAuthCode, storedCredentials!).ThrowIfNull("Cannot generate Instagram short token");
			var igTokenClientHandler = new HttpClientHandler()
			{ 
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate 
			};
			var igLongTokenClient = new HttpClient(igTokenClientHandler);
			HttpResponseMessage igTokenResponse = await igLongTokenClient.GetAsync($"{SociusConstants.ApiUrls.InstagramLongTokenUrl}&client_secret={storedCredentials!.IgClientSecret}&access_token={shortToken!.AccessToken}");
			
			igLongTokenClient.Dispose();

			if (igTokenResponse.IsSuccessStatusCode)
			{
				Stream igTokenResponseStream = await igTokenResponse.Content.ReadAsStreamAsync();
				var igTokenResponseReader = new StreamReader(igTokenResponseStream);

				return JsonConvert.DeserializeObject<InstagramLongTokenResponse>(igTokenResponseReader.ReadToEnd());
			}
			else
			{
				var ex = await igTokenResponse.Content.ReadAsStringAsync();
				_logger.LogError("Error getting long Instagram token | {ex}", ex);
				return null;
			}
		}

		public async Task<DateTime?> RefreshInstagramToken(int sociusProfileId)
		{
			var storedCredentials = await _repository.GetSingle(sociusProfileId).ThrowIfNull("Socius profile cannot be found");
			if (storedCredentials!.IgToken == null)
			{
				return null;
			}
			var igRefreshClientHandler = new HttpClientHandler()
			{ 
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};
			var igRefreshClient = new HttpClient(igRefreshClientHandler);
			HttpResponseMessage IgTokenResponse = await igRefreshClient.GetAsync($"{SociusConstants.ApiUrls.InstagramRefreshUrl}&access_token={storedCredentials.IgToken}");
			
			if (IgTokenResponse.IsSuccessStatusCode)
			{
				Stream IgTokenResponseStream = await IgTokenResponse.Content.ReadAsStreamAsync();
				var IgTokenResponseReader = new StreamReader(IgTokenResponseStream);

				var refreshInstaResponse = JsonConvert.DeserializeObject<InstagramRefreshResponse>(IgTokenResponseReader.ReadToEnd());
				if (refreshInstaResponse == null)
				{
					return null;
				}

				storedCredentials.IgToken = refreshInstaResponse.AccessToken;
				storedCredentials.IgTokenExpiry = DateTime.Now.AddSeconds(refreshInstaResponse.ExpiresIn);

				await _repository.Update(storedCredentials);

				return storedCredentials.IgTokenExpiry;
			}
			else
			{
				var ex = await IgTokenResponse.Content.ReadAsStringAsync();
				_logger.LogError("Error occurred refreshing Instagram token: {ex}", ex);
				return null;
			}
		}
	}
}
