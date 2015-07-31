namespace FFRKInspector.UI
{
    partial class FFRKViewActiveBattle
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
            this.groupBoxEnemyInfo = new System.Windows.Forms.GroupBox();
            this.listViewEnemyInfo = new System.Windows.Forms.ListView();
            this.columnEnemy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMaxHP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatusWeakness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnElemWeak = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnElemResist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnElemNull = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnElemAbsorb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewDropInfo = new System.Windows.Forms.ListView();
            this.columnDropItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDropRarity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDropRound = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDropEnemy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewPrevDrops = new FFRKInspector.UI.ListViewEx();
            this.groupBoxEnemyInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEnemyInfo
            // 
            this.groupBoxEnemyInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEnemyInfo.Controls.Add(this.listViewEnemyInfo);
            this.groupBoxEnemyInfo.Location = new System.Drawing.Point(3, 8);
            this.groupBoxEnemyInfo.Name = "groupBoxEnemyInfo";
            this.groupBoxEnemyInfo.Size = new System.Drawing.Size(1032, 205);
            this.groupBoxEnemyInfo.TabIndex = 0;
            this.groupBoxEnemyInfo.TabStop = false;
            this.groupBoxEnemyInfo.Text = "Enemy Info";
            // 
            // listViewEnemyInfo
            // 
            this.listViewEnemyInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEnemy,
            this.columnMaxHP,
            this.columnStatusWeakness,
            this.columnElemWeak,
            this.columnElemResist,
            this.columnElemNull,
            this.columnElemAbsorb});
            this.listViewEnemyInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEnemyInfo.Location = new System.Drawing.Point(3, 16);
            this.listViewEnemyInfo.Name = "listViewEnemyInfo";
            this.listViewEnemyInfo.Size = new System.Drawing.Size(1026, 186);
            this.listViewEnemyInfo.TabIndex = 0;
            this.listViewEnemyInfo.UseCompatibleStateImageBehavior = false;
            this.listViewEnemyInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnEnemy
            // 
            this.columnEnemy.Text = "Enemy";
            this.columnEnemy.Width = 211;
            // 
            // columnMaxHP
            // 
            this.columnMaxHP.Text = "Max HP";
            // 
            // columnStatusWeakness
            // 
            this.columnStatusWeakness.Text = "Status Weaknesses";
            this.columnStatusWeakness.Width = 112;
            // 
            // columnElemWeak
            // 
            this.columnElemWeak.Text = "Elemental Weakness";
            this.columnElemWeak.Width = 137;
            // 
            // columnElemResist
            // 
            this.columnElemResist.Text = "Resist";
            this.columnElemResist.Width = 121;
            // 
            // columnElemNull
            // 
            this.columnElemNull.Text = "Null";
            this.columnElemNull.Width = 104;
            // 
            // columnElemAbsorb
            // 
            this.columnElemAbsorb.Text = "Absorb";
            this.columnElemAbsorb.Width = 104;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listViewDropInfo);
            this.groupBox1.Location = new System.Drawing.Point(6, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 221);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drop Info";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // listViewDropInfo
            // 
            this.listViewDropInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDropItem,
            this.columnDropRarity,
            this.columnDropRound,
            this.columnDropEnemy});
            this.listViewDropInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDropInfo.Location = new System.Drawing.Point(3, 16);
            this.listViewDropInfo.Name = "listViewDropInfo";
            this.listViewDropInfo.Size = new System.Drawing.Size(458, 202);
            this.listViewDropInfo.TabIndex = 0;
            this.listViewDropInfo.UseCompatibleStateImageBehavior = false;
            this.listViewDropInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnDropItem
            // 
            this.columnDropItem.Text = "Item";
            this.columnDropItem.Width = 120;
            // 
            // columnDropRarity
            // 
            this.columnDropRarity.Text = "Rarity";
            // 
            // columnDropRound
            // 
            this.columnDropRound.Text = "Round";
            // 
            // columnDropEnemy
            // 
            this.columnDropEnemy.Text = "Enemy";
            this.columnDropEnemy.Width = 120;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewPrevDrops);
            this.groupBox2.Location = new System.Drawing.Point(476, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 221);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Previously Recorded Drops";
            // 
            // listViewPrevDrops
            // 
            this.listViewPrevDrops.DataBinding = null;
            this.listViewPrevDrops.Location = new System.Drawing.Point(3, 17);
            this.listViewPrevDrops.Name = "listViewPrevDrops";
            this.listViewPrevDrops.SettingsKey = "FFRKViewActiveDungeon_AllDropsList";
            this.listViewPrevDrops.Size = new System.Drawing.Size(434, 203);
            this.listViewPrevDrops.TabIndex = 0;
            this.listViewPrevDrops.UseCompatibleStateImageBehavior = false;
            this.listViewPrevDrops.View = System.Windows.Forms.View.Details;
            this.listViewPrevDrops.VirtualMode = true;
            // 
            // FFRKViewActiveBattle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxEnemyInfo);
            this.Name = "FFRKViewActiveBattle";
            this.Size = new System.Drawing.Size(1038, 671);
            this.Load += new System.EventHandler(this.FFRKViewCurrentBattle_Load);
            this.groupBoxEnemyInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxEnemyInfo;
        private System.Windows.Forms.ListView listViewEnemyInfo;
        private System.Windows.Forms.ColumnHeader columnEnemy;
        private System.Windows.Forms.ColumnHeader columnMaxHP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewDropInfo;
        private System.Windows.Forms.ColumnHeader columnDropItem;
        private System.Windows.Forms.ColumnHeader columnDropRarity;
        private System.Windows.Forms.ColumnHeader columnDropRound;
        private System.Windows.Forms.ColumnHeader columnDropEnemy;
        private System.Windows.Forms.ColumnHeader columnElemWeak;
        private System.Windows.Forms.ColumnHeader columnStatusWeakness;
        private System.Windows.Forms.ColumnHeader columnElemResist;
        private System.Windows.Forms.ColumnHeader columnElemNull;
        private System.Windows.Forms.ColumnHeader columnElemAbsorb;
        private System.Windows.Forms.GroupBox groupBox2;
        private ListViewEx listViewPrevDrops;
    }
}
