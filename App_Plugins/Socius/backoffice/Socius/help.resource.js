angular.module('umbraco.resources').factory('SociusHelpResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "Socius/Help";

  return {

    GetFaq: () => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetFaq`),
        'Failed to get Socius changelog'
      );
    },

  }

}); 