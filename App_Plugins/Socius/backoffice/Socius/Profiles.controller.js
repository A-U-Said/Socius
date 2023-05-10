angular.module("umbraco").controller("Socius.ProfilesController", function ($scope, $location, appState, navigationService, listViewHelper, usersResource, $routeParams) {
	
	var vm = this;
	var sociusViewFolder = `${Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath}/socius/backoffice/socius`;

	vm.loading = false;

	vm.page = {};
	vm.page.name = "Socius Profiles";
	vm.page.navigation = [
		{
			"name": "Profiles",
			"alias": "profilesList",
			"icon": "icon-facebook-like",
			"view": `${sociusViewFolder}/profileList.html`,
			"active": true,
		},
		{
			"name": "Help",
			"alias": "profilesHelp",
			"icon": "icon-info",
			"view": `${sociusViewFolder}/profilesHelp.html`,
			"active": false,
		}
	];

});