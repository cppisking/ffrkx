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
using FFRKInspector.DataCache;
using FFRKInspector.UI;

namespace FFRKInspector.Proxy
{
	public class FFRKProxy : IAutoTamper, IHandleExecAction
	{
        TabPage mTabPage;
        FFRKTabInspector mInspectorView;
        ResponseHistory mHistory;
        FFRKMySqlInstance mDatabaseInstance;
        FFRKDataCache mDataCache;
        GameState mGameState;
        static FFRKProxy mInstance;

        public static FFRKProxy Instance { get { return mInstance; } }
        internal ResponseHistory ResponseHistory { get { return mHistory; } }
        internal GameState GameState { get { return mGameState; } }

        public FFRKProxy()
        {
        }

        public void OnLoad()
        {
            mHistory = new ResponseHistory();
            mDataCache = new FFRKDataCache();
            mGameState = new GameState();
            mDatabaseInstance = new FFRKMySqlInstance(mDataCache);
            mDatabaseInstance.BeginRefreshItemsCache();

            mTabPage = new TabPage("FFRK Inspector");
            mInspectorView = new FFRKTabInspector();
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
            else if (RequestPath.EndsWith("/leave_dungeon"))
                HandleLeaveDungeonEvent(oSession, ResponseJson);
            else if (RequestPath.EndsWith("get_battle_init_data"))
                HandleInitiateBattleEvent(oSession, ResponseJson);
            else if (RequestPath.StartsWith("/dff/gacha/probability"))
                HandleGachaStats(oSession, ResponseJson);
            else if (RequestPath.Equals("/dff/battle/lose") || RequestPath.Equals("/dff/battle/escape") || RequestPath.StartsWith("/dff/world/fail"))
                HandleBattleFailed(oSession, ResponseJson);
            else if (RequestPath.Equals("/dff/battle/win") || RequestPath.EndsWith("/win_battle"))
                HandleBattleWon(oSession, ResponseJson);
            return;
        }

        void HandleBattleFailed(Session oSession, string ResponseJson)
        {
            // Win or lose, finishing a battle means it's safe to record that encounter and its drops
            // since it won't be possible for the user to have the same drop set if they continue.
            if (mGameState.ActiveBattle != null)
            {
                mDatabaseInstance.BeginRecordBattleEncounter(mGameState.ActiveBattle);
                if (OnFailBattle != null)
                    OnFailBattle(mGameState.ActiveBattle);
            }
            mGameState.ActiveBattle = null;
        }

        void HandleBattleWon(Session oSession, string ResponseJson)
        {
            // Win or lose, finishing a battle means it's safe to record that encounter and its drops
            // since it won't be possible for the user to have the same drop set if they continue.
            if (mGameState.ActiveBattle != null)
            {
                mDatabaseInstance.BeginRecordBattleEncounter(mGameState.ActiveBattle);
                if (OnWinBattle != null)
                    OnWinBattle(mGameState.ActiveBattle);
            }
            mGameState.ActiveBattle = null;
        }

        void HandleGachaStats(Session oSession, string ResponseJson)
        {
            try
            {
                JObject parsed_object = JsonConvert.DeserializeObject<JObject>(ResponseJson);
                EventViewGacha gacha = new EventViewGacha();
                foreach (var child in parsed_object)
                {
                    try
                    {
                        uint series_id = uint.Parse(child.Key);
                        if (child.Value.Type == JTokenType.Object)
                        {
                            string serialized = JsonConvert.SerializeObject(child.Value);
                            DataGachaSeries this_series = JsonConvert.DeserializeObject<DataGachaSeries>(serialized);
                            gacha.Gachas.Add(series_id, this_series);
                        }
                    }
                    catch
                    {
                    }
                }
                if (OnGachaStats != null)
                    OnGachaStats(gacha);
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred processing the gacha stats event.  {0}", ex.Message);
            }
        }

        void HandleLeaveDungeonEvent(Session oSession, string ResponseJson)
        {
            if (OnLeaveDungeon != null)
                OnLeaveDungeon();
        }

        void HandleInitiateBattleEvent(Session oSession, string ResponseJson)
        {
            try
            {
                EventBattleInitiated result = JsonConvert.DeserializeObject<EventBattleInitiated>(ResponseJson);
                mGameState.ActiveBattle = result;
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
                mGameState.ActiveDungeon = result;
                mDatabaseInstance.BeginRecordBattleList(result);
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

                mDatabaseInstance.BeginRecordDungeonList(result);
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
        internal delegate void BattleResultDelegate(EventBattleInitiated battle);
        internal delegate void ListBattlesDelegate(EventListBattles battles);
        internal delegate void ListDungeonsDelegate(EventListDungeons dungeons);
        internal delegate void GachaStatsDelegate(EventViewGacha gacha);
        internal delegate void FFRKDefaultDelegate();
        internal delegate void FFRKResponseDelegate(string Path, string Json);

        internal event BattleInitiatedDelegate OnBattleEngaged;
        internal event ListBattlesDelegate OnListBattles;
        internal event ListDungeonsDelegate OnListDungeons;
        internal event FFRKDefaultDelegate OnLeaveDungeon;
        internal event BattleResultDelegate OnWinBattle;
        internal event BattleResultDelegate OnFailBattle;
        internal event GachaStatsDelegate OnGachaStats;
        internal event FFRKResponseDelegate OnFFRKResponse;
    }
}
