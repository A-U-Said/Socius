using Socius.Models.Repositories;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Socius.Migrations.Repositories
{
	public class AddFacebookCredentialsTable : MigrationBase
    {
        public AddFacebookCredentialsTable(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddFacebookCredentialsTable");

            if (TableExists("FacebookCredentials") == false)
            {
                Create.Table<FacebookCredentialsSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "FacebookCredentials");
            }
        }
    }

   
}
