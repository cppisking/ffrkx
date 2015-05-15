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
            this.components = new System.ComponentModel.Container();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.buttonResetAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxWorld = new FFRKInspector.UI.DeselectableListBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.Battle = new System.Windows.Forms.Label();
            this.listBoxBattle = new FFRKInspector.UI.DeselectableListBox();
            this.labelDungeon = new System.Windows.Forms.Label();
            this.listBoxDungeon = new FFRKInspector.UI.DeselectableListBox();
            this.textBoxNameFilter = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelEquippableBy = new System.Windows.Forms.Label();
            this.listBoxEquippable = new FFRKInspector.UI.DeselectableListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxRealmSynergy = new FFRKInspector.UI.DeselectableListBox();
            this.labelRarity = new System.Windows.Forms.Label();
            this.listBoxRarity = new FFRKInspector.UI.DeselectableListBox();
            this.labelItemType = new System.Windows.Forms.Label();
            this.listBoxItemType = new FFRKInspector.UI.DeselectableListBox();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDungeon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBattle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRarity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSynergy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDropRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStamDrop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSearch = new System.Windows.Forms.Button();
            this.groupBoxParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxParameters.Controls.Add(this.buttonResetAll);
            this.groupBoxParameters.Controls.Add(this.label2);
            this.groupBoxParameters.Controls.Add(this.listBoxWorld);
            this.groupBoxParameters.Controls.Add(this.labelHelp);
            this.groupBoxParameters.Controls.Add(this.Battle);
            this.groupBoxParameters.Controls.Add(this.listBoxBattle);
            this.groupBoxParameters.Controls.Add(this.labelDungeon);
            this.groupBoxParameters.Controls.Add(this.listBoxDungeon);
            this.groupBoxParameters.Controls.Add(this.textBoxNameFilter);
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
            this.groupBoxParameters.Size = new System.Drawing.Size(1028, 233);
            this.groupBoxParameters.TabIndex = 0;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Search Parameters";
            // 
            // buttonResetAll
            // 
            this.buttonResetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetAll.Location = new System.Drawing.Point(872, 195);
            this.buttonResetAll.Name = "buttonResetAll";
            this.buttonResetAll.Size = new System.Drawing.Size(139, 27);
            this.buttonResetAll.TabIndex = 17;
            this.buttonResetAll.Text = "Reset all selections";
            this.buttonResetAll.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "World";
            // 
            // listBoxWorld
            // 
            this.listBoxWorld.FormattingEnabled = true;
            this.listBoxWorld.Items.AddRange(new object[] {
            "FF1",
            "FF2",
            "FF3"});
            this.listBoxWorld.Location = new System.Drawing.Point(371, 63);
            this.listBoxWorld.Name = "listBoxWorld";
            this.listBoxWorld.Size = new System.Drawing.Size(186, 121);
            this.listBoxWorld.TabIndex = 15;
            this.listBoxWorld.SelectionCleared += new System.EventHandler(this.listBoxWorld_SelectionCleared);
            this.listBoxWorld.SelectedIndexChanged += new System.EventHandler(this.listBoxWorld_SelectedIndexChanged);
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Location = new System.Drawing.Point(18, 23);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(582, 13);
            this.labelHelp.TabIndex = 14;
            this.labelHelp.Text = "Enter search parameters here.  Selecting nothing for a parameter will match every" +
    "thing.  You may select at most one world.";
            // 
            // Battle
            // 
            this.Battle.AutoSize = true;
            this.Battle.Location = new System.Drawing.Point(797, 45);
            this.Battle.Name = "Battle";
            this.Battle.Size = new System.Drawing.Size(34, 13);
            this.Battle.TabIndex = 13;
            this.Battle.Text = "Battle";
            // 
            // listBoxBattle
            // 
            this.listBoxBattle.FormattingEnabled = true;
            this.listBoxBattle.Items.AddRange(new object[] {
            "Zozo (Elite) - Whatever"});
            this.listBoxBattle.Location = new System.Drawing.Point(800, 63);
            this.listBoxBattle.Name = "listBoxBattle";
            this.listBoxBattle.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxBattle.Size = new System.Drawing.Size(225, 121);
            this.listBoxBattle.TabIndex = 12;
            this.listBoxBattle.SelectedIndexChanged += new System.EventHandler(this.listBoxBattle_SelectedIndexChanged);
            // 
            // labelDungeon
            // 
            this.labelDungeon.AutoSize = true;
            this.labelDungeon.Location = new System.Drawing.Point(560, 45);
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
            this.listBoxDungeon.Location = new System.Drawing.Point(563, 63);
            this.listBoxDungeon.Name = "listBoxDungeon";
            this.listBoxDungeon.Size = new System.Drawing.Size(231, 121);
            this.listBoxDungeon.TabIndex = 10;
            this.listBoxDungeon.SelectionCleared += new System.EventHandler(this.listBoxDungeon_SelectionCleared);
            this.listBoxDungeon.SelectedIndexChanged += new System.EventHandler(this.listBoxDungeon_SelectedIndexChanged);
            // 
            // textBoxNameFilter
            // 
            this.textBoxNameFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNameFilter.Location = new System.Drawing.Point(38, 199);
            this.textBoxNameFilter.Name = "textBoxNameFilter";
            this.textBoxNameFilter.Size = new System.Drawing.Size(819, 20);
            this.textBoxNameFilter.TabIndex = 9;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 202);
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
            this.listBoxEquippable.Size = new System.Drawing.Size(95, 121);
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
            "Core",
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
            this.listBoxRealmSynergy.Size = new System.Drawing.Size(82, 121);
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
            this.listBoxRarity.Size = new System.Drawing.Size(82, 121);
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
            this.listBoxItemType.Size = new System.Drawing.Size(82, 121);
            this.listBoxItemType.TabIndex = 0;
            // 
            // listViewResults
            // 
            this.listViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDungeon,
            this.columnHeaderBattle,
            this.columnHeaderType,
            this.columnHeaderRarity,
            this.columnHeaderSynergy,
            this.columnHeaderDropRate,
            this.columnHeaderStamDrop});
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.Location = new System.Drawing.Point(16, 247);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(1028, 159);
            this.listViewResults.TabIndex = 1;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            this.listViewResults.VirtualMode = true;
            this.listViewResults.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewResults_RetrieveVirtualItem);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 159;
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
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(952, 412);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(92, 27);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // FFRKViewItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.groupBoxParameters);
            this.Name = "FFRKViewItemSearch";
            this.Size = new System.Drawing.Size(1059, 455);
            this.Load += new System.EventHandler(this.FFRKViewItemSearch_Load);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Label labelItemType;
        private DeselectableListBox listBoxItemType;
        private System.Windows.Forms.Label labelRarity;
        private DeselectableListBox listBoxRarity;
        private System.Windows.Forms.Label labelEquippableBy;
        private DeselectableListBox listBoxEquippable;
        private System.Windows.Forms.Label label1;
        private DeselectableListBox listBoxRealmSynergy;
        private System.Windows.Forms.TextBox textBoxNameFilter;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderRarity;
        private System.Windows.Forms.ColumnHeader columnHeaderSynergy;
        private System.Windows.Forms.ColumnHeader columnHeaderDungeon;
        private System.Windows.Forms.ColumnHeader columnHeaderBattle;
        private System.Windows.Forms.ColumnHeader columnHeaderDropRate;
        private System.Windows.Forms.ColumnHeader columnHeaderStamDrop;
        private System.Windows.Forms.Label Battle;
        private DeselectableListBox listBoxBattle;
        private System.Windows.Forms.Label labelDungeon;
        private DeselectableListBox listBoxDungeon;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label2;
        private DeselectableListBox listBoxWorld;
        private System.Windows.Forms.Button buttonResetAll;
    }
}
