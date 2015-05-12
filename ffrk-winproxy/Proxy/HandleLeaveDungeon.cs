using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleLeaveDungeon : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.EndsWith("/leave_dungeon");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            FFRKProxy.Instance.RaiseLeaveDungeon();
        }
    }
}
