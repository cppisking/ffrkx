namespace FFRKInspector.UI
{
    partial class FFRKViewActiveDungeon
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
            this.groupBoxDungeon = new System.Windows.Forms.GroupBox();
            this.listViewActiveDungeon = new System.Windows.Forms.ListView();
            this.columnHeaderBattleBoss = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBattleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBattleRounds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStamina = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxAllItems = new System.Windows.Forms.GroupBox();
            this.textBoxNameFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxRepeatable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBoxFilterSamples = new System.Windows.Forms.CheckBox();
            this.groupBoxCurrentDungeon = new System.Windows.Forms.GroupBox();
            this.listViewMasteryCondition = new System.Windows.Forms.ListView();
            this.columnMasteryCond = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCondBattle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewAllDrops = new FFRKInspector.UI.ListViewEx();
            this.groupBoxDungeon.SuspendLayout();
            this.groupBoxAllItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBoxCurrentDungeon.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxDungeon
            // 
            this.groupBoxDungeon.Controls.Add(this.listViewActiveDungeon);
            this.groupBoxDungeon.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDungeon.Name = "groupBoxDungeon";
            this.groupBoxDungeon.Size = new System.Drawing.Size(427, 213);
            this.groupBoxDungeon.TabIndex = 1;
            this.groupBoxDungeon.TabStop = false;
            this.groupBoxDungeon.Text = "(No Active Dungeon)";
            // 
            // listViewActiveDungeon
            // 
            this.listViewActiveDungeon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewActiveDungeon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderBattleBoss,
            this.columnHeaderBattleName,
            this.columnHeaderBattleRounds,
            this.columnHeaderStamina});
            this.listViewActiveDungeon.FullRowSelect = true;
            this.listViewActiveDungeon.Location = new System.Drawing.Point(6, 19);
            this.listViewActiveDungeon.Name = "listViewActiveDungeon";
            this.listViewActiveDungeon.Size = new System.Drawing.Size(412, 188);
            this.listViewActiveDungeon.TabIndex = 0;
            this.listViewActiveDungeon.UseCompatibleStateImageBehavior = false;
            this.listViewActiveDungeon.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderBattleBoss
            // 
            this.columnHeaderBattleBoss.Text = "BOSS";
            this.columnHeaderBattleBoss.Width = 57;
            // 
            // columnHeaderBattleName
            // 
            this.columnHeaderBattleName.Text = "Name";
            this.columnHeaderBattleName.Width = 190;
            // 
            // columnHeaderBattleRounds
            // 
            this.columnHeaderBattleRounds.Text = "Rounds";
            // 
            // columnHeaderStamina
            // 
            this.columnHeaderStamina.Text = "Stamina";
            // 
            // groupBoxAllItems
            // 
            this.groupBoxAllItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAllItems.Controls.Add(this.textBoxNameFilter);
            this.groupBoxAllItems.Controls.Add(this.label2);
            this.groupBoxAllItems.Controls.Add(this.checkBoxRepeatable);
            this.groupBoxAllItems.Controls.Add(this.label1);
            this.groupBoxAllItems.Controls.Add(this.numericUpDown1);
            this.groupBoxAllItems.Controls.Add(this.checkBoxFilterSamples);
            this.groupBoxAllItems.Controls.Add(this.listViewAllDrops);
            this.groupBoxAllItems.Location = new System.Drawing.Point(0, 225);
            this.groupBoxAllItems.Name = "groupBoxAllItems";
            this.groupBoxAllItems.Size = new System.Drawing.Size(1024, 201);
            this.groupBoxAllItems.TabIndex = 10;
            this.groupBoxAllItems.TabStop = false;
            this.groupBoxAllItems.Text = "All items dropped in this dungeon";
            // 
            // textBoxNameFilter
            // 
            this.textBoxNameFilter.Location = new System.Drawing.Point(699, 21);
            this.textBoxNameFilter.Name = "textBoxNameFilter";
            this.textBoxNameFilter.Size = new System.Drawing.Size(182, 20);
            this.textBoxNameFilter.TabIndex = 9;
            this.textBoxNameFilter.TextChanged += new System.EventHandler(this.textBoxNameFilter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(480, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Show only items with a name that contains: ";
            // 
            // checkBoxRepeatable
            // 
            this.checkBoxRepeatable.AutoSize = true;
            this.checkBoxRepeatable.Location = new System.Drawing.Point(307, 22);
            this.checkBoxRepeatable.Name = "checkBoxRepeatable";
            this.checkBoxRepeatable.Size = new System.Drawing.Size(154, 17);
            this.checkBoxRepeatable.TabIndex = 7;
            this.checkBoxRepeatable.Text = "Show only repeatable runs.";
            this.checkBoxRepeatable.UseVisualStyleBackColor = true;
            this.checkBoxRepeatable.CheckedChanged += new System.EventHandler(this.checkBoxRepeatable_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "runs.";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(199, 21);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(57, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // checkBoxFilterSamples
            // 
            this.checkBoxFilterSamples.AutoSize = true;
            this.checkBoxFilterSamples.Location = new System.Drawing.Point(20, 22);
            this.checkBoxFilterSamples.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxFilterSamples.Name = "checkBoxFilterSamples";
            this.checkBoxFilterSamples.Size = new System.Drawing.Size(171, 17);
            this.checkBoxFilterSamples.TabIndex = 4;
            this.checkBoxFilterSamples.Text = "Show only battles with at least ";
            this.checkBoxFilterSamples.UseVisualStyleBackColor = true;
            this.checkBoxFilterSamples.CheckedChanged += new System.EventHandler(this.checkBoxFilterSamples_CheckedChanged);
            // 
            // groupBoxCurrentDungeon
            // 
            this.groupBoxCurrentDungeon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCurrentDungeon.Controls.Add(this.listViewMasteryCondition);
            this.groupBoxCurrentDungeon.Location = new System.Drawing.Point(436, 3);
            this.groupBoxCurrentDungeon.Name = "groupBoxCurrentDungeon";
            this.groupBoxCurrentDungeon.Size = new System.Drawing.Size(588, 213);
            this.groupBoxCurrentDungeon.TabIndex = 11;
            this.groupBoxCurrentDungeon.TabStop = false;
            this.groupBoxCurrentDungeon.Text = "Dungeon Information";
            // 
            // listViewMasteryCondition
            // 
            this.listViewMasteryCondition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCondBattle,
            this.columnMasteryCond});
            this.listViewMasteryCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMasteryCondition.Location = new System.Drawing.Point(3, 16);
            this.listViewMasteryCondition.Name = "listViewMasteryCondition";
            this.listViewMasteryCondition.Size = new System.Drawing.Size(582, 194);
            this.listViewMasteryCondition.TabIndex = 0;
            this.listViewMasteryCondition.UseCompatibleStateImageBehavior = false;
            this.listViewMasteryCondition.View = System.Windows.Forms.View.Details;
            // 
            // columnMasteryCond
            // 
            this.columnMasteryCond.Text = "Mastery Condition";
            this.columnMasteryCond.Width = 296;
            // 
            // columnCondBattle
            // 
            this.columnCondBattle.Text = "Stage";
            this.columnCondBattle.Width = 136;
            // 
            // listViewAllDrops
            // 
            this.listViewAllDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAllDrops.DataBinding = null;
            this.listViewAllDrops.FullRowSelect = true;
            this.listViewAllDrops.Location = new System.Drawing.Point(14, 55);
            this.listViewAllDrops.Name = "listViewAllDrops";
            this.listViewAllDrops.SettingsKey = "FFRKViewActiveDungeon_AllDropsList";
            this.listViewAllDrops.Size = new System.Drawing.Size(1004, 140);
            this.listViewAllDrops.TabIndex = 3;
            this.listViewAllDrops.UseCompatibleStateImageBehavior = false;
            this.listViewAllDrops.View = System.Windows.Forms.View.Details;
            this.listViewAllDrops.VirtualMode = true;
            // 
            // FFRKViewActiveDungeon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCurrentDungeon);
            this.Controls.Add(this.groupBoxAllItems);
            this.Controls.Add(this.groupBoxDungeon);
            this.Name = "FFRKViewActiveDungeon";
            this.Size = new System.Drawing.Size(1038, 441);
            this.Load += new System.EventHandler(this.FFRKViewCurrentBattle_Load);
            this.groupBoxDungeon.ResumeLayout(false);
            this.groupBoxAllItems.ResumeLayout(false);
            this.groupBoxAllItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBoxCurrentDungeon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDungeon;
        private ListViewEx listViewAllDrops;
        private System.Windows.Forms.ListView listViewActiveDungeon;
        private System.Windows.Forms.ColumnHeader columnHeaderBattleName;
        private System.Windows.Forms.ColumnHeader columnHeaderBattleRounds;
        private System.Windows.Forms.ColumnHeader columnHeaderBattleBoss;
        private System.Windows.Forms.ColumnHeader columnHeaderStamina;
        private System.Windows.Forms.GroupBox groupBoxAllItems;
        private System.Windows.Forms.GroupBox groupBoxCurrentDungeon;
        private System.Windows.Forms.CheckBox checkBoxRepeatable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBoxFilterSamples;
        private System.Windows.Forms.TextBox textBoxNameFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewMasteryCondition;
        private System.Windows.Forms.ColumnHeader columnMasteryCond;
        private System.Windows.Forms.ColumnHeader columnCondBattle;
    }
}
