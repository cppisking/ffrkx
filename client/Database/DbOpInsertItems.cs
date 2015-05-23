using FFRKInspector.DataCache.Items;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpInsertItems : IDbRequest
    {
        public class ItemRecord
        {
            public uint Id;
            public string Name;
            public SchemaConstants.Rarity Rarity;
            public uint? Series;
            public SchemaConstants.ItemType Type;
            public SchemaConstants.EquipmentCategory? EquipCategory;
            public SchemaConstants.OrbType? OrbType;

            public byte Subtype
            {
                get
                {
                    switch (Type)
                    {
                        case SchemaConstants.ItemType.Weapon:
                        case SchemaConstants.ItemType.Armor:
                        case SchemaConstants.ItemType.Accessory:
                            return (byte)EquipCategory.Value;
                        case SchemaConstants.ItemType.Orb:
                            return (byte)OrbType.Value;
                        default:
                            return 0;
                    }
                }
            }
        }

        private List<ItemRecord> mItemRecords;

        public DbOpInsertItems()
        {
            mItemRecords = new List<ItemRecord>();
        }

        public bool RequiresTransaction
        {
            get { return true; }
        }

        public IList<ItemRecord> Items { get { return mItemRecords; } }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            foreach (ItemRecord item in mItemRecords)
            {
                Key key = new Key { ItemId = item.Id };
                Data data = null;

                // If the item is in the cache and the name matches, it's assumed to be
                // a valid entry in the database.
                bool Found = FFRKProxy.Instance.Cache.Items.TryGetValue(key, out data);
                if (Found && data.Name.Equals(item.Name) && data.Subtype==item.Subtype)
                    continue;

                CallProcInsertItemEntry(connection, transaction, item);

                data = new Data { 
                    Type = (byte)item.Type,
                    Name = item.Name,
                    Rarity = (byte)item.Rarity,
                    Series = (uint)item.Series,
                    Subtype = item.Subtype 
                };
                FFRKProxy.Instance.Cache.Items.Update(key, data);
            }
        }

        public void Respond()
        {
        }

        private void CallProcInsertItemEntry(MySqlConnection Connection, MySqlTransaction Transaction, ItemRecord Item)
        {
            using (MySqlCommand command = new MySqlCommand("insert_item_entry", Connection, Transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iid", Item.Id);
                command.Parameters.AddWithValue("@iname", Item.Name);
                command.Parameters.AddWithValue("@irarity", (byte)Item.Rarity);
                command.Parameters.AddWithValue("@iseries", Item.Series);
                command.Parameters.AddWithValue("@itype", (byte)Item.Type);
                command.Parameters.AddWithValue("@isubtype", (byte)Item.Subtype);
                command.ExecuteNonQuery();
            }
        }
    }
}
