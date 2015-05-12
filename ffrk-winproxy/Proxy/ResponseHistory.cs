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
        }

        List<HistoryItem> mResponseHistory;

        public ResponseHistory()
        {
            mResponseHistory = new List<HistoryItem>();
        }

        public void AddResponse(Session session)
        {
            HistoryItem data = new HistoryItem();
            data.Timestamp = DateTime.Now;
            data.Session = session;

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
