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

        public bool RequiresTransaction { get { return false; } }

        public DbOpLoadAllBattles()
        {
            mBattles = new FFRKDataCacheTable<DataCache.Battles.Key, DataCache.Battles.Data>();
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = "SELECT * FROM battles ORDER BY dungeon ASC, id ASC";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    uint current_dungeon = 0;
                    ushort current_stam_to_reach = 0;
                    DataCache.Battles.Data last_data = null;
                    while (reader.Read())
                    {
                        DataCache.Battles.Key key = new DataCache.Battles.Key();
                        DataCache.Battles.Data data = new DataCache.Battles.Data();

                        key.BattleId = (uint)reader["id"];
                        data.DungeonId = (uint)reader["dungeon"];
                        data.Name = (string)reader["name"];
                        data.Stamina = (ushort)reader["stamina"];
                        data.Samples = (uint)reader["samples"];
                        data.HistoSamples = (uint)reader["histo_samples"];
                        data.Repeatable = true;

                        if (current_dungeon != data.DungeonId)
                        {
                            // We just went to a new dungeon.  Stamina to reach is 0.
                            current_stam_to_reach = 0;

                            // The previous battle is not repeatable.
                            if (last_data != null)
                                last_data.Repeatable = false;
                        }
                        else
                            current_stam_to_reach += data.Stamina;

                        data.StaminaToReach = current_stam_to_reach;
                        mBattles.Update(key, data);

                        current_dungeon = data.DungeonId;
                        last_data = data;
                    }
                    last_data.Repeatable = false;
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
