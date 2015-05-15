using FFRKInspector.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache.Worlds
{
    public struct Key
    {
        public uint WorldId;
    }

    public class Data
    {
        public uint Series;
        public SchemaConstants.WorldType Type;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
