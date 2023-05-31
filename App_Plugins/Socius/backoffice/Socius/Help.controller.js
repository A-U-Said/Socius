angular.module("umbraco").controller("Socius.HelpController", function ($scope, SociusHelpResource) {
	
	var vm = this;

	vm.loading = false;
	vm.page = {};
	vm.page.name = "Help";

	vm.faq = [];

	vm.init = () => {
		SociusHelpResource.GetFaq()
		.then(data => {
			vm.faq = data;
		})
		.catch(error => {
		});
	}

	vm.init();
	
});