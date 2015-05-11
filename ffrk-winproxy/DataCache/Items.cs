using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.DataCache.Items
{
    public class Key
    {
        public uint ItemId;
    }

    public class Data
    {
        public string Name;
        public byte Rarity;
        public byte Type;
        public byte Subtype;
        public byte? Realm;
    }
}
