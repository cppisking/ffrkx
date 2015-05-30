using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    public class EquipStats
    {
        public short? Atk;
        public short? Mag;
        public short? Acc;
        public short? Def;
        public short? Res;
        public short? Eva;
        public short? Mnd;

        public bool IsValid
        {
            get
            {
                if (Atk != null) return true;
                if (Mag != null) return true;
                if (Acc != null) return true;
                if (Def != null) return true;
                if (Res != null) return true;
                if (Eva != null) return true;
                if (Mnd != null) return true;
                return false;
            }
        }
    }
}
