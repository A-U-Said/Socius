angular.module('umbraco.resources').factory('SociusProfilesResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "Socius/Profiles";

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

  }

}); 