using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    public static class SchemaConstants
    {
        public enum EvolutionLevel : byte
        {
            Base = 0,
            Plus = 1,
            PlusPlus = 2
        }

        public enum EventType : byte
        {
            DailyDungeon = 1,
            Challenge = 2,
            Survival = 3,
            Coliseum = 4
        }

        // These correspond to values the game sends us.  We can just cast an
        // equipment category directly from the game to this type and it will
        // match.
        public enum ItemType : byte
        {
            Weapon = 1,
            Armor = 2,
            Accessory = 3,
            UpgradeMaterial = 4,
            Orb = 5,
            GrowthEgg = 6
        }

        public enum EquipmentCategory : byte
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
            Gun = 15,
            Ball = 30,
            Hairpin = 31,
            Shield = 50,
            Hat = 51,
            Helm = 52,
            LightArmor = 53,
            Armor = 54,
            Robe = 55,
            Bracer = 56,
            Accessory = 80,
            WeaponUpgrade = 98,
            ArmorUpgrade = 99
        }

        public enum PartyFormation : byte
        {
            Front = 1,
            Back = 2
        }

        public enum Rarity : byte
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7
        }

        // Similar to ItemType, the values here are application-defined, but should
        // remain consistent.
        public enum OrbType : byte
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

        public enum ElementID : ushort
        {
            Fire = 100,
            Ice = 101,
            Lightning = 102,
            Earth = 103,
            Wind = 104,
            Water = 105,
            Holy = 106,
            Dark = 107,
            Poison = 108
        }

        public enum ElementVulnerability : ushort
        {
            Vulnerable = 1,
            Resist = 6,
            Null = 11,
            Absorb = 21
        }

        public enum StatusID : ushort
        {
            Poison = 200,
            Silence = 201,
            Paralyze = 202,
            Confuse = 203,
            Haste = 204,
            Slow = 205,
            Stop = 206,
            Protect = 207,
            Shell = 208,
            Reflect = 209,
            Blind = 210,
            Sleep = 211,
            Petrify = 212,
            Doom = 213,
            Instant_KO = 214,
            Beserk = 215,
            Regen = 216,
            Reraise = 217,
            Float = 218,
            Weak = 219,
            Zombie = 220,
            Mini = 221,
            Toad = 222,
            Curse = 223,
            Slownumb = 224,
            Blink = 225
        }

        public enum StatusVulnerability : ushort
        {
            Immune = 1
        }

    }
}
