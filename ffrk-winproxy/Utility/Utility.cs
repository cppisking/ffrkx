using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Utility
{
    static class Utility
    {
        private static Dictionary<string, uint> romans = new Dictionary<string, uint>
        {
            {"I", 1}, {"II", 2}, {"III", 3}, {"IV", 4}, {"V", 5}, {"VI", 6}, {"VII", 7},
            {"VIII", 8}, {"IX", 9}, {"X", 10}, {"XI", 11}, {"XII", 12}, {"XIII", 13}, {"XIV", 14}
        };

        public static uint RomanNumeralToNumber(string Roman)
        {
            uint result = 0;
            romans.TryGetValue(Roman, out result);
            return result;
        }
    }
}
