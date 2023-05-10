using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;

namespace Socius.Migrations.Permissions
{
	public class SociusPermissionsComposer : ComponentComposer<SociusPermissionsComponent>
	{
	}

	public class SociusPermissionsComponent : IComponent
	{
		private readonly ICoreScopeProvider _coreScopeProvider;
		private readonly IMigrationPlanExecutor _migrationPlanExecutor;
		private readonly IKeyValueService _keyValueService;
		private readonly IRuntimeState _runtimeState;

		public SociusPermissionsComponent(
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

			var migrationName = $"Socius-Permissions-{Guid.NewGuid()}";
			var migrationPlan = new MigrationPlan(migrationName);

			migrationPlan.From(string.Empty)
				.To<SociusSectionToAdminsMigration>("AddedSociusSectionForAdmins");

			var upgrader = new Upgrader(migrationPlan);
			upgrader.Execute(_migrationPlanExecutor, _coreScopeProvider, _keyValueService);
		}

		public void Terminate()
		{
		}
	}

}
