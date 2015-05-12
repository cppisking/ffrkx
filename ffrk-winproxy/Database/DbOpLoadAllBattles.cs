using FFRKInspector.DataCache;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpLoadAllBattles : IDbRequest
    {
        private FFRKDataCacheTable<DataCache.Battles.Key, DataCache.Battles.Data> mBattles;

        public delegate void DataReadyCallback(FFRKDataCacheTable<DataCache.Battles.Key, DataCache.Battles.Data> battles);
        public event DataReadyCallback OnRequestComplete;

        public DbOpLoadAllBattles()
        {
            mBattles = new FFRKDataCacheTable<DataCache.Battles.Key, DataCache.Battles.Data>();
        }

        public void Execute(MySqlConnection connection)
        {
            string stmt = "SELECT * FROM battles";
            using (MySqlCommand command = new MySqlCommand(stmt, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Battles.Key key = new DataCache.Battles.Key();
                        DataCache.Battles.Data data = new DataCache.Battles.Data();
                        key.BattleId = (uint)reader["id"];
                        data.DungeonId = (uint)reader["dungeon"];
                        data.Name = (string)reader["name"];
                        data.Stamina = (byte)reader["stamina"];
                        data.TimesRun = (uint)reader["times_run"];
                        mBattles.Update(key, data);
                    }
                }
            }
        }

        public void Respond()
        {
            if (OnRequestComplete != null)
                OnRequestComplete(mBattles);
        }
    }
}
