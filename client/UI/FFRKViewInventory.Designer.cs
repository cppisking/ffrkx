﻿namespace FFRKInspector.UI
{
    partial class FFRKViewInventory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFRKViewInventory));
            this.dataGridViewEquipment = new System.Windows.Forms.DataGridView();
            this.exportContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportCSVInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportJSONInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxUpgradeMode = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.linkLabelAlgo = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabelMissing = new System.Windows.Forms.LinkLabel();
            this.comboBoxScoreSelection = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSynergy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxViewMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewBuddies = new FFRKInspector.UI.DataGridViewEx();
            this.dgcCharacterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCharacterLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCharacterMaxLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCharacterOptimize = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgcCharacterOffensiveStat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgcCharacterDefensiveStat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgcItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRarity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcSynergy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcATK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDEF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipment)).BeginInit();
            this.exportContext.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuddies)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEquipment
            // 
            this.dataGridViewEquipment.AllowUserToAddRows = false;
            this.dataGridViewEquipment.AllowUserToDeleteRows = false;
            this.dataGridViewEquipment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(213)))), ((int)(((byte)(180)))));
            this.dataGridViewEquipment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEquipment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEquipment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcItemID,
            this.dgcItem,
            this.dgcCategory,
            this.dgcType,
            this.dgcRarity,
            this.dgcSynergy,
            this.dgcLevel,
            this.dgcATK,
            this.dgcMAG,
            this.dgcMND,
            this.dgcDEF,
            this.dgcRES,
            this.dgcScore});
            this.dataGridViewEquipment.ContextMenuStrip = this.exportContext;
            this.dataGridViewEquipment.Location = new System.Drawing.Point(13, 92);
            this.dataGridViewEquipment.MultiSelect = false;
            this.dataGridViewEquipment.Name = "dataGridViewEquipment";
            this.dataGridViewEquipment.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(210)))), ((int)(((byte)(228)))));
            this.dataGridViewEquipment.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEquipment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEquipment.ShowEditingIcon = false;
            this.dataGridViewEquipment.Size = new System.Drawing.Size(748, 446);
            this.dataGridViewEquipment.TabIndex = 0;
            this.dataGridViewEquipment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEquipment_CellContentClick);
            // 
            // exportContext
            // 
            this.exportContext.AccessibleName = "";
            this.exportContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCSVInventoryToolStripMenuItem, this.exportJSONInventoryToolStripMenuItem});
            this.exportContext.Name = "exportContext";
            this.exportContext.Size = new System.Drawing.Size(161, 26);
            // 
            // exportCSVInventoryToolStripMenuItem
            // 
            this.exportCSVInventoryToolStripMenuItem.Name = "exportCSVInventoryToolStripMenuItem";
            this.exportCSVInventoryToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exportCSVInventoryToolStripMenuItem.Text = "Export Inventory";
            this.exportCSVInventoryToolStripMenuItem.Click += new System.EventHandler(this.exportCSVInventoryToolStripMenuItem_Click);
            // 
            // exportJSONInventoryToolStripMenuItem
            // 
            this.exportJSONInventoryToolStripMenuItem.Name = "exportJSONInventoryToolStripMenuItem";
            this.exportJSONInventoryToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exportJSONInventoryToolStripMenuItem.Text = "Export names and levels to JSON";
            this.exportJSONInventoryToolStripMenuItem.Click += new System.EventHandler(this.exportJSONInventoryToolStripMenuItem_Click);
            // 
            // comboBoxUpgradeMode
            // 
            this.comboBoxUpgradeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpgradeMode.FormattingEnabled = true;
            this.comboBoxUpgradeMode.Items.AddRange(new object[] {
            "at the items\' current rarity and level.",
            "at the items\' current rarity and maximum level.",
            "at the maximum level combining only existing equipment.",
            "at the items\' maximum rarity and level."});
            this.comboBoxUpgradeMode.Location = new System.Drawing.Point(260, 40);
            this.comboBoxUpgradeMode.Name = "comboBoxUpgradeMode";
            this.comboBoxUpgradeMode.Size = new System.Drawing.Size(292, 21);
            this.comboBoxUpgradeMode.TabIndex = 1;
            this.comboBoxUpgradeMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxUpgradeMode_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.dataGridViewBuddies);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 606);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Available characters";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.comboBoxFilterType);
            this.groupBox4.Controls.Add(this.linkLabelAlgo);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.linkLabelMissing);
            this.groupBox4.Controls.Add(this.comboBoxScoreSelection);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.comboBoxSynergy);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.comboBoxViewMode);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.comboBoxUpgradeMode);
            this.groupBox4.Controls.Add(this.dataGridViewEquipment);
            this.groupBox4.Location = new System.Drawing.Point(405, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(767, 606);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Equipment";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(482, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Filter by Type:";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "All",
            "Weapon",
            "Armor",
            "Accessory"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(561, 65);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(150, 21);
            this.comboBoxFilterType.TabIndex = 13;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // linkLabelAlgo
            // 
            this.linkLabelAlgo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelAlgo.AutoSize = true;
            this.linkLabelAlgo.Location = new System.Drawing.Point(22, 586);
            this.linkLabelAlgo.Name = "linkLabelAlgo";
            this.linkLabelAlgo.Size = new System.Drawing.Size(277, 13);
            this.linkLabelAlgo.TabIndex = 12;
            this.linkLabelAlgo.TabStop = true;
            this.linkLabelAlgo.Text = "Click here to read about how the scoring algorithm works ";
            this.linkLabelAlgo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAlgo_LinkClicked_1);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 558);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(697, 28);
            this.label7.TabIndex = 11;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // linkLabelMissing
            // 
            this.linkLabelMissing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelMissing.AutoSize = true;
            this.linkLabelMissing.Location = new System.Drawing.Point(22, 543);
            this.linkLabelMissing.Name = "linkLabelMissing";
            this.linkLabelMissing.Size = new System.Drawing.Size(495, 13);
            this.linkLabelMissing.TabIndex = 9;
            this.linkLabelMissing.TabStop = true;
            this.linkLabelMissing.Text = "Item\'s whose score shows N/A may be missing stat information in the database.  Cl" +
    "ick here to add them.";
            this.linkLabelMissing.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMissing_LinkClicked);
            // 
            // comboBoxScoreSelection
            // 
            this.comboBoxScoreSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScoreSelection.FormattingEnabled = true;
            this.comboBoxScoreSelection.Items.AddRange(new object[] {
            "using each item\'s current rarity and level.",
            "using each item\'s current rarity and maximum level.",
            "using each item\'s maximum rarity and level."});
            this.comboBoxScoreSelection.Location = new System.Drawing.Point(94, 65);
            this.comboBoxScoreSelection.Name = "comboBoxScoreSelection";
            this.comboBoxScoreSelection.Size = new System.Drawing.Size(255, 21);
            this.comboBoxScoreSelection.TabIndex = 8;
            this.comboBoxScoreSelection.SelectedIndexChanged += new System.EventHandler(this.comboBoxScoreSelection_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Compute score";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(712, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Don\'t see anything here?  The game only sends this information the first time you" +
    " view your party screen.  You may need to close and re-open the app.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(675, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "record synergy.";
            // 
            // comboBoxSynergy
            // 
            this.comboBoxSynergy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSynergy.FormattingEnabled = true;
            this.comboBoxSynergy.Location = new System.Drawing.Point(590, 40);
            this.comboBoxSynergy.Name = "comboBoxSynergy";
            this.comboBoxSynergy.Size = new System.Drawing.Size(79, 21);
            this.comboBoxSynergy.TabIndex = 4;
            this.comboBoxSynergy.SelectedIndexChanged += new System.EventHandler(this.comboBoxSynergy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(558, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "with";
            // 
            // comboBoxViewMode
            // 
            this.comboBoxViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewMode.FormattingEnabled = true;
            this.comboBoxViewMode.Items.AddRange(new object[] {
            "Available equipment and their stats"});
            this.comboBoxViewMode.Location = new System.Drawing.Point(46, 40);
            this.comboBoxViewMode.Name = "comboBoxViewMode";
            this.comboBoxViewMode.Size = new System.Drawing.Size(208, 21);
            this.comboBoxViewMode.TabIndex = 2;
            this.comboBoxViewMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxViewMode_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "View";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "Item";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 43;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "Category";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 52;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 74;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Rarity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "Synergy";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Level";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "ATK";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "MAG";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn9.HeaderText = "MND";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 58;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn10.HeaderText = "DEF";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 57;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn11.HeaderText = "RES";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 58;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn12.HeaderText = "Score";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 57;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn13.HeaderText = "Score";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 60;
            // 
            // dataGridViewBuddies
            // 
            this.dataGridViewBuddies.AllowUserToAddRows = false;
            this.dataGridViewBuddies.AllowUserToDeleteRows = false;
            this.dataGridViewBuddies.AllowUserToResizeRows = false;
            this.dataGridViewBuddies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBuddies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcCharacterName,
            this.dgcCharacterLevel,
            this.dgcCharacterMaxLevel,
            this.dgcCharacterOptimize,
            this.dgcCharacterOffensiveStat,
            this.dgcCharacterDefensiveStat});
            this.dataGridViewBuddies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBuddies.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewBuddies.Name = "dataGridViewBuddies";
            this.dataGridViewBuddies.RowHeadersVisible = false;
            this.dataGridViewBuddies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBuddies.Size = new System.Drawing.Size(390, 587);
            this.dataGridViewBuddies.TabIndex = 0;
            this.dataGridViewBuddies.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBuddies_CellValueChanged);
            // 
            // dgcCharacterName
            // 
            this.dgcCharacterName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcCharacterName.HeaderText = "Name";
            this.dgcCharacterName.Name = "dgcCharacterName";
            this.dgcCharacterName.ReadOnly = true;
            this.dgcCharacterName.Width = 60;
            // 
            // dgcCharacterLevel
            // 
            this.dgcCharacterLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcCharacterLevel.HeaderText = "Level";
            this.dgcCharacterLevel.Name = "dgcCharacterLevel";
            this.dgcCharacterLevel.ReadOnly = true;
            // 
            // dgcCharacterMaxLevel
            // 
            this.dgcCharacterMaxLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcCharacterMaxLevel.HeaderText = "Max";
            this.dgcCharacterMaxLevel.Name = "dgcCharacterMaxLevel";
            this.dgcCharacterMaxLevel.ReadOnly = true;
            // 
            // dgcCharacterOptimize
            // 
            this.dgcCharacterOptimize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcCharacterOptimize.HeaderText = "Score";
            this.dgcCharacterOptimize.Name = "dgcCharacterOptimize";
            this.dgcCharacterOptimize.ToolTipText = "When checked, this character will be considered when computing each piece of equi" +
    "pment\'s score on the right-hand pane";
            // 
            // dgcCharacterOffensiveStat
            // 
            this.dgcCharacterOffensiveStat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcCharacterOffensiveStat.HeaderText = "Off. Stat";
            this.dgcCharacterOffensiveStat.Name = "dgcCharacterOffensiveStat";
            this.dgcCharacterOffensiveStat.ToolTipText = "Determines what stat the scoring algorithm should prioritize for this character o" +
    "n weapons";
            // 
            // dgcCharacterDefensiveStat
            // 
            this.dgcCharacterDefensiveStat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcCharacterDefensiveStat.HeaderText = "Def. Stat";
            this.dgcCharacterDefensiveStat.Name = "dgcCharacterDefensiveStat";
            this.dgcCharacterDefensiveStat.ToolTipText = "Determine what stat the scoring algorithm should prioritize for this character on" +
    " armor.";
            // 
            // dgcItemID
            // 
            this.dgcItemID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcItemID.HeaderText = "ID";
            this.dgcItemID.Name = "dgcItemID";
            this.dgcItemID.ReadOnly = true;
            this.dgcItemID.Width = 43;
            // 
            // dgcItem
            // 
            this.dgcItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcItem.HeaderText = "Item";
            this.dgcItem.Name = "dgcItem";
            this.dgcItem.ReadOnly = true;
            this.dgcItem.Width = 52;
            // 
            // dgcCategory
            // 
            this.dgcCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcCategory.HeaderText = "Category";
            this.dgcCategory.Name = "dgcCategory";
            this.dgcCategory.ReadOnly = true;
            this.dgcCategory.Width = 74;
            // 
            // dgcType
            // 
            this.dgcType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcType.HeaderText = "Type";
            this.dgcType.Name = "dgcType";
            this.dgcType.ReadOnly = true;
            this.dgcType.Width = 56;
            // 
            // dgcRarity
            // 
            this.dgcRarity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcRarity.HeaderText = "Rarity";
            this.dgcRarity.Name = "dgcRarity";
            this.dgcRarity.ReadOnly = true;
            // 
            // dgcSynergy
            // 
            this.dgcSynergy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcSynergy.HeaderText = "Synergy";
            this.dgcSynergy.Name = "dgcSynergy";
            this.dgcSynergy.ReadOnly = true;
            // 
            // dgcLevel
            // 
            this.dgcLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcLevel.HeaderText = "Level";
            this.dgcLevel.Name = "dgcLevel";
            this.dgcLevel.ReadOnly = true;
            // 
            // dgcATK
            // 
            this.dgcATK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcATK.HeaderText = "ATK";
            this.dgcATK.Name = "dgcATK";
            this.dgcATK.ReadOnly = true;
            // 
            // dgcMAG
            // 
            this.dgcMAG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcMAG.HeaderText = "MAG";
            this.dgcMAG.Name = "dgcMAG";
            this.dgcMAG.ReadOnly = true;
            // 
            // dgcMND
            // 
            this.dgcMND.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcMND.HeaderText = "MND";
            this.dgcMND.Name = "dgcMND";
            this.dgcMND.ReadOnly = true;
            // 
            // dgcDEF
            // 
            this.dgcDEF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcDEF.HeaderText = "DEF";
            this.dgcDEF.Name = "dgcDEF";
            this.dgcDEF.ReadOnly = true;
            // 
            // dgcRES
            // 
            this.dgcRES.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcRES.HeaderText = "RES";
            this.dgcRES.Name = "dgcRES";
            this.dgcRES.ReadOnly = true;
            // 
            // dgcScore
            // 
            this.dgcScore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcScore.HeaderText = "Score";
            this.dgcScore.Name = "dgcScore";
            this.dgcScore.ReadOnly = true;
            this.dgcScore.Width = 60;
            // 
            // FFRKViewInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "FFRKViewInventory";
            this.Size = new System.Drawing.Size(1172, 631);
            this.Load += new System.EventHandler(this.FFRKViewInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEquipment)).EndInit();
            this.exportContext.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuddies)).EndInit();
            this.ResumeLayout(false);

        }

        private void ComboBoxFilterType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEquipment;
        private System.Windows.Forms.ComboBox comboBoxUpgradeMode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxViewMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSynergy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewEx dataGridViewBuddies;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCharacterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCharacterLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCharacterMaxLevel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgcCharacterOptimize;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgcCharacterOffensiveStat;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgcCharacterDefensiveStat;
        private System.Windows.Forms.ComboBox comboBoxScoreSelection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabelAlgo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabelMissing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.ContextMenuStrip exportContext;
        private System.Windows.Forms.ToolStripMenuItem exportCSVInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportJSONInventoryToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRarity;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcSynergy;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcATK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMND;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDEF;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRES;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcScore;
    }
}
