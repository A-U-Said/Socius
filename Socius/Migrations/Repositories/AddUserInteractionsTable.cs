using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Socius.Migrations.Repositories
{
	public class AddUserInteractionsTable : MigrationBase
	{
		public AddUserInteractionsTable(IMigrationContext context) : base(context)
		{
		}

		protected override void Migrate()
		{
			Logger.LogDebug("Running migration {MigrationStep}", "AddUserInteractionsTable");

			if (TableExists("UserInteractions") == false)
			{
				Create.Table<UserInteractionsSchema>().Do();
			}
			else
			{
				Logger.LogDebug("The database table {DbTable} already exists, skipping", "UserInteractions");
			}
		}
	}


}
