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
    class DbOpRecordBattleEncounter : ITransactedDbRequest
    {
        private EventBattleInitiated mEncounter;

        public DbOpRecordBattleEncounter(EventBattleInitiated encounter)
        {
            mEncounter = encounter;
        }

        public bool RunInTransaction { get { return true; } }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            int failedDropCount = 0;
            StringBuilder failedDropList = new StringBuilder();
            CallProcRecordBattleEncounter(connection, transaction, mEncounter.Battle);
            List<DropEvent> drops = mEncounter.Battle.Drops.ToList();

            // Insert enemies before inserting drops, since there are foreign key
            // requirements
            foreach (BasicEnemyInfo enemy in mEncounter.Battle.Enemies)
                CallProcInsertEnemyEntry(connection, transaction, enemy.EnemyId, enemy.EnemyName);

            foreach (DropEvent drop in drops)
            {
                if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                    continue;

                try
                {
                    CallProcRecordDropEvent(connection, transaction, mEncounter.Battle, drop);
                } catch (MySqlException)
                {
                    if (failedDropCount++ > 0)
                        failedDropList.Append(", ");
                    failedDropList.AppendFormat("[item: {0}, round: {1}, enemy: {2}]", drop.ItemId, drop.Round, drop.EnemyName);
                }
            }

            if (failedDropCount == 0)
            {
                FiddlerApplication.Log.LogFormat("Committed drop information for battle #{0}.  0/{1} items failed.",
                    mEncounter.Battle.BattleId, drops.Count);
            } else
            {
                FiddlerApplication.Log.LogFormat("WARNING: Committed drop information for battle #{0}.  {1}/{2} items failed.",
                    mEncounter.Battle.BattleId, failedDropCount, drops.Count, failedDropList.ToString());
            }
        }

        public void Respond()
        {
        }

        private int CallProcRecordBattleEncounter(MySqlConnection connection, MySqlTransaction transaction, DataActiveBattle battle)
        {
            using (MySqlCommand command = new MySqlCommand("record_battle_encounter", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle.BattleId);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcInsertEnemyEntry(MySqlConnection connection, MySqlTransaction transaction, uint EnemyId, string EnemyName)
        {
            using (MySqlCommand command = new MySqlCommand("insert_enemy_entry", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@enemy_id", EnemyId);
                command.Parameters.AddWithValue("@enemy_name", EnemyName);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcRecordDropEvent(MySqlConnection connection, MySqlTransaction transaction, DataActiveBattle battle, DropEvent drop)
        {
            // Don't update the items cache here.  A DropEvent doesn't contain enough information to insert
            // an item anyway (in particular because it doesn't tell you the name of an item)

            using (MySqlCommand command = new MySqlCommand("record_drop_event", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle.BattleId);
                command.Parameters.AddWithValue("@item_id", drop.ItemId);
                command.Parameters.AddWithValue("@enemy_id", drop.EnemyId);
                command.Parameters.AddWithValue("@item_count", 1);
                return command.ExecuteNonQuery();
            }
        }

    }
}
