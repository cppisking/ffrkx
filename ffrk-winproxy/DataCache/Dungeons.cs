using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.DataCache.Dungeons
{
    public class Key
    {
        public uint DungeonId;
    }

    public class Data
    {
        public uint WorldId;
        public string Name;
        public byte Type;
        public ushort Difficulty;
        public uint Synergy;
    }
}
