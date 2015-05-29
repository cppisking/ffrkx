using FFRKInspector.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache.Characters
{
    struct Key
    {
        public uint Id;
    }

    class Data
    {
        public string Name;
        public uint Series;
        public List<SchemaConstants.EquipmentCategory> UsableEquips;
    }
}
