using Newtonsoft.Json;
using Socius.Extensions;
using Socius.Models.ApiResponses;
using Socius.Models.Repositories;
using Socius.Repositories;
using Socius.Socius.Dto.Commands;
using System.Net;

namespace Socius.Helpers
{
	public class FacebookHelper : IFacebookHelper
	{
		private readonly IFacebookCredentialsRepository _repository;
		private readonly ILogger<FacebookHelper> _logger;

		public FacebookHelper(
			IFacebookCredentialsRepository repository,
			ILogger<FacebookHelper> logger)
		{
			_repository = repository;
			_logger = logger;
		}


		private async Task<FacebookLongTokenResponse?> GetLongUserToken(FacebookCredentialsSchema profile, string shortUserToken)
		{
			var fbLongTokenClientHandler = new HttpClientHandler()
			{
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};
			var fbLongTokenClient = new HttpClient(fbLongTokenClientHandler);
			HttpResponseMessage fbLongTokenResponse = await fbLongTokenClient.GetAsync($"{SociusConstants.ApiUrls.FacebookApiBaseUrl}/{profile.PageID}?fields=access_token&access_token={shortUserToken}");

			fbLongTokenClient.Dispose();

			if (fbLongTokenResponse.IsSuccessStatusCode)
			{
				Stream fbLongTokenResponseStream = await fbLongTokenResponse.Content.ReadAsStreamAsync();
				var fbLongTokenResponseReader = new StreamReader(fbLongTokenResponseStream);

				return JsonConvert.DeserializeObject<FacebookLongTokenResponse>(fbLongTokenResponseReader.ReadToEnd());
			}
			else
			{
				var ex = await fbLongTokenResponse.Content.ReadAsStringAsync();
				_logger.LogError("Error getting long Facebook user token | {ex}", ex);
				return null;
			}
		}


		private async Task<FacebookPageTokenResponse?> GetPageToken(FacebookCredentialsSchema profile, string longUserToken)
		{
			var fbPageTokenClientHandler = new HttpClientHandler()
			{
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};
			var fbPageTokenClient = new HttpClient(fbPageTokenClientHandler);
			HttpResponseMessage fbPageTokenResponse = await fbPageTokenClient.GetAsync($"{SociusConstants.ApiUrls.FacebookPageTokenUrl}&client_id={profile.AppId}&client_secret={profile.ClientSecret}&fb_exchange_token={longUserToken}");

			fbPageTokenClient.Dispose();

			if (fbPageTokenResponse.IsSuccessStatusCode)
			{
				Stream fbPageTokenResponseStream = await fbPageTokenResponse.Content.ReadAsStreamAsync();
				var fbPageTokenResponseReader = new StreamReader(fbPageTokenResponseStream);

				return JsonConvert.DeserializeObject<FacebookPageTokenResponse>(fbPageTokenResponseReader.ReadToEnd());
			}
			else
			{
				var ex = await fbPageTokenResponse.Content.ReadAsStringAsync();
				_logger.LogError("Error getting Facebook page token | {ex}", ex);
				return null;
			}
		}


		public async Task<FacebookCredentialsSchema?> UserTokenToPageToken(int sociusProfileId, FacebookUserTokenExchangeCommand shortToken)
		{
			var profile = await _repository.GetSingle(sociusProfileId).ThrowIfNull("Socius profile cannot be found");
			var longToken = await GetLongUserToken(profile!, shortToken.AccessToken).ThrowIfNull("Cannot generate Facebook long lived user token");
			var pageToken = await GetPageToken(profile!, longToken!.AccessToken).ThrowIfNull("Cannot generate Facebook page token");

			profile!.Token = pageToken!.AccessToken;
			await _repository.Update(profile);

			return profile;
		}
	}
}
