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
            this.groupBoxItemAndLocation = new System.Windows.Forms.GroupBox();
            this.textBoxNameFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxWorld = new FFRKInspector.UI.DeselectableListBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.Battle = new System.Windows.Forms.Label();
            this.listBoxBattle = new FFRKInspector.UI.DeselectableListBox();
            this.labelDungeon = new System.Windows.Forms.Label();
            this.listBoxDungeon = new FFRKInspector.UI.DeselectableListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxRealmSynergy = new FFRKInspector.UI.DeselectableListBox();
            this.labelRarity = new System.Windows.Forms.Label();
            this.listBoxRarity = new FFRKInspector.UI.DeselectableListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMinBattles = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRepeatable = new System.Windows.Forms.CheckBox();
            this.buttonResetAll = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.groupBoxSampleSize = new System.Windows.Forms.GroupBox();
            this.numericUpDownLowSampleThreshold = new System.Windows.Forms.NumericUpDown();
            this.radioButtonMinSamples = new System.Windows.Forms.RadioButton();
            this.radioButtonHelp = new System.Windows.Forms.RadioButton();
            this.radioButtonAllSamples = new System.Windows.Forms.RadioButton();
            this.groupBoxAdditional = new System.Windows.Forms.GroupBox();
            this.buttonHideParameters = new System.Windows.Forms.Button();
            this.listViewResults = new FFRKInspector.UI.ListViewEx();
            this.groupBoxItemAndLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinBattles)).BeginInit();
            this.groupBoxSampleSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowSampleThreshold)).BeginInit();
            this.groupBoxAdditional.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxItemAndLocation
            // 
            this.groupBoxItemAndLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxItemAndLocation.Controls.Add(this.textBoxNameFilter);
            this.groupBoxItemAndLocation.Controls.Add(this.label3);
            this.groupBoxItemAndLocation.Controls.Add(this.label2);
            this.groupBoxItemAndLocation.Controls.Add(this.listBoxWorld);
            this.groupBoxItemAndLocation.Controls.Add(this.labelHelp);
            this.groupBoxItemAndLocation.Controls.Add(this.Battle);
            this.groupBoxItemAndLocation.Controls.Add(this.listBoxBattle);
            this.groupBoxItemAndLocation.Controls.Add(this.labelDungeon);
            this.groupBoxItemAndLocation.Controls.Add(this.listBoxDungeon);
            this.groupBoxItemAndLocation.Controls.Add(this.label1);
            this.groupBoxItemAndLocation.Controls.Add(this.listBoxRealmSynergy);
            this.groupBoxItemAndLocation.Controls.Add(this.labelRarity);
            this.groupBoxItemAndLocation.Controls.Add(this.listBoxRarity);
            this.groupBoxItemAndLocation.Location = new System.Drawing.Point(16, 8);
            this.groupBoxItemAndLocation.Name = "groupBoxItemAndLocation";
            this.groupBoxItemAndLocation.Size = new System.Drawing.Size(852, 233);
            this.groupBoxItemAndLocation.TabIndex = 0;
            this.groupBoxItemAndLocation.TabStop = false;
            this.groupBoxItemAndLocation.Text = "Item and Location";
            // 
            // textBoxNameFilter
            // 
            this.textBoxNameFilter.Location = new System.Drawing.Point(227, 200);
            this.textBoxNameFilter.Name = "textBoxNameFilter";
            this.textBoxNameFilter.Size = new System.Drawing.Size(217, 20);
            this.textBoxNameFilter.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Show only items with a name that contains: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 45);
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
            this.listBoxWorld.Location = new System.Drawing.Point(187, 63);
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
            this.Battle.Location = new System.Drawing.Point(613, 45);
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
            this.listBoxBattle.Location = new System.Drawing.Point(616, 63);
            this.listBoxBattle.Name = "listBoxBattle";
            this.listBoxBattle.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxBattle.Size = new System.Drawing.Size(225, 121);
            this.listBoxBattle.TabIndex = 12;
            this.listBoxBattle.SelectedIndexChanged += new System.EventHandler(this.listBoxBattle_SelectedIndexChanged);
            // 
            // labelDungeon
            // 
            this.labelDungeon.AutoSize = true;
            this.labelDungeon.Location = new System.Drawing.Point(376, 45);
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
            this.listBoxDungeon.Location = new System.Drawing.Point(379, 63);
            this.listBoxDungeon.Name = "listBoxDungeon";
            this.listBoxDungeon.Size = new System.Drawing.Size(231, 121);
            this.listBoxDungeon.TabIndex = 10;
            this.listBoxDungeon.SelectionCleared += new System.EventHandler(this.listBoxDungeon_SelectionCleared);
            this.listBoxDungeon.SelectedIndexChanged += new System.EventHandler(this.listBoxDungeon_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 45);
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
            this.listBoxRealmSynergy.Location = new System.Drawing.Point(100, 63);
            this.listBoxRealmSynergy.Name = "listBoxRealmSynergy";
            this.listBoxRealmSynergy.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxRealmSynergy.Size = new System.Drawing.Size(82, 121);
            this.listBoxRealmSynergy.TabIndex = 4;
            // 
            // labelRarity
            // 
            this.labelRarity.AutoSize = true;
            this.labelRarity.Location = new System.Drawing.Point(9, 45);
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
            this.listBoxRarity.Location = new System.Drawing.Point(12, 63);
            this.listBoxRarity.Name = "listBoxRarity";
            this.listBoxRarity.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxRarity.Size = new System.Drawing.Size(82, 121);
            this.listBoxRarity.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(362, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "samples.";
            // 
            // numericUpDownMinBattles
            // 
            this.numericUpDownMinBattles.Enabled = false;
            this.numericUpDownMinBattles.Location = new System.Drawing.Point(301, 64);
            this.numericUpDownMinBattles.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMinBattles.Name = "numericUpDownMinBattles";
            this.numericUpDownMinBattles.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownMinBattles.TabIndex = 19;
            this.numericUpDownMinBattles.ValueChanged += new System.EventHandler(this.numericUpDownMinBattles_ValueChanged);
            // 
            // checkBoxRepeatable
            // 
            this.checkBoxRepeatable.AutoSize = true;
            this.checkBoxRepeatable.Location = new System.Drawing.Point(10, 19);
            this.checkBoxRepeatable.Name = "checkBoxRepeatable";
            this.checkBoxRepeatable.Size = new System.Drawing.Size(154, 17);
            this.checkBoxRepeatable.TabIndex = 21;
            this.checkBoxRepeatable.Text = "Show only repeatable runs.";
            this.checkBoxRepeatable.UseVisualStyleBackColor = true;
            this.checkBoxRepeatable.CheckedChanged += new System.EventHandler(this.checkBoxRepeatable_CheckedChanged);
            // 
            // buttonResetAll
            // 
            this.buttonResetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetAll.Location = new System.Drawing.Point(650, 579);
            this.buttonResetAll.Name = "buttonResetAll";
            this.buttonResetAll.Size = new System.Drawing.Size(120, 27);
            this.buttonResetAll.TabIndex = 17;
            this.buttonResetAll.Text = "Reset all selections";
            this.buttonResetAll.UseVisualStyleBackColor = true;
            this.buttonResetAll.Click += new System.EventHandler(this.buttonResetAll_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(776, 579);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(92, 27);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // groupBoxSampleSize
            // 
            this.groupBoxSampleSize.Controls.Add(this.numericUpDownLowSampleThreshold);
            this.groupBoxSampleSize.Controls.Add(this.radioButtonMinSamples);
            this.groupBoxSampleSize.Controls.Add(this.radioButtonHelp);
            this.groupBoxSampleSize.Controls.Add(this.label4);
            this.groupBoxSampleSize.Controls.Add(this.radioButtonAllSamples);
            this.groupBoxSampleSize.Controls.Add(this.numericUpDownMinBattles);
            this.groupBoxSampleSize.Location = new System.Drawing.Point(16, 280);
            this.groupBoxSampleSize.Name = "groupBoxSampleSize";
            this.groupBoxSampleSize.Size = new System.Drawing.Size(515, 101);
            this.groupBoxSampleSize.TabIndex = 18;
            this.groupBoxSampleSize.TabStop = false;
            this.groupBoxSampleSize.Text = "Sample Size";
            // 
            // numericUpDownLowSampleThreshold
            // 
            this.numericUpDownLowSampleThreshold.Enabled = false;
            this.numericUpDownLowSampleThreshold.Location = new System.Drawing.Point(444, 41);
            this.numericUpDownLowSampleThreshold.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownLowSampleThreshold.Name = "numericUpDownLowSampleThreshold";
            this.numericUpDownLowSampleThreshold.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownLowSampleThreshold.TabIndex = 21;
            this.numericUpDownLowSampleThreshold.ValueChanged += new System.EventHandler(this.numericUpDownLowSampleThreshold_ValueChanged);
            // 
            // radioButtonMinSamples
            // 
            this.radioButtonMinSamples.AutoSize = true;
            this.radioButtonMinSamples.Location = new System.Drawing.Point(12, 65);
            this.radioButtonMinSamples.Name = "radioButtonMinSamples";
            this.radioButtonMinSamples.Size = new System.Drawing.Size(266, 17);
            this.radioButtonMinSamples.TabIndex = 2;
            this.radioButtonMinSamples.Text = "I just want my stuff!  Show only battles with at least ";
            this.radioButtonMinSamples.UseVisualStyleBackColor = true;
            this.radioButtonMinSamples.CheckedChanged += new System.EventHandler(this.radioButtonMinSamples_CheckedChanged);
            // 
            // radioButtonHelp
            // 
            this.radioButtonHelp.AutoSize = true;
            this.radioButtonHelp.Location = new System.Drawing.Point(12, 42);
            this.radioButtonHelp.Name = "radioButtonHelp";
            this.radioButtonHelp.Size = new System.Drawing.Size(401, 17);
            this.radioButtonHelp.TabIndex = 1;
            this.radioButtonHelp.Text = "I want to help!  Show only results that need more data.  Low sample threshold = ";
            this.radioButtonHelp.UseVisualStyleBackColor = true;
            this.radioButtonHelp.CheckedChanged += new System.EventHandler(this.radioButtonHelp_CheckedChanged);
            // 
            // radioButtonAllSamples
            // 
            this.radioButtonAllSamples.AutoSize = true;
            this.radioButtonAllSamples.Checked = true;
            this.radioButtonAllSamples.Location = new System.Drawing.Point(12, 19);
            this.radioButtonAllSamples.Name = "radioButtonAllSamples";
            this.radioButtonAllSamples.Size = new System.Drawing.Size(224, 17);
            this.radioButtonAllSamples.TabIndex = 0;
            this.radioButtonAllSamples.TabStop = true;
            this.radioButtonAllSamples.Text = "Show all results, regardless of sample size.";
            this.radioButtonAllSamples.UseVisualStyleBackColor = true;
            this.radioButtonAllSamples.CheckedChanged += new System.EventHandler(this.radioButtonAllSamples_CheckedChanged);
            // 
            // groupBoxAdditional
            // 
            this.groupBoxAdditional.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAdditional.Controls.Add(this.checkBoxRepeatable);
            this.groupBoxAdditional.Location = new System.Drawing.Point(537, 280);
            this.groupBoxAdditional.Name = "groupBoxAdditional";
            this.groupBoxAdditional.Size = new System.Drawing.Size(331, 101);
            this.groupBoxAdditional.TabIndex = 19;
            this.groupBoxAdditional.TabStop = false;
            this.groupBoxAdditional.Text = "Additional Options";
            // 
            // buttonHideParameters
            // 
            this.buttonHideParameters.Location = new System.Drawing.Point(16, 247);
            this.buttonHideParameters.Name = "buttonHideParameters";
            this.buttonHideParameters.Size = new System.Drawing.Size(127, 27);
            this.buttonHideParameters.TabIndex = 20;
            this.buttonHideParameters.Text = "Hide Parameters ↑";
            this.buttonHideParameters.UseVisualStyleBackColor = true;
            this.buttonHideParameters.Click += new System.EventHandler(this.buttonHideParameters_Click);
            // 
            // listViewResults
            // 
            this.listViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResults.DataBinding = null;
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.HideSelection = false;
            this.listViewResults.Location = new System.Drawing.Point(16, 387);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.SettingsKey = "FFRKViewItemSearch_ListViewResults";
            this.listViewResults.Size = new System.Drawing.Size(852, 186);
            this.listViewResults.TabIndex = 1;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            this.listViewResults.VirtualMode = true;
            // 
            // FFRKViewItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonHideParameters);
            this.Controls.Add(this.groupBoxAdditional);
            this.Controls.Add(this.groupBoxSampleSize);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.groupBoxItemAndLocation);
            this.Controls.Add(this.buttonResetAll);
            this.DoubleBuffered = true;
            this.Name = "FFRKViewItemSearch";
            this.Size = new System.Drawing.Size(886, 622);
            this.Load += new System.EventHandler(this.FFRKViewItemSearch_Load);
            this.groupBoxItemAndLocation.ResumeLayout(false);
            this.groupBoxItemAndLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinBattles)).EndInit();
            this.groupBoxSampleSize.ResumeLayout(false);
            this.groupBoxSampleSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLowSampleThreshold)).EndInit();
            this.groupBoxAdditional.ResumeLayout(false);
            this.groupBoxAdditional.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxItemAndLocation;
        private System.Windows.Forms.Label labelRarity;
        private DeselectableListBox listBoxRarity;
        private System.Windows.Forms.Label label1;
        private DeselectableListBox listBoxRealmSynergy;
        private ListViewEx listViewResults;
        private System.Windows.Forms.Label Battle;
        private DeselectableListBox listBoxBattle;
        private System.Windows.Forms.Label labelDungeon;
        private DeselectableListBox listBoxDungeon;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label2;
        private DeselectableListBox listBoxWorld;
        private System.Windows.Forms.Button buttonResetAll;
        private System.Windows.Forms.TextBox textBoxNameFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxRepeatable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownMinBattles;
        private System.Windows.Forms.GroupBox groupBoxSampleSize;
        private System.Windows.Forms.GroupBox groupBoxAdditional;
        private System.Windows.Forms.RadioButton radioButtonMinSamples;
        private System.Windows.Forms.RadioButton radioButtonHelp;
        private System.Windows.Forms.RadioButton radioButtonAllSamples;
        private System.Windows.Forms.Button buttonHideParameters;
        private System.Windows.Forms.NumericUpDown numericUpDownLowSampleThreshold;
    }
}
