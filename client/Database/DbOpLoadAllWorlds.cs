using FFRKInspector.DataCache;
using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpLoadAllWorlds : IDbRequest
    {
        private FFRKDataCacheTable<DataCache.Worlds.Key, DataCache.Worlds.Data> mWorlds;

        public delegate void DataReadyCallback(FFRKDataCacheTable<DataCache.Worlds.Key, DataCache.Worlds.Data> battles);
        public event DataReadyCallback OnRequestComplete;

        public bool RequiresTransaction { get { return false; } }

        public DbOpLoadAllWorlds()
        {
            mWorlds = new FFRKDataCacheTable<DataCache.Worlds.Key, DataCache.Worlds.Data>();
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = "SELECT * FROM worlds";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Worlds.Key key = new DataCache.Worlds.Key();
                        DataCache.Worlds.Data data = new DataCache.Worlds.Data();
                        key.WorldId = (uint)reader["id"];
                        data.Name = (string)reader["name"];
                        data.Series = (uint)reader["series"];
                        data.Type = (SchemaConstants.WorldType)reader["type"];
                        mWorlds.Update(key, data);
                    }
                }
            }
        }

        public void Respond()
        {
            if (OnRequestComplete != null)
                OnRequestComplete(mWorlds);
        }
    }
}
