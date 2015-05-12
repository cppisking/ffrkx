using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpRecordDungeonList : ITransactedDbRequest
    {
        EventListDungeons mDungeonList;

        public DbOpRecordDungeonList(EventListDungeons dungeons)
        {
            mDungeonList = dungeons;
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            CallProcInsertWorldEntry(connection, transaction, mDungeonList.World);

            foreach (DataDungeon dungeon in mDungeonList.Dungeons)
                CallProcInsertDungeonEntry(connection, transaction, mDungeonList.World, dungeon);
        }

        public void Respond()
        {
        }

        int CallProcInsertWorldEntry(MySqlConnection connection, MySqlTransaction transaction, DataWorld world)
        {
            using (MySqlCommand command = new MySqlCommand("insert_world_entry", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@wid", world.Id);
                command.Parameters.AddWithValue("@series", world.SeriesId);
                command.Parameters.AddWithValue("@type", world.Type);
                command.Parameters.AddWithValue("@name", world.Name);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcInsertDungeonEntry(MySqlConnection connection, MySqlTransaction transaction, DataWorld world, DataDungeon dungeon)
        {
            using (MySqlCommand command = new MySqlCommand("insert_dungeon_entry", connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@did", dungeon.Id);
                command.Parameters.AddWithValue("@world_id", world.Id);
                command.Parameters.AddWithValue("@dname", dungeon.Name);
                command.Parameters.AddWithValue("@dtype", dungeon.Type);
                command.Parameters.AddWithValue("@ddiff", dungeon.Difficulty);
                command.Parameters.AddWithValue("@dsyn", 0);
                return command.ExecuteNonQuery();
            }
        }

    }
}
