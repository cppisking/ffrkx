namespace ffrk_winproxy
{
    partial class FFRKViewDebugging
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxJson = new System.Windows.Forms.TextBox();
            this.treeViewJson = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.65347F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.93069F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.41584F));
            this.tableLayoutPanel1.Controls.Add(this.listViewHistory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxJson, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeViewJson, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(998, 523);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listViewHistory
            // 
            this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderPath});
            this.listViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHistory.FullRowSelect = true;
            this.listViewHistory.HideSelection = false;
            this.listViewHistory.Location = new System.Drawing.Point(3, 3);
            this.listViewHistory.MultiSelect = false;
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.Size = new System.Drawing.Size(339, 495);
            this.listViewHistory.TabIndex = 0;
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.Details;
            this.listViewHistory.VirtualMode = true;
            this.listViewHistory.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.listViewHistory_CacheVirtualItems);
            this.listViewHistory.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewHistory_RetrieveVirtualItem);
            this.listViewHistory.SelectedIndexChanged += new System.EventHandler(this.listViewHistory_SelectedIndexChanged);
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            this.columnHeaderTime.Width = 150;
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "Request Path";
            this.columnHeaderPath.Width = 250;
            // 
            // textBoxJson
            // 
            this.textBoxJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxJson.Location = new System.Drawing.Point(348, 3);
            this.textBoxJson.MaxLength = 131072;
            this.textBoxJson.Multiline = true;
            this.textBoxJson.Name = "textBoxJson";
            this.textBoxJson.ReadOnly = true;
            this.textBoxJson.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxJson.Size = new System.Drawing.Size(312, 495);
            this.textBoxJson.TabIndex = 1;
            this.textBoxJson.WordWrap = false;
            // 
            // treeViewJson
            // 
            this.treeViewJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewJson.Location = new System.Drawing.Point(666, 3);
            this.treeViewJson.Name = "treeViewJson";
            this.treeViewJson.Size = new System.Drawing.Size(329, 495);
            this.treeViewJson.TabIndex = 2;
            // 
            // FFRKViewDebugging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FFRKViewDebugging";
            this.Size = new System.Drawing.Size(998, 523);
            this.Load += new System.EventHandler(this.FFRKViewDebugging_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderPath;
        private System.Windows.Forms.TextBox textBoxJson;
        private System.Windows.Forms.TreeView treeViewJson;

    }
}
