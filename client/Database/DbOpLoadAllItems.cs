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
            string stmt = "SELECT * FROM items";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Items.Key key = new DataCache.Items.Key();
                        DataCache.Items.Data data = new DataCache.Items.Data();
                        int series_ordinal = reader.GetOrdinal("series");
                        key.ItemId = (uint)reader["id"];
                        data.Name = (string)reader["name"];
                        data.Rarity = (byte)reader["rarity"];
                        if (reader.IsDBNull(series_ordinal))
                            data.Series = null;
                        else
                            data.Series = (uint)reader[series_ordinal];
                        data.Type = (byte)reader["type"];
                        data.Subtype = (byte)reader["subtype"];
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
