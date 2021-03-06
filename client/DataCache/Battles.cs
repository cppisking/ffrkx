﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache.Battles
{
    public struct Key
    {
        public uint BattleId;
    }

    public class Data
    {
        public uint DungeonId;
        public string Name;
        public ushort Stamina;
        public uint Samples;
        public uint HistoSamples;

        public bool Repeatable;
        public ushort StaminaToReach;

        public override string ToString()
        {
            return Name;
        }
    }
}
