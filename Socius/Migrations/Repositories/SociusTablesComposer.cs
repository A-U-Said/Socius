using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;


namespace Socius.Migrations.Repositories
{
    public class SociusTablesComposer : ComponentComposer<SociusTablesComponent>
    {
    }

    public class SociusTablesComponent : IComponent
    {
        private readonly ICoreScopeProvider _coreScopeProvider;
        private readonly IMigrationPlanExecutor _migrationPlanExecutor;
        private readonly IKeyValueService _keyValueService;
        private readonly IRuntimeState _runtimeState;

        public SociusTablesComponent(
            ICoreScopeProvider coreScopeProvider,
            IMigrationPlanExecutor migrationPlanExecutor,
            IKeyValueService keyValueService,
            IRuntimeState runtimeState)
        {
            _coreScopeProvider = coreScopeProvider;
            _migrationPlanExecutor = migrationPlanExecutor;
            _keyValueService = keyValueService;
            _runtimeState = runtimeState;
        }

        public void Initialize()
        {
            if (_runtimeState.Level < RuntimeLevel.Run)
            {
                return;
            }

            var migrationName = $"Socius-Tables-{Guid.NewGuid()}";
            var migrationPlan = new MigrationPlan(migrationName);

            migrationPlan.From(string.Empty)
                .To<AddSociusProfilesTable>("Socius-profiles-table");

			migrationPlan.From("Socius-profiles-table")
				.To<AddFacebookCredentialsTable>("Socius-fb-table");

            migrationPlan.From("Socius-fb-table")
                .To<AddTwitterCredentialsTable>("Socius-tw-table");

            migrationPlan.From("Socius-tw-table")
                .To<AddInstagramCredentialsTable>("Socius-ig-table");

            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_migrationPlanExecutor, _coreScopeProvider, _keyValueService);
        }

        public void Terminate()
        {
        }
    }
}
