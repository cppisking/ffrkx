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
        public ushort Type;
        public string Name;
    }
}
