﻿namespace FFRKInspector.UI
{
    partial class FFRKTabInspector
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
            this.tabControlFFRKInspector = new System.Windows.Forms.TabControl();
            this.tabPageDungeon = new System.Windows.Forms.TabPage();
            this.ffrkViewActiveDungeon = new FFRKInspector.UI.FFRKViewActiveDungeon();
            this.tabPageBattle = new System.Windows.Forms.TabPage();
            this.ffrkViewActiveBattle = new FFRKInspector.UI.FFRKViewActiveBattle();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.ffrkViewItemSearch1 = new FFRKInspector.UI.FFRKViewItemSearch();
            this.tabPageInventory = new System.Windows.Forms.TabPage();
            this.ffrkViewInventory1 = new FFRKInspector.UI.FFRKViewInventory();
            this.tabPageEditEquipment = new System.Windows.Forms.TabPage();
            this.ffrkViewEditItemStats1 = new FFRKInspector.UI.DatabaseUI.FFRKViewDatabase();
            this.tabPageGacha = new System.Windows.Forms.TabPage();
            this.ffrkViewGacha1 = new FFRKInspector.UI.FFRKViewGacha();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.ffrkViewAbout1 = new FFRKInspector.UI.FFRKViewAbout();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.ffrkViewDebugging1 = new FFRKInspector.UI.FFRKViewDebugging();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlFFRKInspector.SuspendLayout();
            this.tabPageDungeon.SuspendLayout();
            this.tabPageBattle.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageInventory.SuspendLayout();
            this.tabPageEditEquipment.SuspendLayout();
            this.tabPageGacha.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlFFRKInspector
            // 
            this.tabControlFFRKInspector.Controls.Add(this.tabPageDungeon);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageBattle);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageSearch);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageInventory);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageEditEquipment);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageGacha);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageAbout);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageDebug);
            this.tabControlFFRKInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFFRKInspector.Location = new System.Drawing.Point(0, 0);
            this.tabControlFFRKInspector.Name = "tabControlFFRKInspector";
            this.tabControlFFRKInspector.SelectedIndex = 0;
            this.tabControlFFRKInspector.Size = new System.Drawing.Size(991, 616);
            this.tabControlFFRKInspector.TabIndex = 0;
            // 
            // tabPageDungeon
            // 
            this.tabPageDungeon.Controls.Add(this.ffrkViewActiveDungeon);
            this.tabPageDungeon.Location = new System.Drawing.Point(4, 22);
            this.tabPageDungeon.Name = "tabPageDungeon";
            this.tabPageDungeon.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDungeon.Size = new System.Drawing.Size(983, 590);
            this.tabPageDungeon.TabIndex = 1;
            this.tabPageDungeon.Text = "Current Dungeon";
            this.tabPageDungeon.UseVisualStyleBackColor = true;
            // 
            // ffrkViewActiveDungeon
            // 
            this.ffrkViewActiveDungeon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewActiveDungeon.Location = new System.Drawing.Point(3, 3);
            this.ffrkViewActiveDungeon.Name = "ffrkViewActiveDungeon";
            this.ffrkViewActiveDungeon.Size = new System.Drawing.Size(977, 584);
            this.ffrkViewActiveDungeon.TabIndex = 0;
            // 
            // tabPageBattle
            // 
            this.tabPageBattle.Controls.Add(this.ffrkViewActiveBattle);
            this.tabPageBattle.Location = new System.Drawing.Point(4, 22);
            this.tabPageBattle.Name = "tabPageBattle";
            this.tabPageBattle.Size = new System.Drawing.Size(983, 590);
            this.tabPageBattle.TabIndex = 10;
            this.tabPageBattle.Text = "Current Battle";
            this.tabPageBattle.UseVisualStyleBackColor = true;
            // 
            // ffrkViewActiveBattle
            // 
            this.ffrkViewActiveBattle.Location = new System.Drawing.Point(-4, 0);
            this.ffrkViewActiveBattle.Name = "ffrkViewActiveBattle";
            this.ffrkViewActiveBattle.Size = new System.Drawing.Size(984, 594);
            this.ffrkViewActiveBattle.TabIndex = 0;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.ffrkViewItemSearch1);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(983, 590);
            this.tabPageSearch.TabIndex = 0;
            this.tabPageSearch.Text = "Item Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // ffrkViewItemSearch1
            // 
            this.ffrkViewItemSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewItemSearch1.Location = new System.Drawing.Point(3, 3);
            this.ffrkViewItemSearch1.Name = "ffrkViewItemSearch1";
            this.ffrkViewItemSearch1.Size = new System.Drawing.Size(977, 584);
            this.ffrkViewItemSearch1.TabIndex = 0;
            // 
            // tabPageInventory
            // 
            this.tabPageInventory.Controls.Add(this.ffrkViewInventory1);
            this.tabPageInventory.Location = new System.Drawing.Point(4, 22);
            this.tabPageInventory.Name = "tabPageInventory";
            this.tabPageInventory.Size = new System.Drawing.Size(983, 590);
            this.tabPageInventory.TabIndex = 2;
            this.tabPageInventory.Text = "Inventory";
            this.tabPageInventory.UseVisualStyleBackColor = true;
            this.tabPageInventory.Click += new System.EventHandler(this.tabPageInventory_Click);
            // 
            // ffrkViewInventory1
            // 
            this.ffrkViewInventory1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ffrkViewInventory1.Location = new System.Drawing.Point(-4, 0);
            this.ffrkViewInventory1.Name = "ffrkViewInventory1";
            this.ffrkViewInventory1.Size = new System.Drawing.Size(984, 594);
            this.ffrkViewInventory1.TabIndex = 0;
            // 
            // tabPageEditEquipment
            // 
            this.tabPageEditEquipment.Controls.Add(this.ffrkViewEditItemStats1);
            this.tabPageEditEquipment.Location = new System.Drawing.Point(4, 22);
            this.tabPageEditEquipment.Name = "tabPageEditEquipment";
            this.tabPageEditEquipment.Size = new System.Drawing.Size(983, 590);
            this.tabPageEditEquipment.TabIndex = 9;
            this.tabPageEditEquipment.Text = "Edit Database";
            this.tabPageEditEquipment.UseVisualStyleBackColor = true;
            // 
            // ffrkViewEditItemStats1
            // 
            this.ffrkViewEditItemStats1.DatabaseMode = FFRKInspector.UI.DatabaseUI.FFRKViewDatabase.DatabaseModeEnum.EquipmentAndStats;
            this.ffrkViewEditItemStats1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewEditItemStats1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewEditItemStats1.Name = "ffrkViewEditItemStats1";
            this.ffrkViewEditItemStats1.Size = new System.Drawing.Size(983, 590);
            this.ffrkViewEditItemStats1.TabIndex = 0;
            // 
            // tabPageGacha
            // 
            this.tabPageGacha.Controls.Add(this.ffrkViewGacha1);
            this.tabPageGacha.Location = new System.Drawing.Point(4, 22);
            this.tabPageGacha.Name = "tabPageGacha";
            this.tabPageGacha.Size = new System.Drawing.Size(983, 590);
            this.tabPageGacha.TabIndex = 8;
            this.tabPageGacha.Text = "Gacha";
            this.tabPageGacha.UseVisualStyleBackColor = true;
            // 
            // ffrkViewGacha1
            // 
            this.ffrkViewGacha1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewGacha1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewGacha1.Name = "ffrkViewGacha1";
            this.ffrkViewGacha1.Size = new System.Drawing.Size(983, 590);
            this.ffrkViewGacha1.TabIndex = 0;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.ffrkViewAbout1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(983, 590);
            this.tabPageAbout.TabIndex = 5;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // ffrkViewAbout1
            // 
            this.ffrkViewAbout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewAbout1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewAbout1.Name = "ffrkViewAbout1";
            this.ffrkViewAbout1.Size = new System.Drawing.Size(983, 590);
            this.ffrkViewAbout1.TabIndex = 0;
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.Controls.Add(this.ffrkViewDebugging1);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Size = new System.Drawing.Size(983, 590);
            this.tabPageDebug.TabIndex = 7;
            this.tabPageDebug.Text = "Debugging";
            this.tabPageDebug.UseVisualStyleBackColor = true;
            // 
            // ffrkViewDebugging1
            // 
            this.ffrkViewDebugging1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewDebugging1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewDebugging1.Name = "ffrkViewDebugging1";
            this.ffrkViewDebugging1.Size = new System.Drawing.Size(983, 590);
            this.ffrkViewDebugging1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelConnection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(991, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // toolStripStatusLabelConnection
            // 
            this.toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
            this.toolStripStatusLabelConnection.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabelConnection.Text = "Disconnected";
            // 
            // FFRKTabInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlFFRKInspector);
            this.Name = "FFRKTabInspector";
            this.Size = new System.Drawing.Size(991, 616);
            this.Load += new System.EventHandler(this.FFRKTabInspectorView_Load);
            this.tabControlFFRKInspector.ResumeLayout(false);
            this.tabPageDungeon.ResumeLayout(false);
            this.tabPageBattle.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageInventory.ResumeLayout(false);
            this.tabPageEditEquipment.ResumeLayout(false);
            this.tabPageGacha.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageDebug.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlFFRKInspector;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.TabPage tabPageDungeon;
        private System.Windows.Forms.TabPage tabPageInventory;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnection;
        private System.Windows.Forms.TabPage tabPageDebug;
        private FFRKViewDebugging ffrkViewDebugging1;
        private System.Windows.Forms.TabPage tabPageGacha;
        private FFRKViewGacha ffrkViewGacha1;
        private FFRKViewAbout ffrkViewAbout1;
        private FFRKViewActiveDungeon ffrkViewActiveDungeon;
        private FFRKViewItemSearch ffrkViewItemSearch1;
        private System.Windows.Forms.TabPage tabPageEditEquipment;
        private DatabaseUI.FFRKViewDatabase ffrkViewEditItemStats1;
        private System.Windows.Forms.TabPage tabPageBattle;
        private FFRKViewActiveBattle ffrkViewActiveBattle;
        private FFRKViewInventory ffrkViewInventory1;
    }
}
