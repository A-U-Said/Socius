angular.module("umbraco").controller("Socius.ProfilesController", function ($scope) {
	
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
			"name": "Import/Export",
			"alias": "profilesImportExport",
			"icon": "icon-cloud-upload",
			"view": `${sociusViewFolder}/profilesImportExport.html`,
			"active": false,
		}
	];

});