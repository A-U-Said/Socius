﻿using Socius.Models.Repositories;

namespace Socius.Dto.Views.Profiles
{
    public class SociusProfileDetailView : SociusProfileListView
    {
		public SociusProfileDetailView(SociusProfilesSchema dbResult): base(dbResult)
        {
			CreatedBy = dbResult.Creator.UserName;
			CreateDate = dbResult.CreateDate;
			UpdatedBy = dbResult.Updater.UserName;
			Feeds = new FeedsView(dbResult.Facebook, dbResult.Instagram, dbResult.Twitter);
        }

		public string CreatedBy { get; protected set; }
		public DateTime CreateDate { get; protected set; }
		public string UpdatedBy { get; protected set; }
		public FeedsView? Feeds { get; protected set; }

	}

}
