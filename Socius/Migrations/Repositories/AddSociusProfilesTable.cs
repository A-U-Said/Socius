using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Socius.Migrations.Repositories
{
	public class AddSociusProfilesTable : MigrationBase
	{
		public AddSociusProfilesTable(IMigrationContext context) : base(context)
		{
		}

		protected override void Migrate()
		{
			Logger.LogDebug("Running migration {MigrationStep}", "AddSociusProfilesTable");

			if (TableExists("SociusProfiles") == false)
			{
				Create.Table<SociusProfilesSchema>().Do();
			}
			else
			{
				Logger.LogDebug("The database table {DbTable} already exists, skipping", "SociusProfiles");
			}
		}
	}

}
