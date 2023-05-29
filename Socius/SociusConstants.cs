using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Trees;

namespace Socius
{
    public static partial class SociusConstants
    {
		public const string PluginName = "socius";

        public static class Application
        {
			public const string SectionName = "Socius";
			public const string SectionAlias = "socius";

			public const string TreeName = "Socius";
			public const string TreeAlias = "socius";

			public const string WelcomeDashboardName = "Welcome";
			public const string WelcomeDashboardAlias = "welcome";

			public const string ProfilesDashboardName = "Socius Profiles";
			public const string ProfilesDashboardAlias = "sociusProfiles";

			public const string ProfileDashboardName = "Socius Profile";
			public const string ProfileDashboardAlias = "sociusProfile";

			public const string InteractionDashboardName = "User Interaction";
			public const string InteractionDashboardAlias = "userInteraction";

			public const string FeedPropertyEditorName = "Socius Feed";
			public const string FeedPropertyEditorAlias = "Socius.Feed";

			public const string FeedDataTypeName = "Socius Feed";
		}


		public static class Urls
		{
			public const string TreeUrl = $"{Application.SectionAlias}/{Application.TreeAlias}";

			public const string WelcomeUrl = $"{TreeUrl}/welcome";

			public const string ProfilesUrl = $"{TreeUrl}/profiles";

			public const string ProfileUrl = $"{TreeUrl}/profile";

			public const string InteractionUrl = $"{TreeUrl}/userInteraction";

			public const string InstagramCallbackUrl = "umbraco/socius/instagramcallback";
		}

		public static class Media
		{
			public const string AvatarDirectory = "/media/SociusAvatars";
			public const string AllowedImageExtensions = "jpeg,jpg,gif,bmp,png,tiff,tif,webp";
		}

		public static class ApiUrls
		{
			public const string InstagramShortTokenUrl = @"https://api.instagram.com/oauth/access_token";
			public const string InstagramLongTokenUrl = @"https://graph.instagram.com/access_token?grant_type=ig_exchange_token";
			public const string InstagramRefreshUrl = @"https://graph.instagram.com/refresh_access_token?grant_type=ig_refresh_token";

			public const string FacebookApiBaseUrl = @"https://graph.facebook.com";
			public const string FacebookPageTokenUrl = @"https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token";
		}
	}
}