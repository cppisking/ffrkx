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
using FFRKInspector.Proxy;

namespace FFRKInspector.UI
{
    public partial class FFRKViewDebugging : UserControl
    {
        private List<ListViewItem> mCache;

        public FFRKViewDebugging()
        {
            InitializeComponent();
            mCache = new List<ListViewItem>();
        }

        private void FFRKViewDebugging_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            listViewHistory.VirtualListSize = FFRKProxy.Instance.ResponseHistory.Size;
            FFRKProxy.Instance.OnFFRKResponse += FFRKProxy_OnFFRKResponse;
        }

        void FFRKProxy_OnFFRKResponse(string Path)
        {
            ++listViewHistory.VirtualListSize;
        }

        private ListViewItem CreateListViewItem(ResponseHistory.HistoryItem data)
        {
            string[] rows = {
                                    data.Timestamp.ToString(),
                                    data.Session.oRequest.headers.RequestPath
                                };
            ListViewItem Item = new ListViewItem(rows);
            Item.Tag = data;
            return Item;
        }

        private void listViewHistory_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            CacheVirtualItems(e.ItemIndex, e.ItemIndex);
            System.Diagnostics.Debug.Assert(mCache[e.ItemIndex] != null);
            e.Item = mCache[e.ItemIndex];
        }

        private void listViewHistory_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            CacheVirtualItems(e.StartIndex, e.EndIndex);
        }

        private void CacheVirtualItems(int StartIndex, int EndIndex)
        {
            // If there's not enough items in the cache, expand it filling with nulls
            if (EndIndex >= mCache.Count)
            {
                int new_size = EndIndex + 1;
                int num_items_to_add = new_size - mCache.Count;
                mCache.Capacity = new_size;
                mCache.AddRange(Enumerable.Repeat<ListViewItem>(null, num_items_to_add));
            }

            for (int i = StartIndex; i <= EndIndex; ++i)
            {
                mCache[i] = CreateListViewItem(FFRKProxy.Instance.ResponseHistory[i]);
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
                if (hi.Handler == null)
                    return;

                // This is expensive, so we do it only once
                hi.JsonObject = hi.Handler.CreateJsonObject(hi.Session);
            }

            string JsonText = JsonConvert.SerializeObject(hi.JsonObject, Formatting.Indented);
            textBoxJson.Text = JsonText;

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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            mCache.Clear();
            FFRKProxy.Instance.ResponseHistory.Clear();
            listViewHistory.VirtualListSize = 0;
        }
    }
}
