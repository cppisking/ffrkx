namespace FFRKInspector.UI
{
    partial class FFRKViewEditItemStats
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonCommit = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.equipmentstatsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ffrktestDataSet = new FFRKInspector.ffrktestDataSet();
            this.equipment_statsTableAdapter = new FFRKInspector.ffrktestDataSetTableAdapters.equipment_statsTableAdapter();
            this.equipmentidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseatkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basemagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseaccDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basedefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseresDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseevaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basemndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxatkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxmagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxaccDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxdefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxresDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxevaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxmndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentstatsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ffrktestDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.equipmentidDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.baseatkDataGridViewTextBoxColumn,
            this.basemagDataGridViewTextBoxColumn,
            this.baseaccDataGridViewTextBoxColumn,
            this.basedefDataGridViewTextBoxColumn,
            this.baseresDataGridViewTextBoxColumn,
            this.baseevaDataGridViewTextBoxColumn,
            this.basemndDataGridViewTextBoxColumn,
            this.maxatkDataGridViewTextBoxColumn,
            this.maxmagDataGridViewTextBoxColumn,
            this.maxaccDataGridViewTextBoxColumn,
            this.maxdefDataGridViewTextBoxColumn,
            this.maxresDataGridViewTextBoxColumn,
            this.maxevaDataGridViewTextBoxColumn,
            this.maxmndDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.equipmentstatsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(965, 424);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonCommit
            // 
            this.buttonCommit.Location = new System.Drawing.Point(588, 466);
            this.buttonCommit.Name = "buttonCommit";
            this.buttonCommit.Size = new System.Drawing.Size(151, 46);
            this.buttonCommit.TabIndex = 1;
            this.buttonCommit.Text = "Commit";
            this.buttonCommit.UseVisualStyleBackColor = true;
            this.buttonCommit.Click += new System.EventHandler(this.buttonCommit_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(745, 466);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(151, 46);
            this.buttonReload.TabIndex = 2;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // equipmentstatsBindingSource
            // 
            this.equipmentstatsBindingSource.DataMember = "equipment_stats";
            this.equipmentstatsBindingSource.DataSource = this.ffrktestDataSet;
            // 
            // ffrktestDataSet
            // 
            this.ffrktestDataSet.DataSetName = "ffrktestDataSet";
            this.ffrktestDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // equipment_statsTableAdapter
            // 
            this.equipment_statsTableAdapter.ClearBeforeFill = true;
            // 
            // equipmentidDataGridViewTextBoxColumn
            // 
            this.equipmentidDataGridViewTextBoxColumn.DataPropertyName = "equipment_id";
            this.equipmentidDataGridViewTextBoxColumn.HeaderText = "equipment_id";
            this.equipmentidDataGridViewTextBoxColumn.Name = "equipmentidDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // baseatkDataGridViewTextBoxColumn
            // 
            this.baseatkDataGridViewTextBoxColumn.DataPropertyName = "base_atk";
            this.baseatkDataGridViewTextBoxColumn.HeaderText = "base_atk";
            this.baseatkDataGridViewTextBoxColumn.Name = "baseatkDataGridViewTextBoxColumn";
            // 
            // basemagDataGridViewTextBoxColumn
            // 
            this.basemagDataGridViewTextBoxColumn.DataPropertyName = "base_mag";
            this.basemagDataGridViewTextBoxColumn.HeaderText = "base_mag";
            this.basemagDataGridViewTextBoxColumn.Name = "basemagDataGridViewTextBoxColumn";
            // 
            // baseaccDataGridViewTextBoxColumn
            // 
            this.baseaccDataGridViewTextBoxColumn.DataPropertyName = "base_acc";
            this.baseaccDataGridViewTextBoxColumn.HeaderText = "base_acc";
            this.baseaccDataGridViewTextBoxColumn.Name = "baseaccDataGridViewTextBoxColumn";
            // 
            // basedefDataGridViewTextBoxColumn
            // 
            this.basedefDataGridViewTextBoxColumn.DataPropertyName = "base_def";
            this.basedefDataGridViewTextBoxColumn.HeaderText = "base_def";
            this.basedefDataGridViewTextBoxColumn.Name = "basedefDataGridViewTextBoxColumn";
            // 
            // baseresDataGridViewTextBoxColumn
            // 
            this.baseresDataGridViewTextBoxColumn.DataPropertyName = "base_res";
            this.baseresDataGridViewTextBoxColumn.HeaderText = "base_res";
            this.baseresDataGridViewTextBoxColumn.Name = "baseresDataGridViewTextBoxColumn";
            // 
            // baseevaDataGridViewTextBoxColumn
            // 
            this.baseevaDataGridViewTextBoxColumn.DataPropertyName = "base_eva";
            this.baseevaDataGridViewTextBoxColumn.HeaderText = "base_eva";
            this.baseevaDataGridViewTextBoxColumn.Name = "baseevaDataGridViewTextBoxColumn";
            // 
            // basemndDataGridViewTextBoxColumn
            // 
            this.basemndDataGridViewTextBoxColumn.DataPropertyName = "base_mnd";
            this.basemndDataGridViewTextBoxColumn.HeaderText = "base_mnd";
            this.basemndDataGridViewTextBoxColumn.Name = "basemndDataGridViewTextBoxColumn";
            // 
            // maxatkDataGridViewTextBoxColumn
            // 
            this.maxatkDataGridViewTextBoxColumn.DataPropertyName = "max_atk";
            this.maxatkDataGridViewTextBoxColumn.HeaderText = "max_atk";
            this.maxatkDataGridViewTextBoxColumn.Name = "maxatkDataGridViewTextBoxColumn";
            // 
            // maxmagDataGridViewTextBoxColumn
            // 
            this.maxmagDataGridViewTextBoxColumn.DataPropertyName = "max_mag";
            this.maxmagDataGridViewTextBoxColumn.HeaderText = "max_mag";
            this.maxmagDataGridViewTextBoxColumn.Name = "maxmagDataGridViewTextBoxColumn";
            // 
            // maxaccDataGridViewTextBoxColumn
            // 
            this.maxaccDataGridViewTextBoxColumn.DataPropertyName = "max_acc";
            this.maxaccDataGridViewTextBoxColumn.HeaderText = "max_acc";
            this.maxaccDataGridViewTextBoxColumn.Name = "maxaccDataGridViewTextBoxColumn";
            // 
            // maxdefDataGridViewTextBoxColumn
            // 
            this.maxdefDataGridViewTextBoxColumn.DataPropertyName = "max_def";
            this.maxdefDataGridViewTextBoxColumn.HeaderText = "max_def";
            this.maxdefDataGridViewTextBoxColumn.Name = "maxdefDataGridViewTextBoxColumn";
            // 
            // maxresDataGridViewTextBoxColumn
            // 
            this.maxresDataGridViewTextBoxColumn.DataPropertyName = "max_res";
            this.maxresDataGridViewTextBoxColumn.HeaderText = "max_res";
            this.maxresDataGridViewTextBoxColumn.Name = "maxresDataGridViewTextBoxColumn";
            // 
            // maxevaDataGridViewTextBoxColumn
            // 
            this.maxevaDataGridViewTextBoxColumn.DataPropertyName = "max_eva";
            this.maxevaDataGridViewTextBoxColumn.HeaderText = "max_eva";
            this.maxevaDataGridViewTextBoxColumn.Name = "maxevaDataGridViewTextBoxColumn";
            // 
            // maxmndDataGridViewTextBoxColumn
            // 
            this.maxmndDataGridViewTextBoxColumn.DataPropertyName = "max_mnd";
            this.maxmndDataGridViewTextBoxColumn.HeaderText = "max_mnd";
            this.maxmndDataGridViewTextBoxColumn.Name = "maxmndDataGridViewTextBoxColumn";
            // 
            // FFRKViewEditItemStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.buttonCommit);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FFRKViewEditItemStats";
            this.Size = new System.Drawing.Size(968, 543);
            this.Load += new System.EventHandler(this.FFRKViewEditItemStats_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.equipmentstatsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ffrktestDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource equipmentstatsBindingSource;
        private ffrktestDataSet ffrktestDataSet;
        private ffrktestDataSetTableAdapters.equipment_statsTableAdapter equipment_statsTableAdapter;
        private System.Windows.Forms.Button buttonCommit;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipmentidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseatkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn basemagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseaccDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn basedefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseresDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseevaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn basemndDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxatkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxmagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxaccDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxdefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxresDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxevaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxmndDataGridViewTextBoxColumn;
    }
}
