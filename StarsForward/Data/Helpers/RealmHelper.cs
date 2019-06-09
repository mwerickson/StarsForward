using System;
using Realms;
using StarsForward.Data.Models;
using Xamarin.Essentials;

namespace StarsForward.Data.Helpers
{
    public class RealmHelper
    {
        public const ulong DbSchemaVersion = 1;

        public static Realm GetInstance()
        {
            try
            {
                var dataPath = FileSystem.AppDataDirectory;
                var dbFile = $"{dataPath}/StarsForward.realm";

                var config = new RealmConfiguration(dbFile) { SchemaVersion = DbSchemaVersion };

                config.MigrationCallback = (migration, version) => { };

                return Realm.GetInstance(config);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}