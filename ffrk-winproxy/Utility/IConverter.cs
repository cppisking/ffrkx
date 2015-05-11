using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.Utility
{
    interface IConverter<T,U>
    {
        T Convert(U u);
    }
}
