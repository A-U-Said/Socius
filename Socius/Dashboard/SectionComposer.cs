using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.Sections;


namespace Socius.Dashboard
{
	public class SectionComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddSection<SociusSection>();
        }
    }


    public class SociusSection : ISection
    {
        public string Alias => SociusConstants.Application.SectionAlias;
        public string Name => SociusConstants.Application.SectionName;
    }

}