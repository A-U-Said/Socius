angular.module("umbraco").controller("Socius.WelcomeController", function ($scope, $location, appState, SociusChangelogResource) {
	
	var vm = this;
	var currentSection = appState.getSectionState("currentSection");
	
	vm.loading = false;

	vm.page = {
		title: "Socius",
		description: "Social media feeds made easy"
	};

	vm.dashboards = [
		{
			name: "Socius Profiles",
			description: "Manage profiles and social media feeds",
			icon: "icon-share",
			url: "socius/profiles"
		},
		{
			name: "User Interaction",
			description: "See user interaction with your feeds",
			icon: "icon-hand-pointer",
			url: "socius/userInteraction"
		},
		{
			name: "Help",
			description: "FAQ and Help",
			icon: "icon-help-alt",
			url: "socius/help"
		},
	];

	vm.changelog = [];

	vm.goToDashboard = (dashboardUrl) => {
		$location.path(`/${currentSection}/${dashboardUrl}`);
	}

	vm.init = () => {
		SociusChangelogResource.GetChanges()
		.then(data => {
			vm.changelog = data;
		})
		.catch(error => {
		});
	}

	vm.init();
	
});