namespace FFRKInspector.UI
{
    partial class FFRKViewGacha
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
            this.listViewGachaItems = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRarity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSynergy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSoulStrike = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxGachaSeries = new System.Windows.Forms.ComboBox();
            this.labelGachaSeries = new System.Windows.Forms.Label();
            this.labelRelics = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelFiveStarPct = new System.Windows.Forms.Label();
            this.labelFourStarPct = new System.Windows.Forms.Label();
            this.labelThreeStarPct = new System.Windows.Forms.Label();
            this.labelTwoStarPct = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelOneStarPct = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewGachaItems
            // 
            this.listViewGachaItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewGachaItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderRarity,
            this.columnHeaderType,
            this.columnHeaderSynergy,
            this.columnHeaderProb,
            this.columnHeaderSoulStrike});
            this.listViewGachaItems.Location = new System.Drawing.Point(15, 46);
            this.listViewGachaItems.Name = "listViewGachaItems";
            this.listViewGachaItems.Size = new System.Drawing.Size(622, 386);
            this.listViewGachaItems.TabIndex = 0;
            this.listViewGachaItems.UseCompatibleStateImageBehavior = false;
            this.listViewGachaItems.View = System.Windows.Forms.View.Details;
            this.listViewGachaItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewGachaItems_ColumnClick);
            this.listViewGachaItems.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 136;
            // 
            // columnHeaderRarity
            // 
            this.columnHeaderRarity.Text = "Rarity";
            this.columnHeaderRarity.Width = 72;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 70;
            // 
            // columnHeaderSynergy
            // 
            this.columnHeaderSynergy.Text = "Synergy";
            // 
            // columnHeaderProb
            // 
            this.columnHeaderProb.Text = "Probability";
            // 
            // columnHeaderSoulStrike
            // 
            this.columnHeaderSoulStrike.Text = "Soul Strike";
            this.columnHeaderSoulStrike.Width = 141;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxGachaSeries);
            this.groupBox1.Controls.Add(this.labelGachaSeries);
            this.groupBox1.Controls.Add(this.labelRelics);
            this.groupBox1.Controls.Add(this.listViewGachaItems);
            this.groupBox1.Location = new System.Drawing.Point(255, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 441);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items";
            // 
            // comboBoxGachaSeries
            // 
            this.comboBoxGachaSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGachaSeries.FormattingEnabled = true;
            this.comboBoxGachaSeries.Location = new System.Drawing.Point(78, 19);
            this.comboBoxGachaSeries.Name = "comboBoxGachaSeries";
            this.comboBoxGachaSeries.Size = new System.Drawing.Size(147, 21);
            this.comboBoxGachaSeries.TabIndex = 3;
            this.comboBoxGachaSeries.SelectedIndexChanged += new System.EventHandler(this.comboBoxGachaSeries_SelectedIndexChanged);
            // 
            // labelGachaSeries
            // 
            this.labelGachaSeries.AutoSize = true;
            this.labelGachaSeries.Location = new System.Drawing.Point(21, 24);
            this.labelGachaSeries.Name = "labelGachaSeries";
            this.labelGachaSeries.Size = new System.Drawing.Size(51, 13);
            this.labelGachaSeries.TabIndex = 2;
            this.labelGachaSeries.Text = "Series Id:";
            // 
            // labelRelics
            // 
            this.labelRelics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRelics.Location = new System.Drawing.Point(15, 193);
            this.labelRelics.Name = "labelRelics";
            this.labelRelics.Size = new System.Drawing.Size(622, 49);
            this.labelRelics.TabIndex = 1;
            this.labelRelics.Text = "Tap \"Relics\" followed by a specific relic banner, and then the \"Relics\"  button a" +
    "bove the text \"About rarity\" to view information for that banner.";
            this.labelRelics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.7839F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.21609F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(907, 469);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 441);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Probability by Rarity";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.69231F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.30769F));
            this.tableLayoutPanel2.Controls.Add(this.labelFiveStarPct, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelFourStarPct, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelThreeStarPct, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelTwoStarPct, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelOneStarPct, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(234, 192);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // labelFiveStarPct
            // 
            this.labelFiveStarPct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFiveStarPct.AutoSize = true;
            this.labelFiveStarPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFiveStarPct.Location = new System.Drawing.Point(154, 160);
            this.labelFiveStarPct.Name = "labelFiveStarPct";
            this.labelFiveStarPct.Size = new System.Drawing.Size(60, 24);
            this.labelFiveStarPct.TabIndex = 9;
            this.labelFiveStarPct.Text = "0.00%";
            // 
            // labelFourStarPct
            // 
            this.labelFourStarPct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFourStarPct.AutoSize = true;
            this.labelFourStarPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFourStarPct.Location = new System.Drawing.Point(154, 121);
            this.labelFourStarPct.Name = "labelFourStarPct";
            this.labelFourStarPct.Size = new System.Drawing.Size(60, 24);
            this.labelFourStarPct.TabIndex = 8;
            this.labelFourStarPct.Text = "0.00%";
            // 
            // labelThreeStarPct
            // 
            this.labelThreeStarPct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelThreeStarPct.AutoSize = true;
            this.labelThreeStarPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThreeStarPct.Location = new System.Drawing.Point(154, 83);
            this.labelThreeStarPct.Name = "labelThreeStarPct";
            this.labelThreeStarPct.Size = new System.Drawing.Size(60, 24);
            this.labelThreeStarPct.TabIndex = 7;
            this.labelThreeStarPct.Text = "0.00%";
            // 
            // labelTwoStarPct
            // 
            this.labelTwoStarPct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTwoStarPct.AutoSize = true;
            this.labelTwoStarPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTwoStarPct.Location = new System.Drawing.Point(154, 45);
            this.labelTwoStarPct.Name = "labelTwoStarPct";
            this.labelTwoStarPct.Size = new System.Drawing.Size(60, 24);
            this.labelTwoStarPct.TabIndex = 6;
            this.labelTwoStarPct.Text = "0.00%";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "★";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "★★★★★";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(84, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "★★";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(46, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "★★★★";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "★★★";
            // 
            // labelOneStarPct
            // 
            this.labelOneStarPct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOneStarPct.AutoSize = true;
            this.labelOneStarPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOneStarPct.Location = new System.Drawing.Point(154, 7);
            this.labelOneStarPct.Name = "labelOneStarPct";
            this.labelOneStarPct.Size = new System.Drawing.Size(60, 24);
            this.labelOneStarPct.TabIndex = 5;
            this.labelOneStarPct.Text = "0.00%";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(231, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(406, 27);
            this.label6.TabIndex = 4;
            this.label6.Text = "Note: I don\'t actually know what this is for, but it appears possible for a singl" +
    "e banner to have multiple drop rates.  So far they all appear identical";
            // 
            // FFRKViewGacha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FFRKViewGacha";
            this.Size = new System.Drawing.Size(907, 469);
            this.Load += new System.EventHandler(this.FFRKViewGacha_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewGachaItems;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderRarity;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderSynergy;
        private System.Windows.Forms.ColumnHeader columnHeaderProb;
        private System.Windows.Forms.ColumnHeader columnHeaderSoulStrike;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelFiveStarPct;
        private System.Windows.Forms.Label labelFourStarPct;
        private System.Windows.Forms.Label labelThreeStarPct;
        private System.Windows.Forms.Label labelTwoStarPct;
        private System.Windows.Forms.Label labelOneStarPct;
        private System.Windows.Forms.Label labelRelics;
        private System.Windows.Forms.ComboBox comboBoxGachaSeries;
        private System.Windows.Forms.Label labelGachaSeries;
        private System.Windows.Forms.Label label6;
    }
}
