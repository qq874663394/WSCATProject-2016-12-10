namespace WSCATProject.Warehouse
{
    partial class TestVoidForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // superGridControl1
            // 
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Location = new System.Drawing.Point(-2, 267);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.AllowRowDelete = true;
            this.superGridControl1.PrimaryGrid.AllowRowInsert = true;
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl1.PrimaryGrid.ShowInsertRow = true;
            this.superGridControl1.PrimaryGrid.ParentChanged += new System.EventHandler(this.superGridControl1_PrimaryGrid_ParentChanged);
            this.superGridControl1.Size = new System.Drawing.Size(1060, 230);
            this.superGridControl1.TabIndex = 2;
            this.superGridControl1.ActiveGridChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridActiveGridChangedEventArgs>(this.superGridControl1_ActiveGridChanged);
            this.superGridControl1.CellActivated += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellActivatedEventArgs>(this.superGridControl1_CellActivated);
            this.superGridControl1.CellActivating += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellActivatingEventArgs>(this.superGridControl1_CellActivating);
            this.superGridControl1.CellValidating += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValidatingEventArgs>(this.superGridControl1_CellValidating);
            this.superGridControl1.CellValidated += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValidatedEventArgs>(this.superGridControl1_CellValidated);
            this.superGridControl1.AfterCheck += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridAfterCheckEventArgs>(this.superGridControl1_AfterCheck);
            this.superGridControl1.CellValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValueChangedEventArgs>(this.superGridControl1_CellValueChanged);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.EditorValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_EditorValueChanged);
            this.superGridControl1.FilterEditValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridFilterEditValueChangedEventArgs>(this.superGridControl1_FilterEditValueChanged);
            this.superGridControl1.FilterPopupValueChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridFilterPopupValueChangedEventArgs>(this.superGridControl1_FilterPopupValueChanged);
            this.superGridControl1.SortChanged += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEventArgs>(this.superGridControl1_SortChanged);
            this.superGridControl1.CausesValidationChanged += new System.EventHandler(this.superGridControl1_CausesValidationChanged);
            this.superGridControl1.Validated += new System.EventHandler(this.superGridControl1_Validated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Name = "gridColumn2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::WSCATProject.Properties.Resources.arrows;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1055, 494);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // TestVoidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 494);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Name = "TestVoidForm";
            this.Text = "TestVoidForm";
            this.Load += new System.EventHandler(this.TestVoidForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}