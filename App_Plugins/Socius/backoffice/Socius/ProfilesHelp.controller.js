angular.module("umbraco").controller("Socius.ProfilesHelpController", function ($scope, $location, appState, navigationService, listViewHelper, usersResource, $routeParams) {
	
	var vm = this;

	vm.loading = false;

	vm.faq = [
		{
			question: "Question",
			answer: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque mattis, ex eu lacinia hendrerit, odio nulla ultricies ipsum, fringilla porttitor libero sem et sem. Maecenas quis cursus magna. Duis at luctus felis. Nullam euismod, risus et laoreet eleifend, dolor nisl fringilla tellus, a venenatis tellus ligula vel elit."
		},
		{
			question: "Question",
			answer: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque mattis, ex eu lacinia hendrerit, odio nulla ultricies ipsum, fringilla porttitor libero sem et sem. Maecenas quis cursus magna. Duis at luctus felis. Nullam euismod, risus et laoreet eleifend, dolor nisl fringilla tellus, a venenatis tellus ligula vel elit."
		},
	];
	
});