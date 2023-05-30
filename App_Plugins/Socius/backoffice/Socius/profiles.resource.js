angular.module('umbraco.resources').factory('SociusProfilesResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "Socius/Profiles";
  const instagramBaseUrl = "Socius/Instagram";
  const facebookBaseUrl = "Socius/Facebook";

  return {

    GetProfileImageUploadUri: (profileId) => `/Umbraco/Socius/Profiles/SetProfileImage?profileId=${profileId}`,

    GetProfiles: () => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetProfiles`),
        'Failed to get Socius profiles'
      );
    },

    GetProfile: (profileId) => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetProfile?profileId=${profileId}`),
        'Failed to get Socius profile'
      );
    },

    UpdateProfile: (profileId, profileData) => {
      return umbRequestHelper.resourcePromise(
        $http.post(`${baseUrl}/UpdateProfile?profileId=${profileId}`, profileData),
        'Failed to update Socius profile'
      );
    },

    CreateProfile: (profileData) => {
      return umbRequestHelper.resourcePromise(
        $http.post(`${baseUrl}/CreateProfile`, profileData),
        'Failed to create Socius profile'
      );
    },

    DeleteProfile: (profileId) => {
      return umbRequestHelper.resourcePromise(
        $http.delete(`${baseUrl}/DeleteProfile?profileId=${profileId}`),
        'Failed to delete Socius profile'
      );
    },

    ClearIgToken: (profileId) => {
      return umbRequestHelper.resourcePromise(
        $http.delete(`${instagramBaseUrl}/ClearIgToken?profileId=${profileId}`),
        'Failed to clear Instagram token'
      );
    },

    RefreshIgToken: (profileId) => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${instagramBaseUrl}/RefreshIgToken?profileId=${profileId}`),
        'Failed to refresh Instagram token'
      );
    },

    CreateIgValidationKey: (profileId) => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${instagramBaseUrl}/CreateIgValidationKey?profileId=${profileId}`),
        'Failed to create Instagram validation key'
      );
    },

    ClearFbToken: (profileId) => {
      return umbRequestHelper.resourcePromise(
        $http.delete(`${facebookBaseUrl}/ClearFbToken?profileId=${profileId}`),
        'Failed to clear Facebook token'
      );
    },

    ExchangeFbUserToken: (profileId, shortUserToken) => {
      return umbRequestHelper.resourcePromise(
        $http.post(`${facebookBaseUrl}/GetPageToken?profileId=${profileId}`, shortUserToken),
        'Failed get Facebook page token'
      );
    }

  }

}); 