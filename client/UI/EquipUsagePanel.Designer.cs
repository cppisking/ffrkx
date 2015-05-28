namespace FFRKInspector.UI
{
    partial class EquipUsagePanel
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
            this.dataGridView2 = new DataGridViewEx();
            this.equipusageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.equipUsageDataSet = new FFRKInspector.EquipUsageDataSet();
            this.equip_usageTableAdapter = new FFRKInspector.EquipUsageDataSetTableAdapters.equip_usageTableAdapter();
            this.charactersTableAdapter1 = new FFRKInspector.EquipUsageDataSetTableAdapters.charactersTableAdapter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipcategoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCharacterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcEquipCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipusageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipUsageDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcCharacterId,
            this.dgcEquipCategory});
            this.dataGridView2.DataSource = this.equipusageBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(936, 489);
            this.dataGridView2.TabIndex = 0;
            // 
            // equipusageBindingSource
            // 
            this.equipusageBindingSource.DataMember = "equip_usage";
            this.equipusageBindingSource.DataSource = this.equipUsageDataSet;
            // 
            // equipUsageDataSet
            // 
            this.equipUsageDataSet.DataSetName = "EquipUsageDataSet";
            this.equipUsageDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // equip_usageTableAdapter
            // 
            this.equip_usageTableAdapter.ClearBeforeFill = true;
            // 
            // charactersTableAdapter1
            // 
            this.charactersTableAdapter1.ClearBeforeFill = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn2.HeaderText = "name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "equip_category";
            this.dataGridViewTextBoxColumn3.HeaderText = "equip_category";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // equipcategoryDataGridViewTextBoxColumn
            // 
            this.equipcategoryDataGridViewTextBoxColumn.DataPropertyName = "equip_category";
            this.equipcategoryDataGridViewTextBoxColumn.HeaderText = "equip_category";
            this.equipcategoryDataGridViewTextBoxColumn.Name = "equipcategoryDataGridViewTextBoxColumn";
            // 
            // dgcCharacterId
            // 
            this.dgcCharacterId.DataPropertyName = "character_id";
            this.dgcCharacterId.HeaderText = "character_id";
            this.dgcCharacterId.Name = "dgcCharacterId";
            this.dgcCharacterId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcEquipCategory
            // 
            this.dgcEquipCategory.DataPropertyName = "equip_category";
            this.dgcEquipCategory.HeaderText = "equip_category";
            this.dgcEquipCategory.Name = "dgcEquipCategory";
            this.dgcEquipCategory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // EquipUsagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView2);
            this.Name = "EquipUsagePanel";
            this.Size = new System.Drawing.Size(936, 489);
            this.Load += new System.EventHandler(this.EquipUsagePanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipusageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipUsageDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipcategoryDataGridViewTextBoxColumn;
        private DataGridViewEx dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipcategoryDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private EquipUsageDataSet equipUsageDataSet;
        private EquipUsageDataSetTableAdapters.equip_usageTableAdapter equip_usageTableAdapter;
        private EquipUsageDataSetTableAdapters.charactersTableAdapter charactersTableAdapter1;
        private System.Windows.Forms.BindingSource equipusageBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCharacterId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcEquipCategory;
    }
}
