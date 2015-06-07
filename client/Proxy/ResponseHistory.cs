using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    internal class ResponseHistory
    {
        public class HistoryItem
        {
            public DateTime Timestamp;
            public Session Session;
            public JObject JsonObject;
            public IResponseHandler Handler;
        }

        List<HistoryItem> mResponseHistory;

        public ResponseHistory()
        {
            mResponseHistory = new List<HistoryItem>();
        }

        public void AddItem(Session Session, IResponseHandler Handler)
        {
            HistoryItem data = new HistoryItem();
            data.Timestamp = DateTime.Now;
            data.Session = Session;
            data.Handler = Handler;

            mResponseHistory.Add(data);
        }

        public void Clear()
        {
            mResponseHistory.Clear();
        }

        public HistoryItem this[int Index]
        {
            get { return mResponseHistory[Index]; }
        }

        public int Size { get { return mResponseHistory.Count; } }
    }
}
