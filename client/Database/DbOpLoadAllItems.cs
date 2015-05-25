using FFRKInspector.DataCache;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpLoadAllItems : IDbRequest
    {
        private FFRKDataCacheTable<DataCache.Items.Key, DataCache.Items.Data> mItems;

        public delegate void DataReadyCallback(FFRKDataCacheTable<DataCache.Items.Key, DataCache.Items.Data> items);
        public event DataReadyCallback OnRequestComplete;

        public bool RequiresTransaction { get { return false; } }

        public DbOpLoadAllItems()
        {
            mItems = new FFRKDataCacheTable<DataCache.Items.Key, DataCache.Items.Data>();
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = "SELECT i.id, i.name, i.rarity, i.series, i.type, i.subtype, " +
                          "       s.base_atk, s.base_mag, s.base_acc, s.base_def, s.base_res, s.base_eva, s.base_mnd, " +
                          "       s.max_atk, s.max_mag, s.max_acc, s.max_def, s.max_res, s.max_eva, s.max_mnd " +
                          "FROM   items i LEFT OUTER JOIN equipment_stats s ON s.equipment_id = i.id";

            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Items.Key key = new DataCache.Items.Key();
                        DataCache.Items.Data data = new DataCache.Items.Data();
                        key.ItemId = (uint)reader["id"];
                        data.Name = (string)reader["name"];
                        data.Rarity = (byte)reader["rarity"];
                        data.Series = reader.GetValueOrNull<uint>("series");
                        data.Type = (byte)reader["type"];
                        data.Subtype = (byte)reader["subtype"];

                        data.BaseStats = new GameData.EquipStats();
                        data.BaseStats.Atk = reader.GetValueOrNull<short>("base_atk");
                        data.BaseStats.Mag = reader.GetValueOrNull<short>("base_mag");
                        data.BaseStats.Acc = reader.GetValueOrNull<short>("base_acc");
                        data.BaseStats.Def = reader.GetValueOrNull<short>("base_def");
                        data.BaseStats.Res = reader.GetValueOrNull<short>("base_res");
                        data.BaseStats.Eva = reader.GetValueOrNull<short>("base_eva");
                        data.BaseStats.Mnd = reader.GetValueOrNull<short>("base_mnd");

                        data.MaxStats = new GameData.EquipStats();
                        data.MaxStats.Atk = reader.GetValueOrNull<short>("max_atk");
                        data.MaxStats.Mag = reader.GetValueOrNull<short>("max_mag");
                        data.MaxStats.Acc = reader.GetValueOrNull<short>("max_acc");
                        data.MaxStats.Def = reader.GetValueOrNull<short>("max_def");
                        data.MaxStats.Res = reader.GetValueOrNull<short>("max_res");
                        data.MaxStats.Eva = reader.GetValueOrNull<short>("max_eva");
                        data.MaxStats.Mnd = reader.GetValueOrNull<short>("max_mnd");
                        mItems.Update(key, data);
                    }
                }
            }
        }

        public void Respond()
        {
            if (OnRequestComplete != null)
                OnRequestComplete(mItems);
        }
    }
}
