using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy
{
    internal class ResponseHistory
    {
        public class HistoryItem
        {
            public DateTime Timestamp;
            public string RequestPath;
            public string JsonText;
            public JObject JsonObject;
        }

        List<HistoryItem> mResponseHistory;

        public ResponseHistory()
        {
            mResponseHistory = new List<HistoryItem>();
        }

        public void AddResponse(string RequestPath, string ResponseJson)
        {
            HistoryItem data = new HistoryItem();
            data.Timestamp = DateTime.Now;
            data.RequestPath = RequestPath;

            data.JsonText = ResponseJson;
            mResponseHistory.Add(data);
        }

        public HistoryItem this[int Index]
        {
            get { return mResponseHistory[Index]; }
        }

        public int Size { get { return mResponseHistory.Count; } }
    }
}
