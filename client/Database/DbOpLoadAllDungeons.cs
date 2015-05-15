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
    class DbOpLoadAllDungeons : IDbRequest
    {
        private FFRKDataCacheTable<DataCache.Dungeons.Key, DataCache.Dungeons.Data> mDungeons;

        public delegate void DataReadyCallback(FFRKDataCacheTable<DataCache.Dungeons.Key, DataCache.Dungeons.Data> dungeons);
        public event DataReadyCallback OnRequestComplete;

        public bool RequiresTransaction { get { return false; } }

        public DbOpLoadAllDungeons()
        {
            mDungeons = new FFRKDataCacheTable<DataCache.Dungeons.Key, DataCache.Dungeons.Data>();
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = "SELECT * FROM dungeons";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Dungeons.Key key = new DataCache.Dungeons.Key();
                        DataCache.Dungeons.Data data = new DataCache.Dungeons.Data();
                        key.DungeonId = (uint)reader["id"];
                        data.WorldId = (uint)reader["world"];
                        data.Series = (uint)reader["series"];
                        data.Name = (string)reader["name"];
                        data.Type = (SchemaConstants.DungeonType)reader["type"];
                        data.Difficulty = (ushort)reader["difficulty"];
                        mDungeons.Update(key, data);
                    }
                }
            }
        }

        public void Respond()
        {
            if (OnRequestComplete != null)
                OnRequestComplete(mDungeons);
        }
    }
}
