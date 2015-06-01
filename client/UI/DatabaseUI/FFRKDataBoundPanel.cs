using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI.DatabaseUI
{
    interface FFRKDataBoundPanel
    {
        void InitializeConnection();
        void Reload();
        void Commit();
    }
}
