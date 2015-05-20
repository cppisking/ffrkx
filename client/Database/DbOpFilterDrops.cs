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

            // Since histogram bars will come in on different rows we need a way to look up the item
            // so we can modify its histogram on the fly.
            var keyed_lookup = new Dictionary<KeyValuePair<uint, uint>, BasicItemDropStats>();

            string stmt = builder.ToString();

            using (MySqlCommand command = new MySqlCommand(stmt, connection))
            {
                builder.Bind(command);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        uint battle_id = (uint)reader["battleid"];
                        uint item_id = (uint)reader["itemid"];
                        var key = new KeyValuePair<uint, uint>(battle_id, item_id);

                        BasicItemDropStats stats = null;
                        if (!keyed_lookup.TryGetValue(key, out stats))
                        {
                            // This is a new entry.
                            RealmSynergy.SynergyValue synergy = null;
                            int series_ordinal = reader.GetOrdinal("item_series");
                            if (!reader.IsDBNull(series_ordinal))
                            {
                                uint series = (uint)reader["item_series"];
                                synergy = RealmSynergy.FromSeries(series);
                            }

                            stats = new BasicItemDropStats
                            {
                                BattleId = battle_id,
                                ItemId = item_id,
                                DungeonId = (uint)reader["dungeon_id"],
                                DungeonName = (string)reader["dungeon_name"],
                                DungeonType = (SchemaConstants.DungeonType)reader["dungeon_type"],
                                Rarity = (SchemaConstants.Rarity)reader["item_rarity"],
                                Type = (SchemaConstants.ItemType)reader["item_type"],
                                TimesRun = (uint)reader["times_run"],
                                TimesRunWithHistogram = (uint)reader["times_run_with_histogram"],
                                Synergy = synergy,
                                BattleName = (string)reader["battle_name"],
                                BattleStamina = (ushort)reader["battle_stamina"],
                                ItemName = (string)reader["item_name"],
                            };

                            keyed_lookup.Add(key, stats);
                        }

                        // Modify its histogram entry.
                        int bucket = (int)reader["histo_bucket"];
                        uint bucket_value = (uint)reader["histo_value"];

                        if (bucket < 0)
                        {
                            // The total drops is stored in bucket -1.  This should always be present.
                            stats.TotalDrops = bucket_value;
                        }
                        else if (bucket > 0)
                        {
                            // We should never have a bucket 0, because that would mean 0 of the item dropped,
                            // in which case why would it even be in the drop list?
                            System.Diagnostics.Debug.Assert(bucket != 0);

                            stats.Histogram[bucket] = bucket_value;
                        }
                    }
                }
            }

            mDropList = keyed_lookup.Values.ToList();
            foreach (BasicItemDropStats stats in mDropList)
            {
                // Post process the list.  None of the items will have a value set for Histogram[0] because that
                // means we didn't see anything.  So we have to compute this by subtracting all the events where
                // we did see something from all the events total.

                stats.Histogram[0] = stats.TimesRunWithHistogram;
                for (int i = 1; i < stats.Histogram.BucketCount; ++i)
                {
                    stats.Histogram[0] -= stats.Histogram[i];
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
