using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpRecordBattleList : IDbRequest
    {
        EventListBattles mBattles;

        public DbOpRecordBattleList(EventListBattles battles)
        {
            mBattles = battles;
        }

        public bool RequiresTransaction { get { return true; } }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            foreach (DataBattle battle in mBattles.Battles)
                CallProcInsertBattleEntry(connection, transaction, battle);
        }

        public void Respond()
        {
        }

        int CallProcInsertBattleEntry(MySqlConnection connection, MySqlTransaction transaction, DataBattle battle)
        {
            using (MySqlCommand command = new MySqlCommand("insert_battle_entry", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@bid", battle.Id);
                command.Parameters.AddWithValue("@did", battle.DungeonId);
                command.Parameters.AddWithValue("@bname", battle.Name);
                command.Parameters.AddWithValue("@bstam", battle.Stamina);
                return command.ExecuteNonQuery();
            }
        }

    }
}
