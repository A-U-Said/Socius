angular.module('umbraco.resources').factory('SociusChangelogResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "Socius/Changelog";

  return {

    GetChanges: () => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetChanges`),
        'Failed to get Socius changelog'
      );
    },

  }

}); 