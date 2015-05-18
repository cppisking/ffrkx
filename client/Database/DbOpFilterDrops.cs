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
        private class SearchParameters
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
        private List<BasicItemDropStats> mDropList;

        private SelectMultiParam<SchemaConstants.ItemType, uint> mItemTypes;
        private SelectMultiParam<SchemaConstants.Rarity, uint> mRarities;
        private SelectMultiParam<RealmSynergy.SynergyValue, uint> mSynergies;
        private SelectMultiParam<uint, uint> mDungeons;
        private SelectMultiParam<uint, uint> mBattles;
        private SelectSingleParam<string> mName;

        public delegate void DataReadyCallback(List<BasicItemDropStats> items);
        public event DataReadyCallback OnRequestComplete;

        public DbOpFilterDrops(FFRKMySqlInstance Database)
        {
            mDatabase = Database;

            mItemTypes = new SelectMultiParam<SchemaConstants.ItemType, uint>("item_type");
            mRarities = new SelectMultiParam<SchemaConstants.Rarity, uint>("item_rarity");
            mSynergies = new SelectMultiParam<RealmSynergy.SynergyValue, uint>("item_series", (x) => x.GameSeries);
            mDungeons = new SelectMultiParam<uint, uint>("dungeon_id");
            mBattles = new SelectMultiParam<uint, uint>("battleid");
            mName = new SelectSingleParam<string>("item_name", SelectSingleParam<string>.ParamOperator.Like);

            mDropList = new List<BasicItemDropStats>();
        }

        public bool RequiresTransaction
        {
            get { return false; }
        }

        public SelectMultiParam<SchemaConstants.ItemType, uint> ItemTypes { get { return mItemTypes; } }
        public SelectMultiParam<SchemaConstants.Rarity, uint> Rarities { get { return mRarities; } }
        public SelectMultiParam<RealmSynergy.SynergyValue, uint> Synergies { get { return mSynergies; } }
        public SelectMultiParam<uint, uint> Dungeons { get { return mDungeons; } }
        public SelectMultiParam<uint, uint> Battles { get { return mBattles; } }
        public SelectSingleParam<string> Name { get { return mName; } }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            SelectBuilder builder = new SelectBuilder();
            builder.Table = "dungeon_drops";
            builder.Columns.Add("*");
            builder.Parameters.Add(mItemTypes);
            builder.Parameters.Add(mRarities);
            builder.Parameters.Add(mSynergies);
            builder.Parameters.Add(mDungeons);
            builder.Parameters.Add(mBattles);
            builder.Parameters.Add(mName);

            string stmt = builder.ToString();

            using (MySqlCommand command = new MySqlCommand(stmt, connection))
            {
                builder.Bind(command);
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
