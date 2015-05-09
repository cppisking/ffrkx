using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Fiddler;
using ffrk_winproxy.GameData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ffrk_winproxy
{
	public class FFRKProxy : IAutoTamper, IHandleExecAction
	{
        TabPage mTabPage;
        FFRKTabInspector mInspectorView;
        static FFRKProxy mInstance;

        public static FFRKProxy Instance { get { return mInstance; } }

        public FFRKProxy()
        {
        }

        public void OnLoad()
        {
            mTabPage = new TabPage("FFRK Inspector");
            mInspectorView = new FFRKTabInspector(this);
            mInspectorView.Dock = DockStyle.Fill;
            mTabPage.Controls.Add(mInspectorView);
            FiddlerApplication.UI.tabsViews.TabPages.Add(mTabPage);
            mInstance = this;
        }

        public void OnBeforeUnload()
        {
            mInstance = null;
        }

        public void AutoTamperRequestBefore(Session oSession) { }
        public void AutoTamperRequestAfter(Session oSession) { }
        public void AutoTamperResponseBefore(Session oSession) { }

        public void AutoTamperResponseAfter(Session oSession)
        {
            if (!oSession.oRequest.host.Equals("ffrk.denagames.com", StringComparison.CurrentCultureIgnoreCase))
                return;

            string RequestPath = oSession.oRequest.headers.RequestPath;
            FiddlerApplication.Log.LogFormat(RequestPath);
            if (!oSession.oResponse.MIMEType.Contains("json"))
                return;

            if (RequestPath.Equals("/dff/world/battles"))
                HandleListBattlesEvent(oSession);
            else if (RequestPath.StartsWith("/dff/world/dungeons"))
                HandleListDungeonsEvent(oSession);
            else if (RequestPath.EndsWith("get_battle_init_data"))
                HandleInitiateBattleEvent(oSession);

            return;
        }

        string DecodeResponse(Session oSession)
        {
            byte[] responseBytes = (byte[])oSession.ResponseBody.Clone();

            if (oSession.oResponse.headers == null)
                return null;

            HTTPResponseHeaders headers = oSession.oResponse.headers;
            if (!headers.Exists("Transfer-Encoding") && !headers.Exists("Content-Encoding"))
                return null;
            string transfer_encoding = headers["Transfer-Encoding"];
            string content_encoding = headers["Content-Encoding"];
            if (Utilities.isUnsupportedEncoding(transfer_encoding, content_encoding))
                return null;
            try
            {
                Utilities.utilDecodeHTTPBody(headers, ref responseBytes, false);
                string encoding = headers.Exists("charset") ? headers["charset"] : "utf-8";
                return Encoding.GetEncoding(encoding).GetString(responseBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        void HandleInitiateBattleEvent(Session oSession)
        {
            try
            {
                string ResponseText = oSession.GetResponseBodyAsString();
                EventBattleInitiated result = JsonConvert.DeserializeObject<EventBattleInitiated>(ResponseText);

                if (OnBattleEngaged != null)
                    OnBattleEngaged(result);
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred processing the initiate battle event.  {0}", ex.Message);
            }
        }

        void HandleListBattlesEvent(Session oSession)
        {
            try
            {
                string ResponseText = oSession.GetResponseBodyAsString();
                EventListBattles result = JsonConvert.DeserializeObject<EventListBattles>(ResponseText);
                FFRKMySqlInstance.RecordBattleList(result);
                if (OnListBattles != null)
                    OnListBattles(result);
            }
            catch(Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred processing the list battles event.  {0}", ex.Message);
            }
        }

        void HandleListDungeonsEvent(Session oSession)
        {
            try
            {
                string ResponseText = oSession.GetResponseBodyAsString();
                EventListDungeons result = JsonConvert.DeserializeObject<EventListDungeons>(ResponseText);

                FFRKMySqlInstance.RecordDungeonList(result);
                if (OnListDungeons != null)
                    OnListDungeons(result);
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred processing the list battles event.  {0}", ex.Message);
            }
        }

        public void OnBeforeReturningError(Session oSession) { }

        public bool OnExecAction(string sCommand)
        {
            throw new NotImplementedException();
        }

        internal delegate void BattleInitiatedDelegate(EventBattleInitiated battle);
        internal delegate void ListBattlesDelegate(EventListBattles battles);
        internal delegate void ListDungeonsDelegate(EventListDungeons dungeons);

        internal event BattleInitiatedDelegate OnBattleEngaged;
        internal event ListBattlesDelegate OnListBattles;
        internal event ListDungeonsDelegate OnListDungeons;
    }
}
