using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Socius.Migrations.Repositories
{
	public class AddTwitterCredentialsTable : MigrationBase
    {
        public AddTwitterCredentialsTable(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddTwitterCredentialsTable");

            if (TableExists("TwitterCredentials") == false)
            {
                Create.Table<TwitterCredentialsSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "TwitterCredentials");
            }
        }
    }

}
