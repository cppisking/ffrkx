using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Utility
{
    public static class CriticalZ
    {
        public static Dictionary<ushort, double> Lookup = new Dictionary<ushort,double> {
            {90, 1.645},
            {91, 1.695},
            {92, 1.75},
            {93, 1.81},
            {94, 1.88},
            {95, 1.96},
            {96, 2.054},
            {97, 2.17},
            {98, 2.326},
            {99, 2.576},
        };
    }
}
