using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    interface IResponseHandler
    {
        bool CanHandle(string RequestPath);
        void Handle(string RequestPath, string ResponseJson);
    }
}
