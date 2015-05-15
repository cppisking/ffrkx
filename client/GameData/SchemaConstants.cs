using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    public static class SchemaConstants
    {
        // These correspond to values the game sends us.  We can just cast an
        // equipment category directly from the game to this type and it will
        // match.
        public enum EquipmentCategory
        {
            Dagger = 1,
            Sword = 2,
            Katana = 3,
            Axe = 4,
            Hammer = 5,
            Spear = 6,
            Fist = 7,
            Rod = 8,
            Staff = 9,
            Bow = 10,
            Instrument = 11,
            Whip = 12,
            Thrown = 13,
            Book = 14,
            Ball = 30,
            Shield = 50,
            Hat = 51,
            Helm = 52,
            LightArmor = 53,
            Armor = 54,
            Robe = 55,
            Bracer = 56,
        }

        // The game doesn't categorize items this way, so this is strictly an enum
        // defined for us.
        public enum ItemType
        {
            Equipment = 1,
            UpgradeMaterial = 2,
            Orb = 3
        }

        // Similar to ItemType, the values here are application-defined, but should
        // remain consistent.
        public enum OrbType
        {
            Power = 0,
            White = 1,
            Black = 2,
            NonElemental = 3,
            Fire = 4,
            Ice = 5,
            Lightning = 6,
            Earth = 7,
            Wind = 8,
            Holy = 9,
            Dark = 10,
            Summon = 11,
            Blue = 12      // This isn't in the game, but it's in the data files.  Hmm...
        }

        public enum WorldType : ushort
        {
            Normal = 1,
            Event = 2
        }

        public enum DungeonType : byte
        {
            Normal = 1,
            Elite = 2
        }
    }
}
