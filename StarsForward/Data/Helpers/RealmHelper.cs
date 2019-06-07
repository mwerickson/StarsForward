using Realms;
using Xamarin.Essentials;

namespace StarsForward.Data.Helpers
{
    public class RealmHelper
    {
        public const ulong DbSchemaVersion = 24;

        public static Realm GetInstance()
        {
            var dataPath = FileSystem.AppDataDirectory;
            var dbFile = $"{dataPath}/StarsForward.realm";

            var config = new RealmConfiguration(dbFile) { SchemaVersion = DbSchemaVersion };

            config.MigrationCallback = (migration, version) => { };

            return Realm.GetInstance(config);
        }
    }
}