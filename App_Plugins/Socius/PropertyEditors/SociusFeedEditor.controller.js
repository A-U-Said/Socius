angular.module('umbraco').controller('Socius.FeedEditorController', function ($scope, SociusProfilesResource) {

	if (!$scope.model.config) {
		$scope.model.config = {};
	}

	SociusProfilesResource.GetProfiles()
	.then(data => {
		$scope.sociusProfiles = data;
	});

});