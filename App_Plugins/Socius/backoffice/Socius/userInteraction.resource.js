angular.module('umbraco.resources').factory('SociusInteractionResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "Socius/UserInteraction";

  return {

    GetInteractions: (profileId, timescale) => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetInteractions?profile=${profileId}&timescale=${timescale}`),
        'Failed to get Socius user interaction'
      );
    },

  }

}); 