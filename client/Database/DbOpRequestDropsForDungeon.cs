using FFRKInspector.GameData;
using Fiddler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpRequestDropsForDungeon : IDbRequest
    {
        private FFRKMySqlInstance mDatabase = null;
        private List<BasicItemDropStats> mDropList;
        private uint mDungeonId;
        public delegate void DataReadyCallback(List<BasicItemDropStats> items);
        public event DataReadyCallback OnRequestComplete;

        public DbOpRequestDropsForDungeon(FFRKMySqlInstance Database, uint DungeonId)
        {
            mDatabase = Database;
            mDungeonId = DungeonId;
            mDropList = new List<BasicItemDropStats>();
        }

        public bool RequiresTransaction { get { return false; } }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            mDropList.Clear();
            string stmt = "SELECT * FROM dungeon_drops WHERE dungeon_id = @id";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                command.Parameters.AddWithValue("@id", mDungeonId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RealmSynergy.SynergyValue synergy = null;
                        int series_ordinal = reader.GetOrdinal("item_series");
                        if (!reader.IsDBNull(series_ordinal))
                        {
                            uint series = (uint)reader["item_series"];
                            synergy = RealmSynergy.FromSeries(series);
                        }

                        BasicItemDropStats stats = new BasicItemDropStats
                        {
                            BattleId = (uint)reader["battleid"],
                            ItemId = (uint)reader["itemid"],
                            DungeonId = (uint)reader["dungeon_id"],
                            DungeonName = (string)reader["dungeon_name"],
                            DungeonType = (SchemaConstants.DungeonType)reader["dungeon_type"],
                            Rarity = (SchemaConstants.Rarity)reader["item_rarity"],
                            Type = (SchemaConstants.ItemType)reader["item_type"],
                            Synergy = synergy,
                            BattleName = (string)reader["battle_name"],
                            BattleStamina = (ushort)reader["battle_stamina"],
                            TotalDrops = (uint)reader["total_drops"],
                            ItemName = (string)reader["item_name"],
                            Samples = (uint)reader["times_run"],
                            StdevSamples = (uint)reader["stdev_samples"],
                            StdevDropCount = Convert.ToUInt64(reader["stdev_sum_of_drops"]),
                            StdevSumOfSquares = Convert.ToUInt64(reader["stdev_sum_of_squares_of_drops"])
                        };
                        mDropList.Add(stats);
                    }
                }
            }
        }

        public void Respond()
        {
            if (OnRequestComplete != null)
                OnRequestComplete(mDropList);
        }
    }
}
