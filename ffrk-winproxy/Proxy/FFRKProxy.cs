using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Fiddler;
using FFRKInspector.Database;
using FFRKInspector.GameData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FFRKInspector.UI;
using FFRKInspector.DataCache;

namespace FFRKInspector.Proxy
{
	public class FFRKProxy : IAutoTamper, IHandleExecAction
	{
        TabPage mTabPage;
        FFRKTabInspector mInspectorView;
        ResponseHistory mHistory;
        FFRKMySqlInstance mDatabaseInstance;
        GameState mGameState;
        List<IResponseHandler> mResponseHandlers;
        FFRKDataCache mCache;

        static FFRKProxy mInstance;

        public static FFRKProxy Instance { get { return mInstance; } }
        internal ResponseHistory ResponseHistory { get { return mHistory; } }
        internal GameState GameState { get { return mGameState; } }
        internal FFRKMySqlInstance Database { get { return mDatabaseInstance; } }
        internal FFRKDataCache Cache { get { return mCache; } }

        public FFRKProxy()
        {
        }

        public void OnLoad()
        {
            mResponseHandlers = new List<IResponseHandler>();
            mResponseHandlers.Add(new HandleListBattles());
            mResponseHandlers.Add(new HandleListDungeons());
            mResponseHandlers.Add(new HandleLeaveDungeon());
            mResponseHandlers.Add(new HandleInitiateBattle());
            mResponseHandlers.Add(new HandleGacha());
            mResponseHandlers.Add(new HandleLoseBattle());
            mResponseHandlers.Add(new HandleWinBattle());

            mHistory = new ResponseHistory();
            mGameState = new GameState();
            mDatabaseInstance = new FFRKMySqlInstance();

            InitializeDataCache();

            mTabPage = new TabPage("FFRK Inspector");
            mInspectorView = new FFRKTabInspector();
            mInspectorView.Dock = DockStyle.Fill;
            mTabPage.Controls.Add(mInspectorView);
            FiddlerApplication.UI.tabsViews.TabPages.Add(mTabPage);
            mInstance = this;
        }

        private void InitializeDataCache()
        {
            mCache = new FFRKDataCache();
            DbOpLoadAllItems items_request = new DbOpLoadAllItems();
            items_request.OnRequestComplete += DbOpLoadAllItems_OnRequestComplete;
            mDatabaseInstance.BeginExecuteRequest(items_request);

            DbOpLoadAllBattles battles_request = new DbOpLoadAllBattles();
            battles_request.OnRequestComplete += DbOpLoadAllBattles_OnRequestComplete;
        }

        void DbOpLoadAllBattles_OnRequestComplete(FFRKDataCacheTable<DataCache.Battles.Key, DataCache.Battles.Data> battles)
        {
            lock (mCache.SyncRoot)
                mCache.Battles = battles;
            if (OnItemCacheRefreshed != null)
                OnItemCacheRefreshed();
        }

        void DbOpLoadAllItems_OnRequestComplete(FFRKDataCacheTable<DataCache.Items.Key, DataCache.Items.Data> items)
        {
            lock(mCache.SyncRoot)
                mCache.Items = items;
            if (OnItemCacheRefreshed != null)
                OnItemCacheRefreshed();
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

            mHistory.AddResponse(oSession);
            if (OnFFRKResponse != null)
                OnFFRKResponse(RequestPath);

            foreach (IResponseHandler handler in mResponseHandlers)
            {
                if (!handler.CanHandle(RequestPath))
                    continue;

                try
                {
                    string ResponseJson = oSession.GetResponseBodyAsString();
                    handler.Handle(RequestPath, ResponseJson);
                }
                catch (Exception ex)
                {
                    FiddlerApplication.Log.LogFormat("An error occurred processing the response from {0}.  {1}", RequestPath, ex.Message);
                }
            }

            return;
        }

        internal void RaiseGachaStats(EventViewGacha gacha) { if (OnGachaStats != null) OnGachaStats(gacha); }
        internal void RaiseListBattles(EventListBattles battles) { if (OnListBattles != null) OnListBattles(battles); }
        internal void RaiseListDungeons(EventListDungeons dungeons) { if (OnListDungeons != null) OnListDungeons(dungeons); }
        internal void RaiseBattleInitiated(EventBattleInitiated battle) { if (OnBattleEngaged != null) OnBattleEngaged(battle); }
        internal void RaiseLeaveDungeon() { if (OnLeaveDungeon != null) OnLeaveDungeon(); }
        internal void RaiseBattleLost(EventBattleInitiated original_battle) { if (OnFailBattle != null) OnFailBattle(original_battle); }
        internal void RaiseBattleWon(EventBattleInitiated original_battle) { if (OnWinBattle != null) OnWinBattle(original_battle); }

        public void OnBeforeReturningError(Session oSession) { }

        public bool OnExecAction(string sCommand)
        {
            throw new NotImplementedException();
        }

        internal delegate void BattleInitiatedDelegate(EventBattleInitiated battle);
        internal delegate void BattleResultDelegate(EventBattleInitiated battle);
        internal delegate void ListBattlesDelegate(EventListBattles battles);
        internal delegate void ListDungeonsDelegate(EventListDungeons dungeons);
        internal delegate void GachaStatsDelegate(EventViewGacha gacha);
        internal delegate void FFRKDefaultDelegate();
        internal delegate void FFRKResponseDelegate(string Path);

        internal event BattleInitiatedDelegate OnBattleEngaged;
        internal event ListBattlesDelegate OnListBattles;
        internal event ListDungeonsDelegate OnListDungeons;
        internal event FFRKDefaultDelegate OnLeaveDungeon;
        internal event BattleResultDelegate OnWinBattle;
        internal event BattleResultDelegate OnFailBattle;
        internal event GachaStatsDelegate OnGachaStats;
        internal event FFRKResponseDelegate OnFFRKResponse;
        internal event FFRKDefaultDelegate OnItemCacheRefreshed;
    }
}
