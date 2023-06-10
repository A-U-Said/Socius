angular.module("umbraco").controller("Socius.UserInteraction", function ($scope, SociusProfilesResource, SociusInteractionResource) {
	
	var vm = this;
	var sociusViewFolder = `${Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath}/socius/backoffice/socius`;
	
	vm.loading = false;

  vm.page = {};
	vm.page.name = "User Interaction";

	vm.profiles = [];
	vm.selectedProfile = null;

	vm.chartData = {
		labels: [],
		facebookClicks: [],
		instagramClicks: [],
		twitterClicks: [],
	};

	vm.timescales = [
		{
			id: 0,
			name: "Day"
		},
		{
			id: 1,
			name: "Week"
		},
		{
			id: 2,
			name: "Month"
		},
		{
			id: 3,
			name: "Year"
		},
		{
			id: 4,
			name: "All Time"
		},
	]
	vm.selectedTimescale = null;

	vm.getInteractions = () => {
		if (vm.selectedProfile != null && vm.selectedTimescale != null)
		{
			SociusInteractionResource.GetInteractions(vm.selectedProfile, vm.selectedTimescale)
			.then(data => {
				vm.chartData = data;
			})
			.catch(error => {
			});
		}
	}

	vm.init = () => {
		vm.loading = true;

		SociusProfilesResource.GetProfiles()
		.then(data => vm.profiles = data);
		
		vm.loading = false;
	}

	vm.init();

})
.directive('interactionChart', function() {
	
	function link(scope, element, attrs) {

		var chart;

		function renderChart(canvasElement, chartData) {
			chart = new Chart(canvasElement, {
				type: "line",
				data: {
					labels: chartData.labels,
					datasets: [{ 
						label: "Facebook",
						data: chartData.facebookClicks,
						borderColor: "#4267B2",
						fill: false
					}, { 
						label: "Instagram",
						data: chartData.instagramClicks,
						borderColor: "#C13584",
						fill: false
					}, { 
						label: "Twitter",
						data: chartData.twitterClicks,
						borderColor: "#1DA1F2",
						fill: false
					}]
				},
				options: {
					legend: {
						display: true,
						position: 'right',
						labels: {
							padding: 40
						}
					},
					scales: {
						yAxes: [{
							ticks: {
								beginAtZero: true
							}
						}]
					},
				}
			});
		}

		attrs.$observe("data", function(value) {
			chart && chart.destroy();
			chartData = JSON.parse(value);
			renderChart(element[0], chartData);
		});

	}
	
	return {
		link: link
	}
});