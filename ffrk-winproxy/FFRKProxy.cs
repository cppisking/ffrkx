using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Fiddler;
using ffrk_winproxy.Database;
using ffrk_winproxy.GameData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ffrk_winproxy
{
	public class FFRKProxy : IAutoTamper, IHandleExecAction
	{
        TabPage mTabPage;
        FFRKTabInspector mInspectorView;
        ResponseHistory mHistory;
        static FFRKProxy mInstance;

        public static FFRKProxy Instance { get { return mInstance; } }
        internal ResponseHistory ResponseHistory { get { return mHistory; } }

        public FFRKProxy()
        {
        }

        public void OnLoad()
        {
            mHistory = new ResponseHistory();

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

            string ResponseJson = oSession.GetResponseBodyAsString();
            mHistory.AddResponse(oSession);
            
            if (OnFFRKResponse != null)
                OnFFRKResponse(RequestPath, ResponseJson);
            if (RequestPath.Equals("/dff/world/battles"))
                HandleListBattlesEvent(oSession, ResponseJson);
            else if (RequestPath.StartsWith("/dff/world/dungeons"))
                HandleListDungeonsEvent(oSession, ResponseJson);
            else if (RequestPath.EndsWith("get_battle_init_data"))
                HandleInitiateBattleEvent(oSession, ResponseJson);

            return;
        }

        void HandleInitiateBattleEvent(Session oSession, string ResponseJson)
        {
            try
            {
                EventBattleInitiated result = JsonConvert.DeserializeObject<EventBattleInitiated>(ResponseJson);

                if (OnBattleEngaged != null)
                    OnBattleEngaged(result);
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred processing the initiate battle event.  {0}", ex.Message);
            }
        }

        void HandleListBattlesEvent(Session oSession, string ResponseJson)
        {
            try
            {
                EventListBattles result = JsonConvert.DeserializeObject<EventListBattles>(ResponseJson);
                FFRKMySqlInstance.RecordBattleList(result);
                if (OnListBattles != null)
                    OnListBattles(result);
            }
            catch(Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred processing the list battles event.  {0}", ex.Message);
            }
        }

        void HandleListDungeonsEvent(Session oSession, string ResponseJson)
        {
            try
            {
                EventListDungeons result = JsonConvert.DeserializeObject<EventListDungeons>(ResponseJson);

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
        internal delegate void FFRKResponseDelegate(string Path, string Json);

        internal event BattleInitiatedDelegate OnBattleEngaged;
        internal event ListBattlesDelegate OnListBattles;
        internal event ListDungeonsDelegate OnListDungeons;
        internal event FFRKResponseDelegate OnFFRKResponse;
    }
}
