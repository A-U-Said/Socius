using Socius.Dto.Commands;
using Socius.Models.Repositories;
using Socius.Repositories;
using System.Security.Cryptography;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Extensions;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Strings;

namespace Socius.Helpers
{
	//Don't like doing long logic in controllers

	public class SociusProfilesHelper : ISociusProfilesHelper
	{
		private readonly ISociusProfileRepository _repository;
		private readonly IFacebookCredentialsRepository _facebookCredentialsRepository;
		private readonly IInstagramCredentialsRepository _instagramCredentialsRepository;
		private readonly ITwitterCredentialsRepository _twitterCredentialsRepository;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IShortStringHelper _shortStringHelper;
		private readonly MediaFileManager _mediaFileManager;
		private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;

		public SociusProfilesHelper(
			ISociusProfileRepository repository,
			IFacebookCredentialsRepository facebookCredentialsRepository,
			IInstagramCredentialsRepository instagramCredentialsRepository,
			ITwitterCredentialsRepository twitterCredentialsRepository,
			IWebHostEnvironment hostingEnvironment,
			IShortStringHelper shortStringHelper,
			MediaFileManager mediaFileManager,
			IBackOfficeSecurityAccessor backOfficeSecurityAccessor)
		{
			_repository = repository;
			_facebookCredentialsRepository = facebookCredentialsRepository;
			_instagramCredentialsRepository = instagramCredentialsRepository;
			_twitterCredentialsRepository = twitterCredentialsRepository;
			_hostingEnvironment = hostingEnvironment;
			_shortStringHelper = shortStringHelper;
			_mediaFileManager = mediaFileManager;
			_backOfficeSecurityAccessor = backOfficeSecurityAccessor;
		}


		public async Task<string?> SetProfileImage(int profileId, IList<IFormFile> file)
		{
			if (file == null || file.Count == 0)
			{
				return null;
			}

			var root = _hostingEnvironment.MapPathContentRoot(Constants.SystemDirectories.TempFileUploads);
			Directory.CreateDirectory(root);

			var profile = await _repository.GetSingle(profileId);
			if (profile == null)
			{
				return null;
			}

			IFormFile newImage = file.First();
			var fileName = newImage.FileName.Trim(new[] { '\"' }).TrimEnd();
			var safeFileName = fileName.ToSafeFileName(_shortStringHelper);
			var ext = safeFileName.Substring(safeFileName.LastIndexOf('.') + 1).ToLower();

			if (SociusConstants.Media.AllowedImageExtensions.Contains(ext) == true)
			{
				var newImagePath = $"{SociusConstants.Media.AvatarDirectory}/{(profile.Id + safeFileName).GenerateHash<SHA1>()}.{ext}";

				profile.SetProfileImage(newImagePath);

				using Stream fs = newImage.OpenReadStream();
				_mediaFileManager.FileSystem.AddFile(newImagePath, fs, true);

				await _repository.Update(profile);
			}

			return profile.ProfileImage;
		}


		private async Task UpdateAction<TSchema, TCommand>(int profileId, TSchema? dbRecord, TCommand? command, ISociusRepository<TSchema> repo)
			where TSchema : SociusUpdateCommand<TCommand>, ISociusSchema
			where TCommand : ISociusUpdateCommand
		{
			if ((dbRecord != null) && (command != null)) //Update
			{
				dbRecord.Update(command);
				await repo.Update(dbRecord);
			}

			if ((dbRecord == null) && (command != null)) //Should never need to create as feed records are created at profile creation. This is just in case someone deletes a record from the DB.
			{
				var newRecord = (TSchema)Activator.CreateInstance(typeof(TSchema), profileId, command);
				await repo.Create(newRecord);
			}
		}


		public async Task UpdateProfile(int profileId, SaveProfileCommand profile)
		{
			var userId = _backOfficeSecurityAccessor.BackOfficeSecurity?.CurrentUser?.Id ?? -1;

			profile.AddUpdater(userId);

			var profileDetails = await _repository.GetProfileDetails(profileId);
			if (profileDetails == null)
			{
				return;
			}

			var facebookDetails = await _facebookCredentialsRepository.GetSingle(profileId);
			await UpdateAction(profileId, facebookDetails, profile.Feeds?.Facebook, _facebookCredentialsRepository);

			var instagramDetails = await _instagramCredentialsRepository.GetSingle(profileId);
			await UpdateAction(profileId, instagramDetails, profile.Feeds?.Instagram, _instagramCredentialsRepository);

			var twitterDetails = await _twitterCredentialsRepository.GetSingle(profileId);
			await UpdateAction(profileId, twitterDetails, profile.Feeds?.Twitter, _twitterCredentialsRepository);

			profileDetails.Update(profile);
			await _repository.Update(profileDetails);
		}


		//Would love to be able to update multiple tables using references, nested objects, and a single aggregate root, but apparently not.
		//https://github.com/schotime/NPoco/issues/483
		public async Task<SociusProfilesSchema> CreateProfile(SaveProfileCommand newProfileDetails)
		{
			var userId = _backOfficeSecurityAccessor.BackOfficeSecurity?.CurrentUser?.Id ?? -1;

			var newProfile = new SociusProfilesSchema(newProfileDetails, userId);
			var createdProfile = await _repository.Create(newProfile);

			var newFbRecord = new FacebookCredentialsSchema(createdProfile.Id, newProfileDetails.Feeds.Facebook);
			await _facebookCredentialsRepository.Create(newFbRecord);

			var newIgRecord = new InstagramCredentialsSchema(createdProfile.Id, newProfileDetails.Feeds.Instagram);
			await _instagramCredentialsRepository.Create(newIgRecord);

			var newTwRecord = new TwitterCredentialsSchema(createdProfile.Id, newProfileDetails.Feeds.Twitter);
			await _twitterCredentialsRepository.Create(newTwRecord);

			return createdProfile;
		}


		public async Task<TaskStatus> DeleteProfile(int profileId)
		{
			var profileDetails = await _repository.GetProfileDetails(profileId);
			if (profileDetails == null)
			{
				return TaskStatus.Faulted;
			}

			await _repository.Delete(profileDetails);

			return TaskStatus.RanToCompletion;
		}
	}
}
