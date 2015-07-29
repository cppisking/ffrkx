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
        public uint EnemyMaxHp;
        //public string EnemyElemWeakness;
        public List<string> EnemyElemWeakness;
        public List<string> EnemyElemResist;
        public List<string> EnemyElemAbsorb;
        public List<string> EnemyElemNull;
        public List<string> EnemyStatusImmunity;

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
