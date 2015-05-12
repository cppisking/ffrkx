using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.GameData;
using System.Text.RegularExpressions;
using FFRKInspector.Proxy;

namespace FFRKInspector.UI
{
    public partial class FFRKViewGacha : UserControl
    {
        private class PercentRemover : Utility.IConverter<string, string>
        {
            public string Convert(string u)
            {
                if (u.EndsWith("%"))
                    return u.Substring(0, u.Length - 1);
                return u;
            }
        }

        private ListViewColumnSorter mSorter;

        private class GachaComboBoxEntry
        {
            public uint SeriesId;
            public DataGachaSeries SeriesData;

            public override string ToString()
            {
                return SeriesId.ToString();
            }
        }

        public FFRKViewGacha()
        {
            InitializeComponent();
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            labelRelics.Left = listViewGachaItems.Left + 10;
            labelRelics.Width = listViewGachaItems.Width - 20;
        }

        private void FFRKViewGacha_Load(object sender, EventArgs e)
        {
            if (FFRKProxy.Instance != null)
                FFRKProxy.Instance.OnGachaStats += FFRKProxy_OnGachaStats;

            mSorter = new ListViewColumnSorter();
            mSorter.AddSorter<int>(1);
            mSorter.AddSorter<int>(3);
            mSorter.AddSorter<float>(4, new PercentRemover());
            listViewGachaItems.ListViewItemSorter = mSorter;
        }

        void FFRKProxy_OnGachaStats(EventViewGacha gacha)
        {
            BeginInvoke((Action)(() => 
                { 
                    comboBoxGachaSeries.Items.Clear();
                    listViewGachaItems.Items.Clear();
                    foreach (var entry in gacha.Gachas)
                    {
                        comboBoxGachaSeries.Items.Add(new GachaComboBoxEntry { SeriesId = entry.Key, SeriesData = entry.Value });
                    }
                    if (comboBoxGachaSeries.Items.Count > 0)
                        comboBoxGachaSeries.SelectedIndex = 0;
                }));
        }

        private void comboBoxGachaSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GachaComboBoxEntry entry = (GachaComboBoxEntry)comboBoxGachaSeries.SelectedItem;
            labelOneStarPct.Text = entry.SeriesData.ProbabilityByRarity.OneStar.ToString("R") + "%";
            labelTwoStarPct.Text = entry.SeriesData.ProbabilityByRarity.TwoStar.ToString("R") + "%";
            labelThreeStarPct.Text = entry.SeriesData.ProbabilityByRarity.ThreeStar.ToString("R") + "%";
            labelFourStarPct.Text = entry.SeriesData.ProbabilityByRarity.FourStar.ToString("R") + "%";
            labelFiveStarPct.Text = entry.SeriesData.ProbabilityByRarity.FiveStar.ToString("R") + "%";

            labelRelics.Visible = false;
            listViewGachaItems.Items.Clear();
            foreach (DataGachaItem item in entry.SeriesData.Items)
            {
                Match match = Regex.Match(item.Name, "\\((.*)\\)");
                uint realm = Utility.Utility.RomanNumeralToNumber(match.Groups[1].Value);
                string[] rows = { 
                                    item.Name,
                                    item.Rarity.ToString(),
                                    item.Category,
                                    realm.ToString(),
                                    item.Probability.ToString("R") + "%",
                                    item.HasSoulStrike ? "YES" : String.Empty
                                };
                ListViewItem lvi = new ListViewItem(rows);
                listViewGachaItems.Items.Add(lvi);
            }
        }

        private void listViewGachaItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == mSorter.SortColumn)
            {
                if (mSorter.Order == SortOrder.Ascending)
                    mSorter.Order = SortOrder.Descending;
                else if (mSorter.Order == SortOrder.Descending)
                    mSorter.Order = SortOrder.Ascending;
            }
            else
            {
                mSorter.SortColumn = e.Column;
                mSorter.Order = SortOrder.Ascending;
            }
            listViewGachaItems.Sort();
        }
    }
}
