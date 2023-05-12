angular.module("umbraco").controller("Socius.ProfileListController", function ($scope, $location, appState, listViewHelper, SociusProfilesResource) {
	
	var vm = this;
	var currentSection = appState.getSectionState("currentSection");
	
	vm.loading = false;
	
	vm.filter = { searchTerm: "" };

	vm.layouts = [
		{
			"icon": "icon-thumbnails-small",
			"path": "1",
			"selected": true
		},
		{
			"icon": "icon-list",
			"path": "2",
			"selected": true
		}
	];

	vm.profiles = [];
	
	vm.activeLayout = listViewHelper.getLayout("sociusProfilesList", vm.layouts);
	
	vm.init = () => {
		vm.loading = true;

		SociusProfilesResource.GetProfiles()
		.then(data => vm.profiles = data);
		
		vm.loading = false;
	}
	
	vm.openProfile = (profileId) => {
		$location.path(`/${currentSection}/socius/profile/${profileId}`);
	}
	
	vm.createNewProfile = () => {
		$location.path(`/${currentSection}/socius/profile/new`);
	}
	
	vm.selectLayout = (selectedLayout) => {
		vm.activeLayout = listViewHelper.setLayout("sociusProfilesList", selectedLayout, vm.layouts);
	}
	
	vm.init();
});