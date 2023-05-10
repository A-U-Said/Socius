angular.module("umbraco").controller("Socius.ProfileController", function ($scope, $location, formHelper, Upload, mediaHelper, SociusProfilesResource) {
	
	var vm = this;
	
	vm.loading = false;

	vm.page = {};
	vm.page.saveButtonState = "success";

	vm.maxFileSize = Umbraco.Sys.ServerVariables.umbracoSettings.maxFileSize + "KB";
	vm.acceptedFileTypes = mediaHelper.formatFileTypes(Umbraco.Sys.ServerVariables.umbracoSettings.imageFileTypes);

	vm.profile = {};

	vm.breadcrumbs = [
		{
				"name": "profiles",
				"path": "/socius/socius/profiles"
		},
		{
				"name": vm.profile.name
		}
	];

	vm.init = () => {
		vm.loading = true;

		SociusProfilesResource.GetProfile(1)
		.then(data => vm.profile = data);
		
		vm.loading = false;
	}
	
	vm.goToPage = (ancestor) => {
		$location.path(ancestor.path);
	};
	
	vm.save = () => {
		vm.page.saveButtonState = "busy";
		vm.profile.updateDate = Date.now();

		if (formHelper.submitForm({ scope: $scope })) {
			SociusProfilesResource.UpdateProfile(1, vm.profile)
			.then(data => {
				console.log(data);
				vm.page.saveButtonState = "success";
			})
			.catch(error => {
				console.log(error);
				vm.page.saveButtonState = "error";
			});
		}
	};

	vm.changeAvatar = (files) => {
		if (files && files.length > 0) {

			Upload.upload({
				url: SociusProfilesResource.GetProfileImageUploadUri(vm.profile.id),
				fields: {},
				file: files[0]
			})
			.success(data => {
				vm.profile.profileImage = data;
			})
			.error((evt, status) => {
				switch(status) {
					case 404:
						console.log("File not found");
						break;
					default:
						console.log(evt.message);
				} 
			});

		}
	};

	vm.clearAvatar = () => {
		vm.profile.profileImage = null;
	}

	vm.init();
});