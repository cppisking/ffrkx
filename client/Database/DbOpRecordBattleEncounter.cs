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
    class DbOpRecordBattleEncounter : IDbRequest
    {
        private EventBattleInitiated mEncounter;

        public DbOpRecordBattleEncounter(EventBattleInitiated encounter)
        {
            mEncounter = encounter;
        }

        public bool RequiresTransaction { get { return true; } }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            CallProcRecordBattleEncounter(connection, transaction, mEncounter.Battle);
            List<DropEvent> drops = mEncounter.Battle.Drops.ToList();

            // Insert enemies before inserting drops, since there are foreign key requirements
            foreach (BasicEnemyInfo enemy in mEncounter.Battle.Enemies)
                CallProcInsertEnemyEntry(connection, transaction, enemy.EnemyId, enemy.EnemyName);

            var non_gold_drop_events = drops.Where(x => x.ItemType != DataEnemyDropItem.DropItemType.Gold);
            // Record per-enemy drops
            foreach (DropEvent drop in non_gold_drop_events)
                CallProcRecordDropsForBattleAndEnemy(connection, transaction, mEncounter.Battle.BattleId, drop);

            var drop_events_grouped_by_item = non_gold_drop_events.GroupBy(x => x.ItemId);
            foreach (var drop in drop_events_grouped_by_item)
            {
                uint total_drops_of_this_item = (uint)drop.Count();
                CallProcRecordDropsForBattle(connection, transaction, mEncounter.Battle.BattleId, drop.Key, total_drops_of_this_item);
            }

            Utility.Log.LogFormat("Committing drop information for battle #{0}.  {1} items.",
                mEncounter.Battle.BattleId, drops.Count);
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

        int CallProcRecordDropsForBattleAndEnemy(MySqlConnection connection, MySqlTransaction transaction, uint battle_id, DropEvent drop)
        {
            // Don't update the items cache here.  A DropEvent doesn't contain enough information to insert
            // an item anyway (in particular because it doesn't tell you the name of an item)
            using (MySqlCommand command = new MySqlCommand("record_drops_for_battle_and_enemy", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle_id);
                command.Parameters.AddWithValue("@item_id", drop.ItemId);
                command.Parameters.AddWithValue("@enemy_id", drop.EnemyId);
                command.Parameters.AddWithValue("@item_count", 1);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcRecordDropsForBattle(MySqlConnection connection, MySqlTransaction transaction, uint battle_id, uint item_id, uint count)
        {
            // Don't update the items cache here.  A DropEvent doesn't contain enough information to insert
            // an item anyway (in particular because it doesn't tell you the name of an item)
            using (MySqlCommand command = new MySqlCommand("record_drops_for_battle", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle_id);
                command.Parameters.AddWithValue("@item_id", item_id);
                command.Parameters.AddWithValue("@item_count", count);
                return command.ExecuteNonQuery();
            }
        }

    }
}
