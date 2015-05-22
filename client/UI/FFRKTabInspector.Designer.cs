namespace FFRKInspector.UI
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
            this.tabPageBattle = new System.Windows.Forms.TabPage();
            this.tabPageGacha = new System.Windows.Forms.TabPage();
            this.tabPageBrowse = new System.Windows.Forms.TabPage();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.tabPageInventory = new System.Windows.Forms.TabPage();
            this.tabPageAbilities = new System.Windows.Forms.TabPage();
            this.tabPageParty = new System.Windows.Forms.TabPage();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ffrkViewActiveDungeon1 = new FFRKInspector.UI.FFRKViewActiveDungeon();
            this.ffrkViewGacha1 = new FFRKInspector.UI.FFRKViewGacha();
            this.ffrkBrowseView1 = new FFRKInspector.UI.FFRKViewBrowse();
            this.ffrkViewItemSearch1 = new FFRKInspector.UI.FFRKViewItemSearch();
            this.ffrkViewAbout1 = new FFRKInspector.UI.FFRKViewAbout();
            this.ffrkViewDebugging1 = new FFRKInspector.UI.FFRKViewDebugging();
            this.ffrkViewInventory1 = new FFRKInspector.UI.FFRKViewInventory();
            this.tabControlFFRKInspector.SuspendLayout();
            this.tabPageBattle.SuspendLayout();
            this.tabPageGacha.SuspendLayout();
            this.tabPageBrowse.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageInventory.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlFFRKInspector
            // 
            this.tabControlFFRKInspector.Controls.Add(this.tabPageBattle);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageGacha);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageBrowse);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageSearch);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageInventory);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageAbilities);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageParty);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageAbout);
            this.tabControlFFRKInspector.Controls.Add(this.tabPageDebug);
            this.tabControlFFRKInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFFRKInspector.Location = new System.Drawing.Point(0, 0);
            this.tabControlFFRKInspector.Name = "tabControlFFRKInspector";
            this.tabControlFFRKInspector.SelectedIndex = 0;
            this.tabControlFFRKInspector.Size = new System.Drawing.Size(724, 383);
            this.tabControlFFRKInspector.TabIndex = 0;
            // 
            // tabPageBattle
            // 
            this.tabPageBattle.Controls.Add(this.ffrkViewActiveDungeon1);
            this.tabPageBattle.Location = new System.Drawing.Point(4, 22);
            this.tabPageBattle.Name = "tabPageBattle";
            this.tabPageBattle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBattle.Size = new System.Drawing.Size(716, 357);
            this.tabPageBattle.TabIndex = 1;
            this.tabPageBattle.Text = "Current Battle";
            this.tabPageBattle.UseVisualStyleBackColor = true;
            // 
            // tabPageGacha
            // 
            this.tabPageGacha.Controls.Add(this.ffrkViewGacha1);
            this.tabPageGacha.Location = new System.Drawing.Point(4, 22);
            this.tabPageGacha.Name = "tabPageGacha";
            this.tabPageGacha.Size = new System.Drawing.Size(716, 357);
            this.tabPageGacha.TabIndex = 8;
            this.tabPageGacha.Text = "Gacha";
            this.tabPageGacha.UseVisualStyleBackColor = true;
            // 
            // tabPageBrowse
            // 
            this.tabPageBrowse.Controls.Add(this.ffrkBrowseView1);
            this.tabPageBrowse.Location = new System.Drawing.Point(4, 22);
            this.tabPageBrowse.Name = "tabPageBrowse";
            this.tabPageBrowse.Size = new System.Drawing.Size(716, 357);
            this.tabPageBrowse.TabIndex = 6;
            this.tabPageBrowse.Text = "Browse";
            this.tabPageBrowse.UseVisualStyleBackColor = true;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.ffrkViewItemSearch1);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(716, 357);
            this.tabPageSearch.TabIndex = 0;
            this.tabPageSearch.Text = "Item Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // tabPageInventory
            // 
            this.tabPageInventory.Controls.Add(this.ffrkViewInventory1);
            this.tabPageInventory.Location = new System.Drawing.Point(4, 22);
            this.tabPageInventory.Name = "tabPageInventory";
            this.tabPageInventory.Size = new System.Drawing.Size(716, 357);
            this.tabPageInventory.TabIndex = 2;
            this.tabPageInventory.Text = "Inventory";
            this.tabPageInventory.UseVisualStyleBackColor = true;
            // 
            // tabPageAbilities
            // 
            this.tabPageAbilities.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbilities.Name = "tabPageAbilities";
            this.tabPageAbilities.Size = new System.Drawing.Size(716, 357);
            this.tabPageAbilities.TabIndex = 3;
            this.tabPageAbilities.Text = "Abilities";
            this.tabPageAbilities.UseVisualStyleBackColor = true;
            // 
            // tabPageParty
            // 
            this.tabPageParty.Location = new System.Drawing.Point(4, 22);
            this.tabPageParty.Name = "tabPageParty";
            this.tabPageParty.Size = new System.Drawing.Size(716, 357);
            this.tabPageParty.TabIndex = 4;
            this.tabPageParty.Text = "Party";
            this.tabPageParty.UseVisualStyleBackColor = true;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.ffrkViewAbout1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(716, 357);
            this.tabPageAbout.TabIndex = 5;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.Controls.Add(this.ffrkViewDebugging1);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Size = new System.Drawing.Size(716, 357);
            this.tabPageDebug.TabIndex = 7;
            this.tabPageDebug.Text = "Debugging";
            this.tabPageDebug.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelConnection,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6});
            this.statusStrip1.Location = new System.Drawing.Point(0, 361);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(724, 22);
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
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel3.Text = "Stamina:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(30, 17);
            this.toolStripStatusLabel4.Text = "0/78";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(24, 17);
            this.toolStripStatusLabel5.Text = "Gil:";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(49, 17);
            this.toolStripStatusLabel6.Text = "1234567";
            // 
            // ffrkViewActiveDungeon1
            // 
            this.ffrkViewActiveDungeon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewActiveDungeon1.Location = new System.Drawing.Point(3, 3);
            this.ffrkViewActiveDungeon1.Name = "ffrkViewActiveDungeon1";
            this.ffrkViewActiveDungeon1.Size = new System.Drawing.Size(710, 351);
            this.ffrkViewActiveDungeon1.TabIndex = 0;
            // 
            // ffrkViewGacha1
            // 
            this.ffrkViewGacha1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewGacha1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewGacha1.Name = "ffrkViewGacha1";
            this.ffrkViewGacha1.Size = new System.Drawing.Size(716, 357);
            this.ffrkViewGacha1.TabIndex = 0;
            // 
            // ffrkBrowseView1
            // 
            this.ffrkBrowseView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkBrowseView1.Location = new System.Drawing.Point(0, 0);
            this.ffrkBrowseView1.Name = "ffrkBrowseView1";
            this.ffrkBrowseView1.Size = new System.Drawing.Size(716, 357);
            this.ffrkBrowseView1.TabIndex = 0;
            // 
            // ffrkViewItemSearch1
            // 
            this.ffrkViewItemSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewItemSearch1.Location = new System.Drawing.Point(3, 3);
            this.ffrkViewItemSearch1.Name = "ffrkViewItemSearch1";
            this.ffrkViewItemSearch1.Size = new System.Drawing.Size(710, 351);
            this.ffrkViewItemSearch1.TabIndex = 0;
            // 
            // ffrkViewAbout1
            // 
            this.ffrkViewAbout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewAbout1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewAbout1.Name = "ffrkViewAbout1";
            this.ffrkViewAbout1.Size = new System.Drawing.Size(716, 357);
            this.ffrkViewAbout1.TabIndex = 0;
            // 
            // ffrkViewDebugging1
            // 
            this.ffrkViewDebugging1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewDebugging1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewDebugging1.Name = "ffrkViewDebugging1";
            this.ffrkViewDebugging1.Size = new System.Drawing.Size(716, 357);
            this.ffrkViewDebugging1.TabIndex = 0;
            // 
            // ffrkViewInventory1
            // 
            this.ffrkViewInventory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ffrkViewInventory1.Location = new System.Drawing.Point(0, 0);
            this.ffrkViewInventory1.Name = "ffrkViewInventory1";
            this.ffrkViewInventory1.Size = new System.Drawing.Size(716, 357);
            this.ffrkViewInventory1.TabIndex = 0;
            // 
            // FFRKTabInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlFFRKInspector);
            this.Name = "FFRKTabInspector";
            this.Size = new System.Drawing.Size(724, 383);
            this.Load += new System.EventHandler(this.FFRKTabInspectorView_Load);
            this.tabControlFFRKInspector.ResumeLayout(false);
            this.tabPageBattle.ResumeLayout(false);
            this.tabPageGacha.ResumeLayout(false);
            this.tabPageBrowse.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageInventory.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPageBattle;
        private System.Windows.Forms.TabPage tabPageInventory;
        private System.Windows.Forms.TabPage tabPageAbilities;
        private System.Windows.Forms.TabPage tabPageParty;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.TabPage tabPageBrowse;
        private FFRKViewBrowse ffrkBrowseView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnection;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.TabPage tabPageDebug;
        private FFRKViewDebugging ffrkViewDebugging1;
        private System.Windows.Forms.TabPage tabPageGacha;
        private FFRKViewGacha ffrkViewGacha1;
        private FFRKViewAbout ffrkViewAbout1;
        private FFRKViewActiveDungeon ffrkViewActiveDungeon1;
        private FFRKViewItemSearch ffrkViewItemSearch1;
        private FFRKViewInventory ffrkViewInventory1;


    }
}
