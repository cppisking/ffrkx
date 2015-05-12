namespace FFRKInspector.UI
{
    partial class FFRKViewItemSearch
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
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.Battle = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelDungeon = new System.Windows.Forms.Label();
            this.listBoxDungeon = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelEquippableBy = new System.Windows.Forms.Label();
            this.listBoxEquippable = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxRealmSynergy = new System.Windows.Forms.ListBox();
            this.labelRarity = new System.Windows.Forms.Label();
            this.listBoxRarity = new System.Windows.Forms.ListBox();
            this.labelItemType = new System.Windows.Forms.Label();
            this.listBoxItemType = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRarity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSynergy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDropRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStamDrop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDungeon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBattle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSearch = new System.Windows.Forms.Button();
            this.groupBoxParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxParameters.Controls.Add(this.labelHelp);
            this.groupBoxParameters.Controls.Add(this.Battle);
            this.groupBoxParameters.Controls.Add(this.listBox1);
            this.groupBoxParameters.Controls.Add(this.labelDungeon);
            this.groupBoxParameters.Controls.Add(this.listBoxDungeon);
            this.groupBoxParameters.Controls.Add(this.textBox1);
            this.groupBoxParameters.Controls.Add(this.labelName);
            this.groupBoxParameters.Controls.Add(this.labelEquippableBy);
            this.groupBoxParameters.Controls.Add(this.listBoxEquippable);
            this.groupBoxParameters.Controls.Add(this.label1);
            this.groupBoxParameters.Controls.Add(this.listBoxRealmSynergy);
            this.groupBoxParameters.Controls.Add(this.labelRarity);
            this.groupBoxParameters.Controls.Add(this.listBoxRarity);
            this.groupBoxParameters.Controls.Add(this.labelItemType);
            this.groupBoxParameters.Controls.Add(this.listBoxItemType);
            this.groupBoxParameters.Location = new System.Drawing.Point(16, 8);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(665, 202);
            this.groupBoxParameters.TabIndex = 0;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Search Parameters";
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Location = new System.Drawing.Point(18, 23);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(599, 13);
            this.labelHelp.TabIndex = 14;
            this.labelHelp.Text = "Enter search parameters here.  Selecting nothing for a parameter will match every" +
    "thing.  You may select at most one dungeon.";
            // 
            // Battle
            // 
            this.Battle.AutoSize = true;
            this.Battle.Location = new System.Drawing.Point(481, 45);
            this.Battle.Name = "Battle";
            this.Battle.Size = new System.Drawing.Size(34, 13);
            this.Battle.TabIndex = 13;
            this.Battle.Text = "Battle";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Zozo (Elite) - Whatever"});
            this.listBox1.Location = new System.Drawing.Point(484, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(163, 95);
            this.listBox1.TabIndex = 12;
            // 
            // labelDungeon
            // 
            this.labelDungeon.AutoSize = true;
            this.labelDungeon.Location = new System.Drawing.Point(355, 45);
            this.labelDungeon.Name = "labelDungeon";
            this.labelDungeon.Size = new System.Drawing.Size(51, 13);
            this.labelDungeon.TabIndex = 11;
            this.labelDungeon.Text = "Dungeon";
            // 
            // listBoxDungeon
            // 
            this.listBoxDungeon.FormattingEnabled = true;
            this.listBoxDungeon.Items.AddRange(new object[] {
            "Zozo",
            "Phantom Train",
            "Darill\'s Tomb"});
            this.listBoxDungeon.Location = new System.Drawing.Point(358, 63);
            this.listBoxDungeon.Name = "listBoxDungeon";
            this.listBoxDungeon.Size = new System.Drawing.Size(120, 95);
            this.listBoxDungeon.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(48, 168);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(599, 20);
            this.textBox1.TabIndex = 9;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(13, 171);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Name: ";
            // 
            // labelEquippableBy
            // 
            this.labelEquippableBy.AutoSize = true;
            this.labelEquippableBy.Location = new System.Drawing.Point(267, 45);
            this.labelEquippableBy.Name = "labelEquippableBy";
            this.labelEquippableBy.Size = new System.Drawing.Size(75, 13);
            this.labelEquippableBy.TabIndex = 7;
            this.labelEquippableBy.Text = "Equippable By";
            // 
            // listBoxEquippable
            // 
            this.listBoxEquippable.FormattingEnabled = true;
            this.listBoxEquippable.Items.AddRange(new object[] {
            "Cyan",
            "Josef",
            "Terra",
            "Kain",
            "Tidus",
            "Cloud"});
            this.listBoxEquippable.Location = new System.Drawing.Point(270, 63);
            this.listBoxEquippable.Name = "listBoxEquippable";
            this.listBoxEquippable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxEquippable.Size = new System.Drawing.Size(82, 95);
            this.listBoxEquippable.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Realm Synergy";
            // 
            // listBoxRealmSynergy
            // 
            this.listBoxRealmSynergy.FormattingEnabled = true;
            this.listBoxRealmSynergy.Items.AddRange(new object[] {
            "I",
            "II",
            "III",
            "IV",
            "V",
            "VI",
            "VII",
            "VIII",
            "IX",
            "X",
            "XI",
            "XII",
            "XIII"});
            this.listBoxRealmSynergy.Location = new System.Drawing.Point(182, 63);
            this.listBoxRealmSynergy.Name = "listBoxRealmSynergy";
            this.listBoxRealmSynergy.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxRealmSynergy.Size = new System.Drawing.Size(82, 95);
            this.listBoxRealmSynergy.TabIndex = 4;
            // 
            // labelRarity
            // 
            this.labelRarity.AutoSize = true;
            this.labelRarity.Location = new System.Drawing.Point(91, 45);
            this.labelRarity.Name = "labelRarity";
            this.labelRarity.Size = new System.Drawing.Size(34, 13);
            this.labelRarity.TabIndex = 3;
            this.labelRarity.Text = "Rarity";
            // 
            // listBoxRarity
            // 
            this.listBoxRarity.FormattingEnabled = true;
            this.listBoxRarity.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.listBoxRarity.Location = new System.Drawing.Point(94, 63);
            this.listBoxRarity.Name = "listBoxRarity";
            this.listBoxRarity.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxRarity.Size = new System.Drawing.Size(82, 95);
            this.listBoxRarity.TabIndex = 2;
            // 
            // labelItemType
            // 
            this.labelItemType.AutoSize = true;
            this.labelItemType.Location = new System.Drawing.Point(6, 45);
            this.labelItemType.Name = "labelItemType";
            this.labelItemType.Size = new System.Drawing.Size(54, 13);
            this.labelItemType.TabIndex = 1;
            this.labelItemType.Text = "Item Type";
            // 
            // listBoxItemType
            // 
            this.listBoxItemType.FormattingEnabled = true;
            this.listBoxItemType.Items.AddRange(new object[] {
            "Weapon",
            "Armor",
            "Orb",
            "Other"});
            this.listBoxItemType.Location = new System.Drawing.Point(6, 63);
            this.listBoxItemType.Name = "listBoxItemType";
            this.listBoxItemType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxItemType.Size = new System.Drawing.Size(82, 95);
            this.listBoxItemType.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderType,
            this.columnHeaderRarity,
            this.columnHeaderSynergy,
            this.columnHeaderDropRate,
            this.columnHeaderStamDrop,
            this.columnHeaderDungeon,
            this.columnHeaderBattle});
            this.listView1.Location = new System.Drawing.Point(16, 216);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(665, 178);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            // 
            // columnHeaderRarity
            // 
            this.columnHeaderRarity.Text = "Rarity";
            // 
            // columnHeaderSynergy
            // 
            this.columnHeaderSynergy.Text = "Synergy";
            // 
            // columnHeaderDropRate
            // 
            this.columnHeaderDropRate.Text = "Drop Rate";
            this.columnHeaderDropRate.Width = 69;
            // 
            // columnHeaderStamDrop
            // 
            this.columnHeaderStamDrop.Text = "Stamina / Drop";
            this.columnHeaderStamDrop.Width = 93;
            // 
            // columnHeaderDungeon
            // 
            this.columnHeaderDungeon.Text = "Dungeon";
            this.columnHeaderDungeon.Width = 96;
            // 
            // columnHeaderBattle
            // 
            this.columnHeaderBattle.Text = "Battle";
            this.columnHeaderBattle.Width = 163;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(589, 400);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(92, 27);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // FFRKViewItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBoxParameters);
            this.Name = "FFRKViewItemSearch";
            this.Size = new System.Drawing.Size(696, 436);
            this.Load += new System.EventHandler(this.FFRKViewItemSearch_Load);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Label labelItemType;
        private System.Windows.Forms.ListBox listBoxItemType;
        private System.Windows.Forms.Label labelRarity;
        private System.Windows.Forms.ListBox listBoxRarity;
        private System.Windows.Forms.Label labelEquippableBy;
        private System.Windows.Forms.ListBox listBoxEquippable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxRealmSynergy;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderRarity;
        private System.Windows.Forms.ColumnHeader columnHeaderSynergy;
        private System.Windows.Forms.ColumnHeader columnHeaderDungeon;
        private System.Windows.Forms.ColumnHeader columnHeaderBattle;
        private System.Windows.Forms.ColumnHeader columnHeaderDropRate;
        private System.Windows.Forms.ColumnHeader columnHeaderStamDrop;
        private System.Windows.Forms.Label Battle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelDungeon;
        private System.Windows.Forms.ListBox listBoxDungeon;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonSearch;
    }
}
