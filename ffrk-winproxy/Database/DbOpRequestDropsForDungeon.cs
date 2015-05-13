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
                        BasicItemDropStats stats = new BasicItemDropStats
                        {
                            BattleId = (uint)reader["battleid"],
                            ItemId = (uint)reader["itemid"],
                            EnemyId = (uint)reader["enemyid"],
                            BattleName = (string)reader["battle_name"],
                            BattleStamina = (ushort)reader["battle_stamina"],
                            EnemyName = (string)reader["enemy_name"],
                            TotalDrops = (uint)reader["drop_count"],
                            ItemName = (string)reader["item_name"],
                            TimesRun = (uint)reader["times_run"]
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
