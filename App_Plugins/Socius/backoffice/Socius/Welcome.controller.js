angular.module("umbraco").controller("Socius.WelcomeController", function ($scope, $location, appState) {
	
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
			name: "Idk",
			description: "No idea. I just like having 3",
			icon: "icon-share-alt-2",
			url: "socius/welcome"
		},
	];

	vm.changelog = [
		{
			title: "0.2",
			date: "02/05/2023",
			changes: [
				"Donec et interdum sem. Fusce eleifend gravida nisi, sit amet tempor ligula feugiat et.",
				"Phasellus tincidunt vestibulum elit, eu gravida tellus congue id."
			]
		},
		{
			title: "0.1",
			date: "01/05/2023",
			changes: [
				"Morbi gravida pharetra nulla nec rhoncus.",
				"Lorem ipsum dolor sit amet, consectetur adipiscing elit."
			]
		},
	];

	vm.goToDashboard = (dashboardUrl) => {
		$location.path(`/${currentSection}/${dashboardUrl}`);
	}
	
});