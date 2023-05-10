angular.module("umbraco").controller("Socius.UserInteraction", function ($scope) {
	
	var vm = this;
	var sociusViewFolder = `${Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath}/socius/backoffice/socius`;
	
	vm.loading = false;

  vm.page = {};
	vm.page.name = "User Interaction";

});