using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    interface FFRKDataBoundPanel
    {
        void Reload();
        void Commit();
    }
}
