using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Socius.Migrations.Repositories
{
	public class AddInstagramCredentialsTable : MigrationBase
    {
        public AddInstagramCredentialsTable(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddInstagramCredentialsTable");

            if (TableExists("InstagramCredentials") == false)
            {
                Create.Table<InstagramCredentialsSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "InstagramCredentials");
            }
        }
    }

}
