namespace FFRKInspector.UI
{
    partial class FFRKViewBrowse
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Items");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Worlds");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Dungeons");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Battles");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Events");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Characters");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFRKViewBrowse));
            this.splitContainerBrowser = new System.Windows.Forms.SplitContainer();
            this.treeViewItemBrowser = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.browseItemDetails = new System.Windows.Forms.Panel();
            this.browseDungeonDetails = new System.Windows.Forms.Panel();
            this.browseEventDetails = new System.Windows.Forms.Panel();
            this.browseBattleDetails = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBrowser)).BeginInit();
            this.splitContainerBrowser.Panel1.SuspendLayout();
            this.splitContainerBrowser.Panel2.SuspendLayout();
            this.splitContainerBrowser.SuspendLayout();
            this.browseItemDetails.SuspendLayout();
            this.browseDungeonDetails.SuspendLayout();
            this.browseEventDetails.SuspendLayout();
            this.browseBattleDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerBrowser
            // 
            this.splitContainerBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBrowser.Location = new System.Drawing.Point(0, 0);
            this.splitContainerBrowser.Name = "splitContainerBrowser";
            // 
            // splitContainerBrowser.Panel1
            // 
            this.splitContainerBrowser.Panel1.Controls.Add(this.treeViewItemBrowser);
            // 
            // splitContainerBrowser.Panel2
            // 
            this.splitContainerBrowser.Panel2.Controls.Add(this.browseItemDetails);
            this.splitContainerBrowser.Size = new System.Drawing.Size(624, 375);
            this.splitContainerBrowser.SplitterDistance = 132;
            this.splitContainerBrowser.TabIndex = 1;
            // 
            // treeViewItemBrowser
            // 
            this.treeViewItemBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewItemBrowser.ImageIndex = 0;
            this.treeViewItemBrowser.ImageList = this.imageList1;
            this.treeViewItemBrowser.Location = new System.Drawing.Point(0, 0);
            this.treeViewItemBrowser.Name = "treeViewItemBrowser";
            treeNode1.Name = "NodeItems";
            treeNode1.Text = "Items";
            treeNode2.Name = "NodeWorlds";
            treeNode2.Text = "Worlds";
            treeNode3.Name = "NodeDunegons";
            treeNode3.Text = "Dungeons";
            treeNode4.Name = "NodeBattles";
            treeNode4.Text = "Battles";
            treeNode5.Name = "Events";
            treeNode5.Text = "Events";
            treeNode6.Name = "NodeCharacters";
            treeNode6.Text = "Characters";
            this.treeViewItemBrowser.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeViewItemBrowser.SelectedImageIndex = 0;
            this.treeViewItemBrowser.Size = new System.Drawing.Size(132, 375);
            this.treeViewItemBrowser.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // browseItemDetails
            // 
            this.browseItemDetails.Controls.Add(this.browseDungeonDetails);
            this.browseItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseItemDetails.Location = new System.Drawing.Point(0, 0);
            this.browseItemDetails.Name = "browseItemDetails";
            this.browseItemDetails.Size = new System.Drawing.Size(488, 375);
            this.browseItemDetails.TabIndex = 0;
            this.browseItemDetails.Visible = false;
            // 
            // browseDungeonDetails
            // 
            this.browseDungeonDetails.Controls.Add(this.browseEventDetails);
            this.browseDungeonDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseDungeonDetails.Location = new System.Drawing.Point(0, 0);
            this.browseDungeonDetails.Name = "browseDungeonDetails";
            this.browseDungeonDetails.Size = new System.Drawing.Size(488, 375);
            this.browseDungeonDetails.TabIndex = 0;
            this.browseDungeonDetails.Visible = false;
            // 
            // browseEventDetails
            // 
            this.browseEventDetails.Controls.Add(this.browseBattleDetails);
            this.browseEventDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseEventDetails.Location = new System.Drawing.Point(0, 0);
            this.browseEventDetails.Name = "browseEventDetails";
            this.browseEventDetails.Size = new System.Drawing.Size(488, 375);
            this.browseEventDetails.TabIndex = 0;
            this.browseEventDetails.Visible = false;
            // 
            // browseBattleDetails
            // 
            this.browseBattleDetails.Controls.Add(this.label1);
            this.browseBattleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseBattleDetails.Location = new System.Drawing.Point(0, 0);
            this.browseBattleDetails.Name = "browseBattleDetails";
            this.browseBattleDetails.Size = new System.Drawing.Size(488, 375);
            this.browseBattleDetails.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This page doesn\'t work yet!  It\'s coming though";
            // 
            // FFRKViewBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerBrowser);
            this.Name = "FFRKViewBrowse";
            this.Size = new System.Drawing.Size(624, 375);
            this.Load += new System.EventHandler(this.FFRKViewBrowse_Load);
            this.splitContainerBrowser.Panel1.ResumeLayout(false);
            this.splitContainerBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBrowser)).EndInit();
            this.splitContainerBrowser.ResumeLayout(false);
            this.browseItemDetails.ResumeLayout(false);
            this.browseDungeonDetails.ResumeLayout(false);
            this.browseEventDetails.ResumeLayout(false);
            this.browseBattleDetails.ResumeLayout(false);
            this.browseBattleDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerBrowser;
        private System.Windows.Forms.TreeView treeViewItemBrowser;
        private System.Windows.Forms.Panel browseItemDetails;
        private System.Windows.Forms.Panel browseDungeonDetails;
        private System.Windows.Forms.Panel browseEventDetails;
        private System.Windows.Forms.Panel browseBattleDetails;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
    }
}
