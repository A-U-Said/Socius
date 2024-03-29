﻿using Newtonsoft.Json;
using Socius.Dto.Views.Feeds;
using Socius.Models.ApiResponses.Facebook;
using Socius.Models.ApiResponses.Instagram;
using Socius.Models.ApiResponses.Twitter;
using Socius.Repositories;
using System.Net;
using System.Net.Http.Headers;

namespace Socius.Helpers
{
	public class SociusFeedHelper : ISociusFeedHelper
	{
		private readonly ISociusProfileRepository _sociusProfileRepository;

		public SociusFeedHelper(ISociusProfileRepository sociusProfileRepository)
		{ 
			_sociusProfileRepository = sociusProfileRepository;
		}


		public SociusFeedView GetSociusFeeds(int profileId)
		{
			var sociusFeed = new SociusFeedView();

			var sociusProfile = _sociusProfileRepository.GetProfileDetails(profileId).Result;
			if (sociusProfile == null)
			{
				return sociusFeed;
			}

			var SmFeedsClientHandler = new HttpClientHandler()
			{
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};

			var SmFeedsClient = new HttpClient(SmFeedsClientHandler);

			if (sociusProfile.Facebook.IsComplete())
			{
				var fbDetails = sociusProfile.Facebook;
				HttpResponseMessage fbResponse = SmFeedsClient.GetAsync($"https://graph.facebook.com/{fbDetails.PageID}?fields=posts{{full_picture,updated_time,permalink_url,message}},username&access_token={fbDetails.Token}").Result;
				if (fbResponse.IsSuccessStatusCode)
				{
					Stream fbResponseStream = fbResponse.Content.ReadAsStream();
					StreamReader fbResponseReader = new StreamReader(fbResponseStream);
					var fbResponseObj = JsonConvert.DeserializeObject<FacebookFeedResponse>(fbResponseReader.ReadToEnd());
					sociusFeed.FacebookPosts = fbResponseObj?.Posts?.Data.Select(x => new SocialMediaPostView(x)).ToList();
				}
			}

			if (sociusProfile.Instagram.IsComplete())
			{
				var igDetails = sociusProfile.Instagram;
				HttpResponseMessage igResponse = SmFeedsClient.GetAsync($"https://graph.instagram.com/me?fields=media{{id,media_url,permalink,caption,timestamp}},username&access_token={igDetails.IgToken}").Result;
				if (igResponse.IsSuccessStatusCode)
				{
					Stream igResponseStream = igResponse.Content.ReadAsStream();
					StreamReader igResponseReader = new StreamReader(igResponseStream);
					var igResponseObj = JsonConvert.DeserializeObject<InstagramFeedResponse>(igResponseReader.ReadToEnd());
					sociusFeed.InstagramPosts = igResponseObj?.Media?.Posts.Select(x => new SocialMediaPostView(x)).ToList();
				}
			}

			SmFeedsClient.Dispose();

			if (sociusProfile.Twitter.IsComplete())
			{
				var twDetails = sociusProfile.Twitter;

				var TwClientHandler = new HttpClientHandler()
				{
					AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
				};
				TwClientHandler.PreAuthenticate = true;
				HttpClient TwClient = new HttpClient(TwClientHandler);
				TwClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", twDetails.TwToken);

				HttpResponseMessage twResponse = TwClient.GetAsync($"https://api.twitter.com/2/users/{twDetails.TwUserID}/tweets?max_results=5&tweet.fields=attachments,created_at&expansions=attachments.media_keys,author_id&media.fields=media_key,preview_image_url,url").Result;
				if (twResponse.IsSuccessStatusCode)
				{
					Stream twResponseStream = twResponse.Content.ReadAsStream();
					StreamReader twResponseReader = new StreamReader(twResponseStream);
					var twResponseObj = JsonConvert.DeserializeObject<TwitterFeedResponse>(twResponseReader.ReadToEnd());
					sociusFeed.TwitterPosts = twResponseObj?.Posts.Select(x => new SocialMediaPostView(x, twResponseObj.Includes)).ToList();
				}

				TwClient.Dispose();
			}


			return sociusFeed;
		}
	}
}
