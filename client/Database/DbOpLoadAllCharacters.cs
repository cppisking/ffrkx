using FFRKInspector.DataCache;
using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpLoadAllCharacters : IDbRequest
    {
        private FFRKDataCacheTable<DataCache.Characters.Key, DataCache.Characters.Data> mCharacters;

        public delegate void DataReadyCallback(FFRKDataCacheTable<DataCache.Characters.Key, DataCache.Characters.Data> characters);
        public event DataReadyCallback OnRequestComplete;

        public DbOpLoadAllCharacters()
        {
            mCharacters = new FFRKDataCacheTable<DataCache.Characters.Key, DataCache.Characters.Data>();
        }

        public bool RequiresTransaction
        {
            get { return false; }
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = "SELECT c.id, c.name, c.series, e.equip_category " +
                          "FROM characters c LEFT JOIN equip_usage e" +
                          "  ON c.id = e.character_id";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Characters.Key key = new DataCache.Characters.Key();
                        key.Id = (uint)reader["id"];
                        DataCache.Characters.Data data = null;
                        if (!mCharacters.TryGetValue(key, out data))
                        {
                            // Since we're doing a join, the same key may appear many times with
                            // different values for equip_category.  Only insert the item into the
                            // cache once, but add additional equip categories on subsequent occurrences
                            // of the same item.
                            data = new DataCache.Characters.Data();
                            data.UsableEquips = new List<SchemaConstants.EquipmentCategory>();
                            data.Name = (string)reader["name"];
                            data.Series = (uint)reader["series"];

                            mCharacters.Update(key, data);
                        }
                        SchemaConstants.EquipmentCategory? category = reader.GetValueOrNull<SchemaConstants.EquipmentCategory>("equip_category");
                        if (category.HasValue)
                            data.UsableEquips.Add(category.Value);
                    }
                }
            }
        }

        public void Respond()
        {
            if (OnRequestComplete != null)
                OnRequestComplete(mCharacters);
        }
    }
}
