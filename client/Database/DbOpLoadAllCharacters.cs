using FFRKInspector.DataCache;
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


        public bool RequiresTransaction
        {
            get { return false; }
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            string stmt = "SELECT * FROM characters";
            using (MySqlCommand command = new MySqlCommand(stmt, connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCache.Characters.Key key = new DataCache.Characters.Key();
                        DataCache.Characters.Data data = new DataCache.Characters.Data();
                        key.Id = (uint)reader["id"];
                        data.Name = (string)reader["name"];
                        data.Series = (uint)reader["series"];
                        mCharacters.Update(key, data);
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
