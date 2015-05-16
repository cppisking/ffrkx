using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpFilterDrops : IDbRequest
    {
        public struct SearchParameters
        {
            public SchemaConstants.ItemType[] ItemTypes;
            public SchemaConstants.Rarity[] Rarity;
            public RealmSynergy.SynergyValue[] Synergies;
            public uint[] Worlds;
            public uint[] Dungeons;
            public uint[] Battles;
            public string Name;
        }

        private FFRKMySqlInstance mDatabase;
        private SearchParameters mParameters;
        private List<BasicItemDropStats> mDropList;

        public delegate void DataReadyCallback(List<BasicItemDropStats> items);
        public event DataReadyCallback OnRequestComplete;

        public DbOpFilterDrops(FFRKMySqlInstance Database, SearchParameters parameters)
        {
            mDatabase = Database;
            mParameters = parameters;
            mDropList = new List<BasicItemDropStats>();
        }

        public bool RequiresTransaction
        {
            get { return false; }
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = String.Format("SELECT * FROM dungeon_drops WHERE item_name LIKE @name");
            using (MySqlCommand command = new MySqlCommand(stmt, connection))
            {
                command.Parameters.AddWithValue("@name", "%" + mParameters.Name + "%");
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
                            TotalDrops = Convert.ToUInt32(reader["drop_count"]),
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
