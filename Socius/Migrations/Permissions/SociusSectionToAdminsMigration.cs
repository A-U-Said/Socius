using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Migrations;


namespace Socius.Migrations.Permissions
{
    public class SociusSectionToAdminsMigration : MigrationBase
    {
        private readonly IUmbracoContextFactory _context;
        private readonly ICoreScopeProvider _scopeProvider;
        private readonly IUserService _userService;

        public SociusSectionToAdminsMigration(
            IMigrationContext context,
            IUmbracoContextFactory umbracoContextFactory,
            ICoreScopeProvider scopeProvider,
            IUserService userService)
            : base(context)
        {
            _userService = userService;
            _context = umbracoContextFactory;
            _scopeProvider = scopeProvider;
        }

        protected override void Migrate()
        {
            using UmbracoContextReference umbracoContextReference = _context.EnsureUmbracoContext();
            using var scope = _scopeProvider.CreateCoreScope();
            var adminGroup = _userService.GetUserGroupByAlias(Constants.Security.AdminGroupAlias);

            if (adminGroup == null)
            {
                return;
            }

            adminGroup.AddAllowedSection(SociusConstants.Application.SectionAlias);

            _userService.Save(adminGroup);

            scope.Complete();
        }
    }
}
