using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Utility
{
    interface IConverter<T,U>
    {
        T Convert(U u);
    }
}
