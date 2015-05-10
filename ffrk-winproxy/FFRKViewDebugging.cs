using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ffrk_winproxy
{
    public partial class FFRKViewDebugging : UserControl
    {
        private ListViewItem[] mCache;
        private int mFirstCachedItem;

        public FFRKViewDebugging()
        {
            InitializeComponent();
            mCache = null;
            mFirstCachedItem = 0;
        }

        private void FFRKViewDebugging_Load(object sender, EventArgs e)
        {
            if (FFRKProxy.Instance != null)
            {
                listViewHistory.VirtualListSize = FFRKProxy.Instance.ResponseHistory.Size;
                FFRKProxy.Instance.OnFFRKResponse += FFRKProxy_OnFFRKResponse;
            }
        }

        void FFRKProxy_OnFFRKResponse(string Path, string Json)
        {
            ++listViewHistory.VirtualListSize;
        }

        private ListViewItem CreateListViewItem(ResponseHistory.HistoryItem data)
        {
            string[] rows = {
                                    data.Timestamp.ToString(),
                                    data.RequestPath
                                };
            ListViewItem Item = new ListViewItem(rows);
            Item.Tag = data;
            return Item;
        }

        private void listViewHistory_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            // Try to pull it from the cache, but if it's not there, just make a new one.
            if (mCache != null && e.ItemIndex >= mFirstCachedItem && e.ItemIndex < mFirstCachedItem + mCache.Length)
            {
                e.Item = mCache[e.ItemIndex - mFirstCachedItem];
            }
            else
                e.Item = CreateListViewItem(FFRKProxy.Instance.ResponseHistory[e.ItemIndex]);
        }

        private void listViewHistory_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            if (mCache != null && e.StartIndex >= mFirstCachedItem && e.EndIndex <= mFirstCachedItem + mCache.Length)
                return;

            mFirstCachedItem = e.StartIndex;
            int length = e.EndIndex - e.StartIndex + 1;
            mCache = new ListViewItem[length];
            for (int i = 0; i <= e.EndIndex; ++i)
            {
                mCache[i] = CreateListViewItem(FFRKProxy.Instance.ResponseHistory[i + mFirstCachedItem]);
            }
        }

        private void listViewHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeViewJson.Nodes.Clear();

            if (listViewHistory.SelectedIndices.Count == 0)
                return;

            int index = listViewHistory.SelectedIndices[0];
            ResponseHistory.HistoryItem hi = FFRKProxy.Instance.ResponseHistory[index];
            if (hi.JsonObject == null)
            {
                hi.JsonObject = JsonConvert.DeserializeObject<JObject>(hi.JsonText);
                hi.JsonText = JsonConvert.SerializeObject(hi.JsonObject, Formatting.Indented);
            }
            textBoxJson.Text = hi.JsonText;

            treeViewJson.Nodes.Clear();
            TreeNode root = new TreeNode("ROOT");
            treeViewJson.Nodes.Add(root);
            Json2Tree(root, hi.JsonObject);

            // Expand 2 levels deep
            root.Expand();
            foreach (TreeNode child in root.Nodes)
                child.Expand();
            root.EnsureVisible();
        }

        private void Json2Tree(TreeNode parent, JObject obj)
        {
            foreach (var token in obj)
            {
                TreeNode child = new TreeNode();
                if (token.Value.Type == JTokenType.Object)
                {
                    JObject o = (JObject)token.Value;
                    child.Text = token.Key.ToString();
                    Json2Tree(child, o);
                }
                else if (token.Value.Type == JTokenType.Array)
                {
                    child.Text = token.Key.ToString();
                    int ix = 0;
                    foreach (var itm in token.Value)
                    {
                        if (itm.Type == JTokenType.Object)
                        {
                            TreeNode objTN = new TreeNode(token.Key.ToString() + "[" + ix + "]");
                            child.Nodes.Add(objTN);

                            JObject o = (JObject)itm;
                            Json2Tree(objTN, o);
                            ix++;
                        }
                        else if (itm.Type == JTokenType.Array)
                        {
                            ix++;
                            TreeNode dataArray = new TreeNode();
                            foreach (var data in itm)
                            {
                                dataArray.Text = token.Key.ToString() + "[" + ix + "]";
                                dataArray.Nodes.Add(data.ToString());
                            }
                            child.Nodes.Add(dataArray);
                        }
                        else
                        {
                            child.Nodes.Add(itm.ToString());
                        }
                    }
                }
                else
                {
                    child.Text = String.Format("'{0}' = '{1}'", token.Key.ToString(), token.Value.ToString());
                }
                parent.Nodes.Add(child);
            }
        }
    }
}
