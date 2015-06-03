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
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using FFRKInspector.Config;
using FFRKInspector.GameData.Party;

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
        AppSettings mSettings;

        static readonly uint mRequiredSchema = 18;

        static FFRKProxy mInstance;

        public static FFRKProxy Instance { get { return mInstance; } }
        internal ResponseHistory ResponseHistory { get { return mHistory; } }
        internal GameState GameState { get { return mGameState; } }
        internal FFRKMySqlInstance Database { get { return mDatabaseInstance; } }
        internal FFRKDataCache Cache { get { return mCache; } }
        internal uint MinimumRequiredSchema { get { return mRequiredSchema; } }
        public AppSettings AppSettings { get { return mSettings; } }
        
        private string SettingsFile
        {
            get
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                string Folder = Path.GetDirectoryName(config.FilePath);
                return Path.Combine(Folder, "ffrk_inspector_settings.config");
            }
        }

        public FFRKProxy()
        {
        }

        public void OnLoad()
        {
            LoadAppSettings();

            mInstance = this;
            mResponseHandlers = new List<IResponseHandler>();
            mResponseHandlers.Add(new HandlePartyList());
            mResponseHandlers.Add(new HandleListBattles());
            mResponseHandlers.Add(new HandleListDungeons());
            mResponseHandlers.Add(new HandleLeaveDungeon());
            mResponseHandlers.Add(new HandleInitiateBattle());
            mResponseHandlers.Add(new HandleGachaSeriesList());
            mResponseHandlers.Add(new HandleGachaSeriesDetails());
            mResponseHandlers.Add(new HandleCompleteBattle());

            mHistory = new ResponseHistory();
            mGameState = new GameState();

            // Do this first, because some of the form's OnLoad events register event handlers with it.
            mDatabaseInstance = new FFRKMySqlInstance();
            mCache = new FFRKDataCache();

            // Do this before initializing the connection, because when it's done we need to update the
            // UI.
            mTabPage = new TabPage("FFRK Inspector");
            mInspectorView = new FFRKTabInspector();
            mInspectorView.Dock = DockStyle.Fill;
            mTabPage.Controls.Add(mInspectorView);
            FiddlerApplication.UI.tabsViews.TabPages.Add(mTabPage);

            // Do this last
            mDatabaseInstance.OnConnectionInitialized += mDatabaseInstance_OnConnectionInitialized;
            mDatabaseInstance.OnSchemaError += mDatabaseInstance_OnSchemaError;
            mDatabaseInstance.InitializeConnection(MinimumRequiredSchema);
        }

        public void OnBeforeUnload()
        {
            mInstance = null;
            SaveAppSettings();
        }

        private void LoadAppSettings()
        {
            try
            {
                using (FileStream fs = File.Open(SettingsFile, FileMode.Open))
                using (StreamReader sw = new StreamReader(fs))
                using (JsonReader jr = new JsonTextReader(sw))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    mSettings = (AppSettings)serializer.Deserialize(jr, typeof(AppSettings));
                }
            }
            catch(Exception)
            {
                mSettings = new Config.AppSettings();
            }
        }

        private void SaveAppSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            string Folder = Path.GetDirectoryName(config.FilePath);
            string SettingsFile = Path.Combine(Folder, "ffrk_inspector_settings.config");

            using (FileStream fs = File.Open(SettingsFile, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, mSettings);
            }
        }

        void mDatabaseInstance_OnSchemaError(FFRKMySqlInstance.ConnectResult ConnectResult)
        {
            mTabPage.BeginInvoke((Action)(() =>
                MessageBox.Show("This client is too old to connect against this database.  Please update FFRK Inspector")));
        }

        void mDatabaseInstance_OnConnectionInitialized(FFRKMySqlInstance.ConnectResult Result)
        {
            switch (Result)
            {
                case FFRKMySqlInstance.ConnectResult.Success:
                    PopulateDataCache();
                    break;
                case FFRKMySqlInstance.ConnectResult.SchemaTooOld:
                    mTabPage.BeginInvoke((Action)(() =>
                        MessageBox.Show(
                            "The database you are connected to is for an older version of FFRK " +
                            "Inspector.  Please point to a newer database instance.  Database " +
                            "connectivity will not be available for this session.",
                            "Database version mismatch")));
                    break;
                case FFRKMySqlInstance.ConnectResult.SchemaTooNew:
                    mTabPage.BeginInvoke((Action)(() =>
                        MessageBox.Show(
                            "FFRK Inspector is outdated and needs to be updated.  Please update " +
                            "to the latest version.  Database connectivity will not be available " +
                            "for this session.",
                            "Database version mismatch")));
                    break;
            }
        }

        private void PopulateDataCache()
        {
            DbOpLoadAllItems items_request = new DbOpLoadAllItems();
            items_request.OnRequestComplete += DbOpLoadAllItems_OnRequestComplete;
            mDatabaseInstance.BeginExecuteRequest(items_request);

            DbOpLoadAllBattles battles_request = new DbOpLoadAllBattles();
            battles_request.OnRequestComplete += DbOpLoadAllBattles_OnRequestComplete;
            mDatabaseInstance.BeginExecuteRequest(battles_request);

            DbOpLoadAllDungeons dungeons_request = new DbOpLoadAllDungeons();
            dungeons_request.OnRequestComplete += DbOpLoadAllDungeons_OnRequestComplete;
            mDatabaseInstance.BeginExecuteRequest(dungeons_request);

            DbOpLoadAllWorlds worlds_request = new DbOpLoadAllWorlds();
            worlds_request.OnRequestComplete += DbOpLoadAllWorlds_OnRequestComplete;
            mDatabaseInstance.BeginExecuteRequest(worlds_request);
        }

        void DbOpLoadAllWorlds_OnRequestComplete(FFRKDataCacheTable<DataCache.Worlds.Key, DataCache.Worlds.Data> worlds)
        {
            lock (mCache.SyncRoot)
                mCache.Worlds = worlds;
            if (OnItemCacheRefreshed != null)
                OnItemCacheRefreshed();
        }

        void DbOpLoadAllDungeons_OnRequestComplete(FFRKDataCacheTable<DataCache.Dungeons.Key, DataCache.Dungeons.Data> dungeons)
        {
            lock (mCache.SyncRoot)
                mCache.Dungeons = dungeons;
            if (OnItemCacheRefreshed != null)
                OnItemCacheRefreshed();
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

        public void AutoTamperRequestBefore(Session oSession) { }
        public void AutoTamperRequestAfter(Session oSession) { }
        public void AutoTamperResponseBefore(Session oSession) { }

        public void AutoTamperResponseAfter(Session oSession)
        {
            if (!oSession.oRequest.host.Equals("ffrk.denagames.com", StringComparison.CurrentCultureIgnoreCase))
                return;

            string RequestPath = oSession.oRequest.headers.RequestPath;
            Utility.Log.LogFormat(RequestPath);
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
                    Utility.Log.LogFormat("An error occurred processing the response from {0}.  {1}", RequestPath, ex.Message);
                }
            }

            return;
        }

        public void OnBeforeReturningError(Session oSession) { }

        public bool OnExecAction(string sCommand)
        {
            throw new NotImplementedException();
        }

        internal void RaiseGachaStats(DataGachaSeriesItemsForEntryPoints gacha) { if (OnGachaStats != null) OnGachaStats(gacha); }
        internal void RaiseListBattles(EventListBattles battles) { if (OnListBattles != null) OnListBattles(battles); }
        internal void RaiseListDungeons(EventListDungeons dungeons) { if (OnListDungeons != null) OnListDungeons(dungeons); }
        internal void RaiseBattleInitiated(EventBattleInitiated battle) { if (OnBattleEngaged != null) OnBattleEngaged(battle); }
        internal void RaiseLeaveDungeon() { if (OnLeaveDungeon != null) OnLeaveDungeon(); }
        internal void RaiseBattleComplete(EventBattleInitiated original_battle) { if (OnCompleteBattle != null) OnCompleteBattle(original_battle); }
        internal void RaisePartyList(DataPartyDetails party) { if (OnPartyList != null) OnPartyList(party); }

        internal delegate void BattleInitiatedDelegate(EventBattleInitiated battle);
        internal delegate void BattleResultDelegate(EventBattleInitiated battle);
        internal delegate void ListBattlesDelegate(EventListBattles battles);
        internal delegate void ListDungeonsDelegate(EventListDungeons dungeons);
        internal delegate void GachaStatsDelegate(DataGachaSeriesItemsForEntryPoints gacha);
        internal delegate void FFRKDefaultDelegate();
        internal delegate void FFRKResponseDelegate(string Path);
        internal delegate void FFRKPartyListDelegate(DataPartyDetails party);

        internal event BattleInitiatedDelegate OnBattleEngaged;
        internal event ListBattlesDelegate OnListBattles;
        internal event ListDungeonsDelegate OnListDungeons;
        internal event FFRKDefaultDelegate OnLeaveDungeon;
        internal event BattleResultDelegate OnCompleteBattle;
        internal event GachaStatsDelegate OnGachaStats;
        internal event FFRKResponseDelegate OnFFRKResponse;
        internal event FFRKDefaultDelegate OnItemCacheRefreshed;
        internal event FFRKPartyListDelegate OnPartyList;
    }
}
