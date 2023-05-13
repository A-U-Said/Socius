angular.module("umbraco").controller("Socius.ProfileController", function ($scope, $location, $routeParams, formHelper, Upload, mediaHelper, appState, overlayService, SociusProfilesResource) {
	
	var vm = this;
	var profileId = $routeParams.id;
	var currentSection = appState.getSectionState("currentSection");
	
	vm.isNewProfile = (profileId == "new");
	
	vm.loading = false;

	vm.page = {};
	vm.page.saveButtonState = "success";

	vm.maxFileSize = Umbraco.Sys.ServerVariables.umbracoSettings.maxFileSize + "KB";
	vm.acceptedFileTypes = mediaHelper.formatFileTypes(Umbraco.Sys.ServerVariables.umbracoSettings.imageFileTypes);

	vm.profile = {
		name: null,
		profileImage: null,
		createdBy: null,
		createDate: null,
		updatedBy: null,
		updateDate: null,
		feeds: {
			facebook: {
				appId: null,
				clientSecret: null,
				pageID: null,
				token: null
			},
			instagram: {
				clientId: null,
				clientSecret: null,
				redirectUri: null,
				token: null,
				tokenExpiry: null
			},
			twitter: {
				userID: null,
				token: null
			}
		}
	};

	vm.breadcrumbs = [];

	vm.init = () => {
		if (!vm.isNewProfile) {
			vm.loading = true;

			SociusProfilesResource.GetProfile(profileId)
			.then(data => {
				vm.profile = data;
				createBreadcrumbs();
			});
			
			vm.loading = false;
		}
		else {
			createBreadcrumbs();
		}
	}

	const createBreadcrumbs = () => {
		vm.breadcrumbs = [
			{
				"name": "profiles",
				"path": "/socius/socius/profiles"
			},
			{
				"name": !vm.isNewProfile ? vm.profile.name : ""
			}
		]
	}
	
	vm.goToPage = (ancestor) => {
		$location.path(ancestor.path);
	};
	
	vm.save = async () => {
		if (formHelper.submitForm({ scope: $scope })) {
			if (!vm.isNewProfile) {
				update();
			}
			else {
				var newProfileId = await create();
			}
		}
		if (newProfileId != null) {
			formHelper.resetForm({ scope: $scope });
			$location.path(`/${currentSection}/socius/profile/${newProfileId}`);
		}
	};

	const update = () => {
		vm.page.saveButtonState = "busy";
		SociusProfilesResource.UpdateProfile(profileId, vm.profile)
		.then(data => {
			vm.page.saveButtonState = "success";
		})
		.catch(error => {
			vm.page.saveButtonState = "error";
		});
	}

	const create = () => {
		vm.page.saveButtonState = "busy";
		return SociusProfilesResource.CreateProfile(vm.profile)
		.then(data => {
			vm.page.saveButtonState = "success";
			return data;
		})
		.catch(error => {
			vm.page.saveButtonState = "error";
			return null;
		});
	}

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

	vm.ensureNumberOnly = (ev) => {
		if (ev.charCode >= 48 && ev.charCode <= 57) {
			return;
		}
		ev.preventDefault();
	}

	vm.deleteProfile = () => {
		const overlay = {
			view: "confirm",
			title: "Delete Profile",
			content: "This will permanently delete this profile and any page feeds using it will no longer work. Are you sure that you want to do this?",
			closeButtonLabel: "Cancel",
			submitButtonLabel: "Delete",
			submitButtonStyle: "danger",
			close: () => {
				overlayService.close();
			},
			submit: () => {
				SociusProfilesResource.DeleteProfile(vm.profile.id)
				.then(data => {
					vm.goToPage(vm.breadcrumbs[0]);
				})
				.catch(error => {
					console.log(error);
				});
				overlayService.close();
			}
		};
		overlayService.open(overlay);
	}

	vm.init();
});