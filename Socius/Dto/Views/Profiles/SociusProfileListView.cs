using Socius.Models.Repositories;

namespace Socius.Dto.Views.Profiles
{

    public class SociusProfileListView
    {

        public SociusProfileListView(SociusProfilesSchema dbResult)
        {
            var websites = new List<string>();
            if (dbResult.Facebook != null)
            {
                websites.Add("Facebook");
            }
            if (dbResult.Instagram != null)
            {
                websites.Add("Instagram");
            }
            if (dbResult.Twitter != null)
            {
                websites.Add("Twitter");
            }

            Id = dbResult.Id;
            Name = dbResult.Name;
            ProfileImage = dbResult.ProfileImage;
            Websites = websites;
            UpdateDate = dbResult.UpdateDate;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string? ProfileImage { get; protected set; }
        public List<string> Websites { get; protected set; }
        public DateTime UpdateDate { get; protected set; }
    }

}
