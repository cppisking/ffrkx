using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleLeaveDungeon : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.EndsWith("/leave_dungeon");
        }

        public override void Handle(Session Session)
        {
            FFRKProxy.Instance.RaiseLeaveDungeon();
        }
    }
}
