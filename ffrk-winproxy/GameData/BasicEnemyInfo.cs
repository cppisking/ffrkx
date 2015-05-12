using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class BasicEnemyInfo
    {
        public uint EnemyId;
        public string EnemyName;

        public override bool Equals(object obj)
        {
            BasicEnemyInfo other = obj as BasicEnemyInfo;
            if (other == null)
                return false;
            return other.EnemyId == EnemyId;
        }

        public override int GetHashCode()
        {
            return EnemyId.GetHashCode();
        }
    }
}
